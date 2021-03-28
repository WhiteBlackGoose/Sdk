// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Elskom.Generic.Libs
{
    /// <summary>
    /// Specifies identifiers to indicate the return value of a dialog box.
    /// </summary>
    public enum DialogResult
    {
        /// <summary>
        /// Nothing is returned from the dialog box. This means that the modal dialog is still running.
        /// </summary>
        None,

        /// <summary>
        /// The dialog box return value is OK (usually sent from a button labeled OK).
        /// </summary>
        OK,

        /// <summary>
        /// The dialog box return value is Cancel (usually sent from a button labeled Cancel).
        /// </summary>
        Cancel,

        /// <summary>
        /// The dialog box return value is Abort (usually sent from a button labeled Abort).
        /// </summary>
        Abort,

        /// <summary>
        /// The dialog box return value is Retry (usually sent from a button labeled Retry).
        /// </summary>
        Retry,

        /// <summary>
        /// The dialog box return value is Ignore (usually sent from a button labeled Ignore).
        /// </summary>
        Ignore,

        /// <summary>
        /// The dialog box return value is Yes (usually sent from a button labeled Yes).
        /// </summary>
        Yes,

        /// <summary>
        ///  The dialog box return value is No (usually sent from a button labeled No).
        /// </summary>
        No,

        /// <summary>
        /// The dialog box return value is Close (usually sent from a button labeled as x).
        /// </summary>
        Close,

        /// <summary>
        /// The dialog box return value is Help (usually sent from a button labeled as ?).
        /// </summary>
        Help,

        /// <summary>
        /// The dialog box return value is Try Again (usually sent from a button labeled Try Again).
        /// </summary>
        TryAgain,

        /// <summary>
        /// The dialog box return value is Continue (usually sent from a button labeled Continue).
        /// </summary>
        Continue,

        /// <summary>
        /// The dialog box return value is Timeout.
        /// </summary>
        Timeout = 32000,
    }
}
