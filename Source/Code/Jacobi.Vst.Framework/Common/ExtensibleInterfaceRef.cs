namespace Jacobi.Vst.Framework.Common
{
    using System.Threading;
    using System;

    public class ExtensibleInterfaceRef<T> : IDisposable where T : class
    {
        private int _threadId;
        private T _instance;
        public T Instance
        {
            get { return _instance; }
            set
            {
                if (_instance != null && value != null) throw new InvalidOperationException("Instance is already set.");

                _instance = value;
                _threadId = (value != null) ? Thread.CurrentThread.ManagedThreadId : 0;
            }
        }

        public T SafeInstance
        {
            get
            {
                if (IsConstructionThread)
                {
                    return Instance;
                }

                return ThreadSafeInstance;
            }
        }

        public T ThreadSafeInstance { get; set; }

        public bool IsConstructionThread
        {
            get { return (Thread.CurrentThread.ManagedThreadId == _threadId); }
        }

        public bool IsCreated
        {
            get { return (_instance != null && ThreadSafeInstance != null); }
        }

        private bool _disposeInstance = true;
        public bool DisposeInstance
        {
            get { return _disposeInstance; }
            set { _disposeInstance = value; }
        }

        private bool _disposeThreadSafeInstance = true;
        public bool DisposeThreadSafeInstance
        {
            get { return _disposeThreadSafeInstance; }
            set { _disposeThreadSafeInstance = value; }
        }

        public static bool IsMatch<TIntf>() where TIntf : class
        {
            return (typeof(T).IsAssignableFrom(typeof(TIntf)));
        }

        #region IDisposable Members

        public void Dispose()
        {
            IDisposable disposableObject = null;

            if (DisposeInstance)
            {
                disposableObject = Instance as IDisposable;

                if (disposableObject != null)
                {
                    disposableObject.Dispose();
                }
            }

            if (DisposeThreadSafeInstance)
            {
                disposableObject = ThreadSafeInstance as IDisposable;

                if (disposableObject != null)
                {
                    disposableObject.Dispose();
                }
            }

            ThreadSafeInstance = null;
            _instance = null;
            _threadId = 0;
        }

        #endregion
    }
}
