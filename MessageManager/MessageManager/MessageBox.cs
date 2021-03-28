// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Elskom.Generic.Libs
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Windows.Sdk;

    internal static class MessageBox
    {
        internal static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            => ShowCore(text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0);

        private static DialogResult ShowCore(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            DialogResult result;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var style = (MESSAGEBOX_STYLE)buttons | (MESSAGEBOX_STYLE)icon | (MESSAGEBOX_STYLE)defaultButton | (MESSAGEBOX_STYLE)options;
                var box = PInvoke.MessageBox(IntPtr.Zero, text, caption, style);
                result = box switch
                {
                    8 => buttons is MessageBoxButtons.CancelTryContinue or MessageBoxButtons.OKCancel or MessageBoxButtons.RetryCancel or MessageBoxButtons.YesNoCancel ? DialogResult.Cancel : DialogResult.No,
                    9 or 32000 or 32001 => DialogResult.No,
                    _ => (DialogResult)box,
                };
            }
            else
            {
                // for now show nothing on Non-Windows Systems.
                result = DialogResult.No;
            }

            return result;
        }
    }
}
