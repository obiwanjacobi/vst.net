namespace Jacobi.Vst.Framework
{
    using System;
    using System.Threading;

    public struct ExtensibleObjectRef<T>
        where T : IExtensibleObject
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
            get { return _instance; }
        }

        public bool Supports<TIntf>() where TIntf : class
        {
            return _instance.Supports<TIntf>(Thread.CurrentThread.ManagedThreadId == _threadId);
        }

        public TIntf GetInstance<TIntf>() where TIntf : class
        {
            return _instance.GetInstance<TIntf>(Thread.CurrentThread.ManagedThreadId == _threadId);
        }
    }
}
