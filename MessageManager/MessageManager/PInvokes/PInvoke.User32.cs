// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Microsoft.Windows.Sdk
{
    using System;
    using global::System.Runtime.InteropServices;

    /// <content>
    /// Contains extern methods from "User32.dll".
    /// </content>
    internal static partial class PInvoke
    {
        /// <summary>Displays a modal dialog box that contains a system icon, a set of buttons, and a brief application-specific message, such as status or error information. The message box returns an integer value that indicates which button the user clicked.</summary>
        /// <param name = "hWnd">
        /// <para>Type: <b>HWND</b></para>
        /// <para>A handle to the owner window of the message box to be created. If this parameter is <b>NULL</b>, the message box has no owner window.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//winuser/nf-winuser-messageboxw#parameters">Read more on docs.microsoft.com</see>.</para>
        /// </param>
        /// <param name = "lpText">
        /// <para>Type: <b>LPCTSTR</b></para>
        /// <para>The message to be displayed. If the string consists of more than one line, you can separate the lines using a carriage return and/or linefeed character between each line.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//winuser/nf-winuser-messageboxw#parameters">Read more on docs.microsoft.com</see>.</para>
        /// </param>
        /// <param name = "lpCaption">
        /// <para>Type: <b>LPCTSTR</b></para>
        /// <para>The dialog box title. If this parameter is <b>NULL</b>, the default title is <b>Error</b>.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//winuser/nf-winuser-messageboxw#parameters">Read more on docs.microsoft.com</see>.</para>
        /// </param>
        /// <param name = "uType">
        /// <para>Type: <b>UINT</b></para>
        /// <para>The contents and behavior of the dialog box. This parameter can be a combination of flags from the following groups of flags.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//winuser/nf-winuser-messageboxw#parameters">Read more on docs.microsoft.com</see>.</para>
        /// </param>
        /// <returns>
        /// <para>Type: <b>int</b></para>
        /// <para>If a message box has a <b>Cancel</b> button, the function returns the <b>IDCANCEL</b> value if either the ESC key is pressed or the <b>Cancel</b> button is selected. If the message box has no <b>Cancel</b> button, pressing ESC will no effect - unless an MB_OK button is present. If an MB_OK button is displayed and the user presses ESC, the return value will be <b>IDOK</b>.</para>
        /// <para>If the function fails, the return value is zero. To get extended error information, call <a href = "/windows/desktop/api/errhandlingapi/nf-errhandlingapi-getlasterror">GetLastError</a>.</para>
        /// <para>If the function succeeds, the return value is one of the following menu-item values.</para>
        /// <para></para>
        /// <para>This doc was truncated.</para>
        /// </returns>
        /// <remarks>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//winuser/nf-winuser-messageboxw">Learn more about this API from docs.microsoft.com</see>.</para>
        /// </remarks>
        [DllImport("User32", ExactSpelling = true, EntryPoint = "MessageBoxW", SetLastError = true, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, MESSAGEBOX_STYLE uType);
    }
}
