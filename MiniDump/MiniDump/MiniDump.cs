// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Elskom.Generic.Libs
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using Microsoft.Windows.Sdk;

    internal static class MiniDump
    {
        internal static void ExceptionEventHandlerCode(Exception e, bool threadException)
        {
            var exceptionData = PrintExceptions(e);

            // do not dump or close if in a debugger.
            if (!Debugger.IsAttached)
            {
                MiniDumpAttribute.ForceClose = true;

                // if this is not Windows, MiniDumpToFile(), which calls MiniDumpWriteDump() in dbghelp.dll
                // cannot be used as it does not exist. I need to figure out mini-dumping for
                // these platforms manually. In that case I guess it does not matter much anyway
                // with the world of debugging and running in a IDE.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    if (string.IsNullOrEmpty(MiniDumpAttribute.CurrentInstance.DumpLogFileName))
                    {
                        MiniDumpAttribute.CurrentInstance.DumpLogFileName = SettingsFile.ErrorLogPath;
                    }

                    if (string.IsNullOrEmpty(MiniDumpAttribute.CurrentInstance.DumpFileName))
                    {
                        MiniDumpAttribute.CurrentInstance.DumpFileName = SettingsFile.MiniDumpPath;
                    }

                    File.WriteAllText(MiniDumpAttribute.CurrentInstance.DumpLogFileName, exceptionData);
                    MiniDumpToFile(MiniDumpAttribute.CurrentInstance.DumpFileName, MiniDumpAttribute.CurrentInstance.DumpType);
                    MiniDumpAttribute.InvokeDumpMessage(typeof(MiniDump), new MessageEventArgs(string.Format(CultureInfo.InvariantCulture, MiniDumpAttribute.CurrentInstance.Text, MiniDumpAttribute.CurrentInstance.DumpLogFileName), threadException ? MiniDumpAttribute.CurrentInstance.ThreadExceptionTitle : MiniDumpAttribute.CurrentInstance.ExceptionTitle, ErrorLevel.Error));
                }
            }
        }

        private static unsafe void MiniDumpToFile(string fileToDump, MINIDUMP_TYPE dumpType)
        {
            using var fsToDump = File.Open(fileToDump, FileMode.Create, FileAccess.ReadWrite, FileShare.Write);
            var mINIDUMP_EXCEPTION_INFORMATION = new MINIDUMP_EXCEPTION_INFORMATION
            {
                ClientPointers = false,
                ExceptionPointers = GetExceptionPointers(),
                ThreadId = PInvoke.GetCurrentThreadId(),
            };
            var error = MiniDumpWriteDump(
                PInvoke.GetCurrentProcess_SafeHandle(),
                PInvoke.GetCurrentProcessId(),
                fsToDump.SafeFileHandle,
                dumpType,
                mINIDUMP_EXCEPTION_INFORMATION,
                default,
                default);
            if (error > 0)
            {
                MiniDumpAttribute.InvokeDumpMessage(typeof(MiniDump), new MessageEventArgs($"Mini-dumping failed with Code: {error}", "Error!", ErrorLevel.Error));
            }
        }

        private static IntPtr GetExceptionPointers()
        {
            // because we target .NET Standard 2.0 we need to probe for
            // Marshal.GetExceptionPointers using reflection then call
            // it if it exists but return a null pointer if it does not exist.
            var method = typeof(Marshal).GetMethod(
                "GetExceptionPointers",
                BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly,
                null,
                Type.EmptyTypes,
                null);
            return method != null ? (IntPtr)method.Invoke(null, null) : IntPtr.Zero;
        }

        private static unsafe int MiniDumpWriteDump(SafeHandle hProcess, uint processId, SafeHandle hFile, MINIDUMP_TYPE dumpType, MINIDUMP_EXCEPTION_INFORMATION? exceptionParam, IntPtr userStreamParam, IntPtr callackParam)
        {
            _ = PInvoke.MiniDumpWriteDump(hProcess, processId, hFile, dumpType, exceptionParam, userStreamParam, callackParam);
            return Marshal.GetLastWin32Error();
        }

        private static string PrintExceptions(Exception exception)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{exception.GetType()}: {exception.Message}{Environment.NewLine}{exception.StackTrace}");
            var currException = exception.InnerException;
            while (currException != null)
            {
                sb.AppendLine($"{currException.GetType()}: {currException.Message}{Environment.NewLine}{currException.StackTrace}");
                currException = currException.InnerException;
            }

            return sb.ToString();
        }
    }
}
