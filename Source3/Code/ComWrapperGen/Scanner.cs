using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ComWrapperGen
{
    internal class Scanner
    {
        private readonly List<Type> _interfaces = new();
        private readonly Dictionary<Type, IEnumerable<Type>> _objectMap = new();

        public IEnumerable<Type> Interfaces => _interfaces;
        public IDictionary<Type, IEnumerable<Type>> ObjectInterfaces => _objectMap;

        public void Scan(string assemblyPath)
            => Scan(Assembly.Load(assemblyPath));

        public void Scan(Assembly assembly)
        {
            var objectsToScan = new List<Type>();

            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsPublic)
                    continue;

                if (type.IsInterface &&
                    type.GetCustomAttribute<ComInterfaceAttribute>() is not null)
                    _interfaces.Add(type);

                if (type.IsClass)
                {
                    objectsToScan.Add(type);
                }
            }

            foreach (var obj in objectsToScan)
            {
                var objIntfs = obj.GetInterfaces();

                var objImpl = _interfaces.Intersect(objIntfs).ToList();
                if (objImpl.Count > 0)
                {
                    _objectMap.Add(obj, objImpl);
                }
            }
        }
    }
}
