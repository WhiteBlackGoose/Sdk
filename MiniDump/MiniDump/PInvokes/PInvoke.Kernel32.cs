// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Microsoft.Windows.Sdk
{
    using global::System.Runtime.InteropServices;

    /// <content>
    /// Contains extern methods from "Kernel32.dll".
    /// </content>
    internal static partial class PInvoke
    {
        /// <summary>Closes an open object handle.</summary>
        /// <param name = "hObject">A valid handle to an open object.</param>
        /// <returns>
        /// <para>If the function succeeds, the return value is nonzero.</para>
        /// <para>If the function fails, the return value is zero. To get extended error information, call <a href = "/windows/desktop/api/errhandlingapi/nf-errhandlingapi-getlasterror">GetLastError</a>.</para>
        /// <para>If the application is running under a debugger,  the function will throw an exception if it receives either a  handle value that is not valid  or a pseudo-handle value. This can happen if you close a handle twice, or if you  call <b>CloseHandle</b> on a handle returned by the <a href = "/windows/desktop/api/fileapi/nf-fileapi-findfirstfilea">FindFirstFile</a> function instead of calling the <a href = "/windows/desktop/api/fileapi/nf-fileapi-findclose">FindClose</a> function.</para>
        /// </returns>
        /// <remarks>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//handleapi/nf-handleapi-closehandle">Learn more about this API from docs.microsoft.com</see>.</para>
        /// </remarks>
        [DllImport("Kernel32", ExactSpelling = true, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern BOOL CloseHandle(HANDLE hObject);

        /// <inheritdoc cref = "GetCurrentProcess()"/>
        internal static unsafe Win32.SafeHandles.SafeFileHandle GetCurrentProcess_SafeHandle()
            => new(GetCurrentProcess(), ownsHandle: true);

        /// <summary>Retrieves a pseudo handle for the current process.</summary>
        /// <returns>The return value is a pseudo handle to the current process.</returns>
        /// <remarks>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//processthreadsapi/nf-processthreadsapi-getcurrentprocess">Learn more about this API from docs.microsoft.com</see>.</para>
        /// </remarks>
        [DllImport("Kernel32", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern HANDLE GetCurrentProcess();

        /// <summary>Retrieves the process identifier of the calling process.</summary>
        /// <returns>The return value is the process identifier of the calling process.</returns>
        /// <remarks>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//processthreadsapi/nf-processthreadsapi-getcurrentprocessid">Learn more about this API from docs.microsoft.com</see>.</para>
        /// </remarks>
        [DllImport("Kernel32", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint GetCurrentProcessId();

        /// <summary>Retrieves the thread identifier of the calling thread.</summary>
        /// <returns>The return value is the thread identifier of the calling thread.</returns>
        /// <remarks>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//processthreadsapi/nf-processthreadsapi-getcurrentthreadid">Learn more about this API from docs.microsoft.com</see>.</para>
        /// </remarks>
        [DllImport("Kernel32", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint GetCurrentThreadId();
    }
}
