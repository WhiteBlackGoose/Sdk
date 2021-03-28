// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Elskom.Generic.Libs
{
    /// <summary>
    /// Event that holds that data to call the apis for notifications.
    /// </summary>
    public class NotificationEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationEventArgs"/> class.
        /// </summary>
        /// <param name="timeout">The notification timeout.</param>
        /// <param name="title">The notification title.</param>
        /// <param name="text">The notification text.</param>
        /// <param name="icon">The notification icon.</param>
        public NotificationEventArgs(int timeout, string title, string text, ToolTipIcon icon)
        {
            this.TimeOut = timeout;
            this.Title = title;
            this.Text = text;
            this.Icon = icon;
        }

        /// <summary>
        /// Gets the timeout to use for the Notification.
        /// </summary>
        public int TimeOut { get; private set; }

        /// <summary>
        /// Gets the title of the Notification.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the text of the Notification.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the icon to use for the Notification.
        /// </summary>
        public ToolTipIcon Icon { get; private set; }
    }
}
