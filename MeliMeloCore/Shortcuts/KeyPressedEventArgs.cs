using System;
using System.Windows.Forms;

namespace MeliMelo.Core.Shortcuts
{
    /// <summary>
    /// Event Args for the event that is fired after the hot key has been pressed.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new KeyPressedEventArgs
        /// </summary>
        /// <param name="modifier">Pressed modifier(s)</param>
        /// <param name="key">Pressed key</param>
        public KeyPressedEventArgs(Modifiers modifier, Keys key)
        {
            modifier_ = modifier;
            key_ = key;
        }

        /// <summary>
        /// Gets the pressed key
        /// </summary>
        public Keys Key
        {
            get { return key_; }
        }

        /// <summary>
        /// Gets the pressed modifier(s)
        /// </summary>
        public Modifiers Modifier
        {
            get { return modifier_; }
        }

        /// <summary>
        /// Pressed key
        /// </summary>
        private Keys key_;

        /// <summary>
        /// Pressed modifier(s)
        /// </summary>
        private Modifiers modifier_;
    }
}
