using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;

namespace MefCalculator.Gui
{
    public class GenericMefPluginLoader<T>
    {
        [ImportMany]
        public Lazy<T>[] Plugins
        {
            get;
            set;
        }

        public GenericMefPluginLoader(string basePath)
        {
            var catalog = new AggregateCatalog();

            foreach (var path in Directory.EnumerateDirectories($@".\{basePath}", "*", SearchOption.TopDirectoryOnly))
            {
                var folderName = path.Remove(0, path.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                var dllName = $"{folderName}.dll";
                if (File.Exists(Path.Combine(path, dllName)))
                {
                    catalog.Catalogs.Add(new DirectoryCatalog(path, dllName));
                }
            }

            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}
