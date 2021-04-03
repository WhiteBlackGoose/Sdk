// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Elskom.Generic.Libs
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// A class that handles the settings for any application.
    /// </summary>
    public static class SettingsFile
    {
        /// <summary>
        /// Gets or sets the Json settings file instance.
        ///
        /// This is designed so there is globally only
        /// a single instance to save time, and memory.
        /// </summary>
        /// <value>
        /// The Json settings file instance.
        ///
        /// This is designed so there is globally only
        /// a single instance to save time, and memory.
        /// </value>
        public static JsonSettings SettingsJson { get; set; } = JsonSettings.OpenFile();

        /// <summary>
        /// Gets the path to the Application Settings file.
        ///
        /// Creates the folder if needed.
        /// </summary>
        /// <value>
        /// The path to the Application Settings file.
        ///
        /// Creates the folder if needed.
        /// </value>
        public static string SettingsPath
            => PrivatePathResolver(".json");

        /// <summary>
        /// Gets the path to the Application Error Log file.
        ///
        /// Creates the Error Log file if needed.
        /// </summary>
        public static string ErrorLogPath
            => PrivatePathResolver(".log");

        /// <summary>
        /// Gets the path to the Application Mini-Dump file.
        /// </summary>
        public static string MiniDumpPath
            => PrivatePathResolver(".mdmp");

        private static string PrivatePathResolver(string fileExtension)
        {
            // We cannot use System.Windows.Forms.Application.LocalUserAppDataPath as it would
            // Create annoying folders, and throw annoying Exceptions making it harder to
            // debug as it spams the debugger. Also then we would not need to Replace
            // everything added to the path obtained from System.Environment.GetFolderPath.
            var localPath = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);
            using var thisProcess = Process.GetCurrentProcess();
            localPath += $"{Path.DirectorySeparatorChar}{thisProcess.ProcessName}";
            if (!Directory.Exists(localPath))
            {
                _ = Directory.CreateDirectory(localPath);
            }

            if (fileExtension.Equals(".json", StringComparison.Ordinal))
            {
                // do not create the settings file, just pass this path to the json
                // interface for settings. if we create it ourselves the optimized
                // class will fail to work right if it is empty.
                localPath += $"{Path.DirectorySeparatorChar}Settings.json";
            }
            else if (fileExtension.Equals(".log", StringComparison.Ordinal))
            {
                localPath += $"{Path.DirectorySeparatorChar}{thisProcess.ProcessName}-{thisProcess.Id}.log";
            }
            else if (fileExtension.Equals(".mdmp", StringComparison.Ordinal))
            {
                localPath += $"{Path.DirectorySeparatorChar}{thisProcess.ProcessName}-{thisProcess.Id}.mdmp";
            }

            // trap devenv if it is detected.
#if NETSTANDARD2_1
            localPath = localPath.Replace("devenv", "Els_kom", StringComparison.OrdinalIgnoreCase);
#else
            localPath = localPath.Replace("devenv", "Els_kom");
#endif
            return localPath;
        }
    }
}
