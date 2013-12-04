using System;
using System.Collections.Generic;
using System.Diagnostics;
//
// This file is not compiled.
// TO BE DELETED
//
//

using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Jacobi.Vst3.Common
{
    // turns out that calling Marshal.ReleaseComObject will do more harm than good.

    public sealed class ComRef<T> : IDisposable where T : class
    {
        public ComRef(T instance)
        {
            if (!Marshal.IsComObject(instance))
            {
                throw new ArgumentException("The instance is not a Com object.", "instance");
            }

            Instance = instance;
        }

        public T Instance { get; private set; }

        public void Dispose()
        {
            if (Instance != null)
            {
                int refCount = Marshal.ReleaseComObject(Instance);
                Trace.WriteLine("ReleaseComObject - ref count: " + refCount);
                
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

        public static void Release(ref ComRef<T> comRef)
        {
            if (comRef != null)
            {
                comRef.Dispose();
                comRef = null;
            }
        }

        public static void Terminate(ref ComRef<T> comRef)
        {
            if (comRef != null)
            {
                int refCount = Marshal.FinalReleaseComObject(comRef.Instance);
                Trace.WriteLine("FinalReleaseComObject - ref count: " + refCount);

                comRef = null;
            }
        }
    }
}
