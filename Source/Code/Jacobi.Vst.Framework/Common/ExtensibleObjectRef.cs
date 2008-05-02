namespace Jacobi.Vst.Framework.Common
{
    using System;
    using System.Threading;

    public class ExtensibleObjectRef<T> : IDisposable
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

        #region IDisposable Members

        public void Dispose()
        {
            IDisposable disposableObject = _instance as IDisposable;

            if (disposableObject != null)
            {
                disposableObject.Dispose();
            }

            _instance = null;
            _threadId = 0;
        }

        #endregion
    }
}
