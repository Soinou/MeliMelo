using MeliMelo.Utils;
using System;

namespace MeliMelo.Core.Tray
{
    /// <summary>
    /// Represents a tray icon manager
    /// </summary>
    public interface ITrayIconManager
    {
        /// <summary>
        /// Gets the number of items in the tray icon menu
        /// </summary>
        int ItemCount
        {
            get;
        }

        /// <summary>
        /// Adds the given item to the tray icon menu
        /// </summary>
        /// <param name="item">Item name</param>
        void AddItem(string item);

        /// <summary>
        /// Adds a separator to the tray icon menu
        /// </summary>
        void AddSeparator();

        /// <summary>
        /// Hides the tray icon
        /// </summary>
        void Hide();

        /// <summary>
        /// Displays a notification on the screen using the tray icon
        /// </summary>
        /// <param name="title">Title of the notification</param>
        /// <param name="message">Message of the notification</param>
        void Notify(string title, string message);

        /// <summary>
        /// Shows the tray icon
        /// </summary>
        void Show();

        /// <summary>
        /// Triggered when the tray icon is double clicked
        /// </summary>
        event EventHandler DoubleClicked;

        /// <summary>
        /// Triggered when a menu item is clicked
        /// </summary>
        event EventHandler<DataEventArgs<string>> ItemClicked;

        /// <summary>
        /// Triggered when a notification is clicked
        /// </summary>
        event EventHandler NotificationClicked;
    }
}
