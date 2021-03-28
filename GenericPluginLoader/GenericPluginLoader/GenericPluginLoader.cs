// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

/*
 Credits to this file goes to the C# plugin
 loader & interface examples.
*/
namespace Elskom.Generic.Libs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// A generic loader for plugins.
    /// </summary>
    /// <typeparam name="T">The type to look for when loading plugins.</typeparam>
    public class GenericPluginLoader<T>
    {
        /// <summary>
        /// Triggers when the Plugin Loader has a message to send to the application.
        /// </summary>
        public static event EventHandler<MessageEventArgs> PluginLoaderMessage;

#if NET5_0_OR_GREATER
        /// <summary>
        /// Gets the list of <see cref="PluginLoadContext"/>s loaded by this instance.
        /// </summary>
        public List<PluginLoadContext> Contexts { get; private set; } = new List<PluginLoadContext>();
#else
        /// <summary>
        /// Gets the list of <see cref="AppDomain"/>s loaded by this instance.
        /// </summary>
        public List<AppDomain> Domains { get; private set; } = new List<AppDomain>();
#endif

#if NET5_0_OR_GREATER
        /// <summary>
        /// Loads plugins with the specified plugin interface type.
        /// </summary>
        /// <param name="path">
        /// The path to look for plugins to load.
        /// </param>
        /// <param name="saveToZip">
        /// Tells this function to see if the plugin was saved to a zip file and it's pdb file as well.
        /// </param>
        /// <returns>
        /// A list of plugins loaded that derive from the specified type.
        /// </returns>
        public ICollection<T> LoadPlugins(string path, bool saveToZip = false)
#else
        /// <summary>
        /// Loads plugins with the specified plugin interface type.
        /// </summary>
        /// <param name="path">
        /// The path to look for plugins to load.
        /// </param>
        /// <param name="saveToZip">
        /// Tells this function to see if the plugin was saved to a zip file and it's pdb file as well.
        /// </param>
        /// <returns>
        /// A list of plugins loaded that derive from the specified type.
        /// </returns>
        public ICollection<T> LoadPlugins(string path, bool saveToZip = false)
#endif
        {
            string[] dllFileNames = null;
            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");
            }

            // try to load from a zip as well if plugins are installed in both places.
            var zippath = $"{path}.zip";
            ICollection<T> plugins = new List<T>();

            // handle when path points to a zip file.
            if (Directory.Exists(path) || File.Exists(zippath))
            {
                ICollection<Assembly> assemblies = new List<Assembly>();
                if (dllFileNames != null)
                {
                    foreach (var dllFile in dllFileNames)
                    {
#if NET5_0_OR_GREATER
                        var context = new PluginLoadContext($"Plugin#{dllFileNames.ToList().IndexOf(dllFile)}", path);
#else
                        var domain = AppDomain.CreateDomain($"Plugin#{dllFileNames.ToList().IndexOf(dllFile)}");
                        domain.AssemblyResolve += this.Domain_AssemblyResolve;
#endif
                        try
                        {
                            var asmFile = File.ReadAllBytes(dllFile);

                            // We need to handle the case where the pdb does not exist and where the
                            // symbols might actually be embedded inside the dll instead or simply does
                            // not exist yet.
#if NET5_0_OR_GREATER
                            var pdbFile = Debugger.IsAttached && File.Exists(dllFile.Replace("dll", "pdb", StringComparison.Ordinal))
                                ? File.ReadAllBytes(
                                    dllFile.Replace("dll", "pdb", StringComparison.Ordinal)) : null;
                            using var ms1 = new MemoryStream(asmFile);
                            using var ms2 = new MemoryStream(pdbFile);
                            var assembly = Debugger.IsAttached && pdbFile != null ?
                                context.LoadFromStream(ms1, ms2) :
                                context.LoadFromStream(ms1);
                            this.Contexts.Add(context);
#else
                            var pdbFile = Debugger.IsAttached && File.Exists(dllFile.Replace("dll", "pdb"))
                                ? File.ReadAllBytes(dllFile.Replace("dll", "pdb")) : null;
                            var assembly = Debugger.IsAttached && pdbFile != null ?
                                domain.Load(asmFile, pdbFile) :
                                domain.Load(asmFile);
                            this.Domains.Add(domain);
#endif
                            assemblies.Add(assembly);
                        }
                        catch (BadImageFormatException)
                        {
                            // ignore the error and load the other files.
#if NET5_0_OR_GREATER
                            context.Unload();
#else
                            AppDomain.Unload(domain);
#endif
                        }
                        catch (FileLoadException)
                        {
#if NET5_0_OR_GREATER
                            var assembly = context.LoadFromAssemblyPath(dllFile);
                            this.Contexts.Add(context);
                            assemblies.Add(assembly);
#else
                            try
                            {
                                var assembly = domain.Load(AssemblyName.GetAssemblyName(dllFile));
                                this.Domains.Add(domain);
                                assemblies.Add(assembly);
                            }
                            catch (Exception)
                            {
                                // ignore the error and load the other files.
                                AppDomain.Unload(domain);
                            }
#endif
                        }
                        catch (FileNotFoundException)
                        {
                            // ignore the error and load other files.
#if NET5_0_OR_GREATER
                            context.Unload();
#else
                            AppDomain.Unload(domain);
#endif
                        }
                    }
                }

                if (saveToZip && File.Exists(zippath))
                {
                    var filesInZip = new Dictionary<string, int>();
                    using (var zipFile = ZipFile.OpenRead(zippath))
                    {
                        foreach (var entry in zipFile.Entries)
                        {
                            filesInZip.Add(entry.FullName, zipFile.Entries.IndexOf(entry));
                        }
                    }

                    foreach (var entry in filesInZip.Keys)
                    {
                        // just lookup the dlls here. The LoadFromZip method will load the pdb’s if they are deemed needed.
                        if (entry.EndsWith(".dll", StringComparison.Ordinal))
                        {
#if NET5_0_OR_GREATER
                            var context = new PluginLoadContext($"ZipPlugin#{filesInZip[entry]}", path);
                            var assembly = ZipAssembly.LoadFromZip(zippath, entry, context);
                            this.Contexts.Add(context);
#else
                            var domain = AppDomain.CreateDomain($"ZipPlugin#{filesInZip[entry]}");
                            var assembly = ZipAssembly.LoadFromZip(zippath, entry, domain);
                            this.Domains.Add(domain);
#endif
                            if (assembly != null)
                            {
                                assemblies.Add(assembly);
                            }
                        }
                    }
                }

                var pluginType = typeof(T);
                var pluginTypes = new List<Type>();
                foreach (var assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        try
                        {
                            var types = assembly.GetTypes();
                            foreach (var type in types)
                            {
                                if (!type.IsInterface && !type.IsAbstract && type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                        catch (ReflectionTypeLoadException ex)
                        {
                            var exMsg = new StringBuilder();
                            foreach (var exceptions in ex.LoaderExceptions)
                            {
                                exMsg.Append($"{ex.GetType()}: {exceptions.Message}{Environment.NewLine}{exceptions.StackTrace}{Environment.NewLine}");
                            }

                            PluginLoaderMessage?.Invoke(null, new MessageEventArgs(exMsg.ToString(), "Error!", ErrorLevel.Error));
                        }
                        catch (ArgumentNullException ex)
                        {
                            var exMsg = LogException(ex);
                            PluginLoaderMessage?.Invoke(null, new MessageEventArgs(exMsg, "Error!", ErrorLevel.Error));
                        }
                    }
                }

                ICollection<int> toRemove = new List<int>();
                foreach (var type in pluginTypes)
                {
                    try
                    {
                        var plugin = (T)Activator.CreateInstance(type);
                        plugins.Add(plugin);
                    }
                    catch (MissingMethodException)
                    {
                        toRemove.Add(pluginTypes.IndexOf(type));
                        var index = 0;
#if NET5_0_OR_GREATER
                        foreach (var context in this.Contexts)
                        {
                            if (context.Assemblies.Contains(type.Assembly) && context.IsCollectible)
                            {
                                index = this.Contexts.IndexOf(context);
                                context.Unload();
                            }
                        }
#else
                        foreach (var domain in this.Domains)
                        {
                            if (domain.GetAssemblies().ToList().Contains(type.Assembly))
                            {
                                index = this.Domains.IndexOf(domain);
                                AppDomain.Unload(domain);
                            }
                        }
#endif

                        if (index > -1)
                        {
#if NET5_0_OR_GREATER
                            this.Contexts.RemoveAt(index);
#else
                            this.Domains.RemoveAt(index);
#endif
                        }
                    }
                }

                foreach (var indexToRemove in toRemove)
                {
                    if (indexToRemove > -1)
                    {
                        pluginTypes.RemoveAt(indexToRemove);
                    }
                }

                toRemove.Clear();
                return plugins;
            }

            return plugins;
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Unloads all of the loaded plugins.
        /// </summary>
        public void UnloadPlugins()
        {
            foreach (var context in this.Contexts)
            {
                if (context.IsCollectible)
                {
                    context.Unload();
                }
            }

            this.Contexts.Clear();
        }
#else
        /// <summary>
        /// Unloads all of the loaded plugins.
        /// </summary>
        public void UnloadPlugins()
        {
            foreach (var domain in this.Domains)
            {
                AppDomain.Unload(domain);
            }

            this.Domains.Clear();
        }
#endif

        private static string LogException(Exception ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{ex.GetType()}: {ex.Message}{Environment.NewLine}{ex.StackTrace}");
            var currException = ex.InnerException;
            while (currException != null)
            {
                sb.AppendLine($"{currException.GetType()}: {currException.Message}{Environment.NewLine}{currException.StackTrace}");
                currException = currException.InnerException;
            }

            return sb.ToString();
        }

#if !NET5_0_OR_GREATER
        private static bool IsLoadedToDefaultDomain(string name)
        {
            var found = false;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.Equals(name, StringComparison.Ordinal))
                {
                    found = true;
                }
            }

            return AppDomain.CurrentDomain.IsDefaultAppDomain() && found;
        }

        private static Assembly GetFromDefaultDomain(string name)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.Equals(name, StringComparison.Ordinal))
                {
                    return assembly;
                }
            }

            return null;
        }

        private Assembly Domain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (IsLoadedToDefaultDomain(args.Name))
            {
                return GetFromDefaultDomain(args.Name);
            }

            var assemblyName = new AssemblyName(args.Name);
            var domain = this.GetDomainFromAssembly(args.RequestingAssembly);
            return !File.Exists($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}{assemblyName.Name}.dll")
                ? null
                : domain.Load(AssemblyName.GetAssemblyName($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}{assemblyName.Name}.dll"));
        }

        private AppDomain GetDomainFromAssembly(Assembly assembly)
        {
            if (assembly != null)
            {
                foreach (var domain in this.Domains)
                {
                    if (domain.GetAssemblies().Contains(assembly))
                    {
                        return domain;
                    }
                }
            }

            return AppDomain.CurrentDomain;
        }
#endif
    }
}
