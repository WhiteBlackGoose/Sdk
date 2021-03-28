// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

#if NET5_0_OR_GREATER
namespace Elskom.Generic.Libs
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Loader;

    /// <inheritdoc/>
    public class PluginLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginLoadContext"/> class.
        /// </summary>
        /// <param name="name">The name of the load context.</param>
        /// <param name="pluginPath">The path the the plugins.</param>
        public PluginLoadContext(string name, string pluginPath)
            : base(name, true)
            => this.resolver = new AssemblyDependencyResolver(pluginPath);

        /// <inheritdoc/>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            if (IsLoadedToDefaultContext(assemblyName.FullName))
            {
                // return the assembly from the default context instead of reloading it (is same assembly and version).
                return GetFromDefaultContext(assemblyName.FullName);
            }

            var assemblyPath = this.resolver.ResolveAssemblyToPath(assemblyName);
            return assemblyPath != null
                ? this.LoadFromAssemblyPath(assemblyPath)
                : !File.Exists($"{AppContext.BaseDirectory}{assemblyName.Name}.dll")
                ? null
                : this.LoadFromAssemblyPath($"{AppContext.BaseDirectory}{assemblyName.Name}.dll");
        }

        /// <inheritdoc/>
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            var libraryPath = this.resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            return libraryPath != null
                ? this.LoadUnmanagedDllFromPath(libraryPath)
                : !File.Exists($"{AppContext.BaseDirectory}{unmanagedDllName}.dll")
                ? IntPtr.Zero
                : this.LoadUnmanagedDllFromPath($"{AppContext.BaseDirectory}{unmanagedDllName}.dll");
        }

        private static bool IsLoadedToDefaultContext(string name)
        {
            foreach (var assembly in Default.Assemblies)
            {
                if (assembly.FullName.Equals(name, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        private static Assembly GetFromDefaultContext(string name)
        {
            foreach (var assembly in Default.Assemblies)
            {
                if (assembly.FullName.Equals(name, StringComparison.Ordinal))
                {
                    return assembly;
                }
            }

            return null;
        }
    }
}
#endif
