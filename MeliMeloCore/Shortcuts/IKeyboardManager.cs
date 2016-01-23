using System;
using System.Windows.Forms;

namespace MeliMelo.Core.Shortcuts
{
    /// <summary>
    /// Represents a keyboard manager
    /// </summary>
    public interface IKeyboardManager
    {
        /// <summary>
        /// Registers a keyboard hotkey
        /// </summary>
        /// <param name="modifier">Modifiers</param>
        /// <param name="key">Key</param>
        /// <returns>Id of the registered hotkey (Used for Unregister(int))</returns>
        int Register(Modifiers modifier, Keys key);

        /// <summary>
        /// Unregisters a keyboard hotkey
        /// </summary>
        /// <param name="id">Id of the hotkey</param>
        void Unregister(int id);

        /// <summary>
        /// Triggered when a key is pressed
        /// </summary>
        event EventHandler<KeyPressedEventArgs> KeyPressed;
    }
}
