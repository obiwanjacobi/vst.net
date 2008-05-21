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

        public static bool IsMatch<TIntf>() where TIntf : class
        {
            return (typeof(T).IsAssignableFrom(typeof(TIntf)));
        }

        #region IDisposable Members

        public void Dispose()
        {
            IDisposable disposableObject = Instance as IDisposable;
            
            if (disposableObject != null)
            {
                disposableObject.Dispose();
            }

            disposableObject = ThreadSafeInstance as IDisposable;

            if (disposableObject != null)
            {
                disposableObject.Dispose();
            }

            ThreadSafeInstance = null;
            _instance = null;
            _threadId = 0;
        }

        #endregion
    }
}
