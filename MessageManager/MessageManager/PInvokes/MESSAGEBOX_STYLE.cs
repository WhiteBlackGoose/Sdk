// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Microsoft.Windows.Sdk
{
    using global::System;

    [Flags]
    internal enum MESSAGEBOX_STYLE
    {
        MB_OK = 0,
        MB_ABORTRETRYIGNORE = 2,
        MB_CANCELTRYCONTINUE = 6,
        MB_HELP = 16384,
        MB_OKCANCEL = 1,
        MB_RETRYCANCEL = 5,
        MB_YESNO = 4,
        MB_YESNOCANCEL = 3,
        MB_ICONHAND = 16,
        MB_ICONQUESTION = 32,
        MB_ICONEXCLAMATION = 48,
        MB_ICONASTERISK = 64,
        MB_USERICON = 128,
        MB_ICONWARNING = MB_ICONEXCLAMATION,
        MB_ICONERROR = MB_ICONHAND,
        MB_ICONINFORMATION = MB_ICONASTERISK,
        MB_ICONSTOP = MB_ICONHAND,
        MB_DEFBUTTON1 = MB_OK,
        MB_DEFBUTTON2 = 256,
        MB_DEFBUTTON3 = 512,
        MB_DEFBUTTON4 = 768,
        MB_APPLMODAL = MB_OK,
        MB_SYSTEMMODAL = 4096,
        MB_TASKMODAL = 8192,
        MB_NOFOCUS = 32768,
        MB_SETFOREGROUND = 65536,
        MB_DEFAULT_DESKTOP_ONLY = 131072,
        MB_TOPMOST = 262144,
        MB_RIGHT = 524288,
        MB_RTLREADING = 1048576,
        MB_SERVICE_NOTIFICATION = 2097152,
        MB_SERVICE_NOTIFICATION_NT3X = MB_TOPMOST,
        MB_TYPEMASK = 15,
        MB_ICONMASK = 240,
        MB_DEFMASK = 3840,
        MB_MODEMASK = 12288,
        MB_MISCMASK = 49152,
    }
}
