using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Jacobi.Vst3.Common
{
    public sealed class ComRef<T> : IDisposable where T : class
    {
        public ComRef(T instance)
        {
            Instance = instance;
        }

        public T Instance { get; private set; }

        public void Dispose()
        {
            if (Instance != null)
            {
                Marshal.FinalReleaseComObject(Instance);
                Instance = null;
            }
        }

        public static ComRef<T> Create(T instance)
        {
            if (instance != null)
            {
                return new ComRef<T>(instance);
            }

            return null;
        }

        public static T GetInstance(ComRef<T> comRef)
        {
            return comRef == null ? null : comRef.Instance;
        }

        public static void Dispose(ref ComRef<T> comRef)
        {
            if (comRef != null)
            {
                comRef.Dispose();
                comRef = null;
            }
        }
    }
}
