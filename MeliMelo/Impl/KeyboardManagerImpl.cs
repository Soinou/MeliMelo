using MeliMelo.Core.Shortcuts;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MeliMelo.Impl
{
    /// <summary>
    /// Represents a hook around the keyboard
    /// </summary>
    internal sealed class KeyboardManagerImpl : IKeyboardManager, IDisposable
    {
        /// <summary>
        /// Creates a new Keyboard
        /// </summary>
        public KeyboardManagerImpl()
        {
            window_ = new Window();

            window_.KeyPressed += delegate (object sender, KeyPressedEventArgs args)
            {
                if (KeyPressed != null)
                    KeyPressed(this, args);
            };
        }

        /// <summary>
        /// Disposes of the used resources
        /// </summary>
        public void Dispose()
        {
            for (int i = current_id_; i > 0; i--)
                UnregisterHotKey(window_.Handle, i);

            window_.Dispose();
        }

        /// <summary>
        /// Registers a hot key in the system.
        /// </summary>
        /// <param name="modifier">The modifiers that are associated with the hot key.</param>
        /// <param name="key">The key itself that is associated with the hot key.</param>
        public int Register(Modifiers modifier, Keys key)
        {
            current_id_ = current_id_ + 1;

            if (!RegisterHotKey(window_.Handle, current_id_, (uint)modifier, (uint)key))
                throw new InvalidOperationException("Couldn’t register the hot key.");

            return current_id_;
        }

        /// <summary>
        /// Sends the given key to the active window
        /// </summary>
        /// <param name="key">Key or combination of keys to send</param>
        public void Send(string key)
        {
            SendKeys.SendWait(key);
        }

        /// <summary>
        /// Unregisters a hot key
        /// </summary>
        /// <param name="id">Id of the hotkey to unregister</param>
        public void Unregister(int id)
        {
            if (!UnregisterHotKey(window_.Handle, id))
                throw new InvalidOperationException("Couldn't unregister the hot key.");
        }

        /// <summary>
        /// A hot key has been pressed.
        /// </summary>
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        /// <summary>
        /// Represents the window that is used internally to get the messages.
        /// </summary>
        private class Window : NativeWindow, IDisposable
        {
            /// <summary>
            /// Creates a new Window
            /// </summary>
            public Window()
            {
                CreateHandle(new CreateParams());
            }

            /// <summary>
            /// Disposes of the used resources
            /// </summary>
            public void Dispose()
            {
                DestroyHandle();
            }

            /// <summary>
            /// Triggered when a registered hotkey is pressed
            /// </summary>
            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            /// <summary>
            /// Overridden to get the notifications.
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_HOTKEY)
                {
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    Modifiers modifier = (Modifiers)((int)m.LParam & 0xFFFF);

                    if (KeyPressed != null)
                        KeyPressed(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            /// <summary>
            /// WM_HOTKEY event constant
            /// </summary>
            private static int WM_HOTKEY = 0x0312;
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// Current id
        /// </summary>
        private int current_id_;

        /// <summary>
        /// Internal window
        /// </summary>
        private Window window_;
    }
}
