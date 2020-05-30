using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Jacobi.Vst3.Core.Common
{
    public sealed class AssemblyLoader : IDisposable
    {
        private readonly string _basePath;
        private readonly AssemblyLoadContext _context;

        public AssemblyLoader(string basePath)
        {
            _basePath = basePath;

            _context = AssemblyLoadContext.GetLoadContext(this.GetType().Assembly);
            _context.Resolving += LoadContext_ResolvingAssembly;
        }

        public void Dispose()
        {
            _context.Resolving -= LoadContext_ResolvingAssembly;
        }

        public Assembly LoadPlugin(string pluginName)
        {
            return LoadAssembly($"{pluginName}.dll");
        }

        public Assembly LoadAssembly(string fileName)
        {
            var filePath = Path.Combine(_basePath, fileName);

            if (File.Exists(filePath))
            {
                return _context.LoadFromAssemblyPath(filePath);
            }

            return null;
        }

        private Assembly LoadContext_ResolvingAssembly(AssemblyLoadContext assemblyLoadContext, AssemblyName assemblyName)
        {
            return LoadAssembly($"{assemblyName.Name}.dll");
        }
    }
}
