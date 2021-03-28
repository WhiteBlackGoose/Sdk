// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

#define ENUM_SYNC
#if !ENUM_SYNC
namespace Elskom.Generic.Libs
{
    using System;

    /// <summary>Identifies the type of information that will be written to the minidump file by the MiniDumpWriteDump function.</summary>
    /// <remarks>
    /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    [Flags]
    public enum MINIDUMP_TYPE
    {
        /// <summary>Include just the information necessary to capture stack traces for all existing threads in a process.</summary>
        MiniDumpNormal = 0x00000000,

        /// <summary>
        /// <para>Include the data sections from all loaded modules. This results in the inclusion of global variables, which can make the minidump file significantly larger. For per-module control, use the <b>ModuleWriteDataSeg</b> enumeration value from <a href = "https://docs.microsoft.com/windows/desktop/api/minidumpapiset/ne-minidumpapiset-module_write_flags">MODULE_WRITE_FLAGS</a>.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithDataSegs = 0x00000001,

        /// <summary>
        /// <para>Include all accessible memory in the process. The raw memory data is included at the end, so that the initial structures can be mapped directly without the raw memory information. This option can result in a very large file.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithFullMemory = 0x00000002,

        /// <summary>
        /// <para>Include high-level information about the operating system handles that are active when the minidump is made.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithHandleData = 0x00000004,

        /// <summary>
        /// <para>Stack and backing store memory written to the minidump file should be filtered to remove all but the pointer values necessary to reconstruct a stack trace.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpFilterMemory = 0x00000008,

        /// <summary>
        /// <para>Stack and backing store memory should be scanned for pointer references to modules in the module list. If a module is referenced by stack or backing store memory, the <b>ModuleWriteFlags</b> member of the <a href = "https://docs.microsoft.com/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_callback_output">MINIDUMP_CALLBACK_OUTPUT</a> structure is set to <b>ModuleReferencedByMemory</b>.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpScanMemory = 0x00000010,

        /// <summary>
        /// <para>Include information from the list of modules that were recently unloaded, if this information is maintained by the operating system.</para>
        /// <para><b>Windows Server 2003 and Windows XP:  </b>The operating system does not maintain information for unloaded modules until Windows Server 2003 with SP1 and Windows XP with SP2. <b>DbgHelp 5.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithUnloadedModules = 0x00000020,

        /// <summary>
        /// <para>Include pages with data referenced by locals or other stack memory. This option can increase the size of the minidump file significantly.</para>
        /// <para><b>DbgHelp 5.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithIndirectlyReferencedMemory = 0x00000040,

        /// <summary>
        /// <para>Filter module paths for information such as user names or important directories. This option may prevent the system from locating the image file and should be used only in special situations.</para>
        /// <para><b>DbgHelp 5.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpFilterModulePaths = 0x00000080,

        /// <summary>
        /// <para>Include complete per-process and per-thread information from the operating system.</para>
        /// <para><b>DbgHelp 5.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithProcessThreadData = 0x00000100,

        /// <summary>
        /// <para>Scan the virtual address space for <b>PAGE_READWRITE</b> memory to be included.</para>
        /// <para><b>DbgHelp 5.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithPrivateReadWriteMemory = 0x00000200,

        /// <summary>
        /// <para>Reduce the data that is dumped by eliminating memory regions that are not essential to meet criteria specified for the dump. This can avoid dumping  memory that may contain data that is private to the user. However, it is not a guarantee that no private information will be present.</para>
        /// <para><b>DbgHelp 6.1 and earlier:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithoutOptionalData = 0x00000400,

        /// <summary>
        /// <para>Include memory region information. For more information, see <a href = "https://docs.microsoft.com/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_memory_info_list">MINIDUMP_MEMORY_INFO_LIST</a>.</para>
        /// <para><b>DbgHelp 6.1 and earlier:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithFullMemoryInfo = 0x00000800,

        /// <summary>
        /// <para>Include thread state information. For more information, see <a href = "https://docs.microsoft.com/windows/win32/api/minidumpapiset/ns-minidumpapiset-minidump_thread_info_list">MINIDUMP_THREAD_INFO_LIST</a>.</para>
        /// <para><b>DbgHelp 6.1 and earlier:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithThreadInfo = 0x00001000,

        /// <summary>
        /// <para>Include all code and code-related sections from loaded modules to capture executable content. For per-module control, use the <b>ModuleWriteCodeSegs</b> enumeration value from <a href = "https://docs.microsoft.com/windows/desktop/api/minidumpapiset/ne-minidumpapiset-module_write_flags">MODULE_WRITE_FLAGS</a>.</para>
        /// <para><b>DbgHelp 6.1 and earlier:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithCodeSegs = 0x00002000,

        /// <summary>Turns off secondary auxiliary-supported memory gathering.</summary>
        MiniDumpWithoutAuxiliaryState = 0x00004000,

        /// <summary>
        /// <para>Requests that auxiliary data providers include their state in the dump image; the state data that is included is provider dependent. This option can result in a large dump image.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithFullAuxiliaryState = 0x00008000,

        /// <summary>
        /// <para>Scans the virtual address space for <b>PAGE_WRITECOPY</b> memory to be included.</para>
        /// <para><b>Prior to DbgHelp 6.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithPrivateWriteCopyMemory = 0x00010000,

        /// <summary>
        /// <para>If you specify <b>MiniDumpWithFullMemory</b>, the <a href = "https://docs.microsoft.com/windows/desktop/api/minidumpapiset/nf-minidumpapiset-minidumpwritedump">MiniDumpWriteDump</a> function will fail if the function cannot read the memory regions; however, if you include <b>MiniDumpIgnoreInaccessibleMemory</b>, the <b>MiniDumpWriteDump</b> function will ignore the memory read failures and continue to generate the dump. Note that the inaccessible memory regions are not included in the dump.</para>
        /// <para><b>Prior to DbgHelp 6.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpIgnoreInaccessibleMemory = 0x00020000,

        /// <summary>
        /// <para>Adds security token related data. This will make the "!token" extension work when processing a user-mode dump.</para>
        /// <para><b>Prior to DbgHelp 6.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithTokenInformation = 0x00040000,

        /// <summary>
        /// <para>Adds module header related data.</para>
        /// <para><b>Prior to DbgHelp 6.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithModuleHeaders = 0x00080000,

        /// <summary>
        /// <para>Adds filter triage related data.</para>
        /// <para><b>Prior to DbgHelp 6.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpFilterTriage = 0x00100000,

        /// <summary>
        /// <para>Adds AVX crash state context registers.</para>
        /// <para><b>Prior to DbgHelp 6.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithAvxXStateContext = 0x00200000,

        /// <summary>
        /// <para>Adds Intel Processor Trace related data.</para>
        /// <para><b>Prior to DbgHelp 6.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpWithIptTrace = 0x00400000,

        /// <summary>
        /// <para>Scans inaccessible partial memory pages.</para>
        /// <para><b>Prior to DbgHelp 6.1:  </b>This value is not supported.</para>
        /// <para><see href = "https://docs.microsoft.com/windows/win32/api//minidumpapiset/ne-minidumpapiset-minidump_type#members">Read more on docs.microsoft.com</see>.</para>
        /// </summary>
        MiniDumpScanInaccessiblePartialPages = 0x00800000,

        /// <summary>Indicates which flags are valid.</summary>
        MiniDumpValidTypeFlags = 0x00FFFFFF,
    }
}
#endif
