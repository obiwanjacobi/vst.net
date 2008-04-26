namespace Jacobi.Vst.Framework
{
    using System;
    using System.Threading;

    public struct ExtensibleObjectRef<T>
        where T : class, IExtensibleObject
    {
        private int _threadId;

        public ExtensibleObjectRef(T instance)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            
            _instance = instance;
            _threadId = Thread.CurrentThread.ManagedThreadId;
        }

        private T _instance;
        public T Instance
        {
            get
            {
                if (!IsConstructionThread)
                {
                    return _instance.GetInstance<T>(true);
                }

                return _instance;
            }
        }

        public bool Supports<TIntf>() where TIntf : class
        {
            return _instance.Supports<TIntf>(!IsConstructionThread);
        }

        public TIntf GetInstance<TIntf>() where TIntf : class
        {
            return _instance.GetInstance<TIntf>(!IsConstructionThread);
        }

        public bool IsConstructionThread
        {
            get { return (Thread.CurrentThread.ManagedThreadId == _threadId); }
        }
    }
}
