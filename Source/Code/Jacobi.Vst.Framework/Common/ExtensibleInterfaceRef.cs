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
            get
            {
                if (IsConstructionThread)
                {
                    return _instance;
                }

                return ThreadSafeInstance;
            }
            set
            {
                if (_instance != null) throw new InvalidOperationException("Instance is already set.");

                _instance = value;
                _threadId = Thread.CurrentThread.ManagedThreadId;
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
            return (typeof(TIntf).IsAssignableFrom(typeof(T)));
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
