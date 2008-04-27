namespace Jacobi.Vst.Framework.Common
{
    using System.Threading;
    using System;

    public class ExtensibleInterfaceRef<T> where T : class
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
    }
}
