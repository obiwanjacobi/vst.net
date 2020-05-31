namespace Jacobi.Vst.Framework.Common
{
    using System;
    using System.Threading;

    /// <summary>
    /// This class manages one interface (instance) that is passed by a <see cref="IExtensible"/> implementation.
    /// </summary>
    /// <typeparam name="T">The type of the interface.</typeparam>
    /// <remarks>This class manages 2 instances of the interface <typeparamref name="T"/>. 
    /// One default instance and one explicitly type safe instance. 
    /// The construction Thread is tracked when the default instance is assigned.
    /// When a call comes in over another Thread, the <see cref="ThreadSafeInstance"/> is used.</remarks>
    public sealed class ExtensibleInterfaceRef<T> : IDisposable where T : class
    {
        private int _threadId;
        private T _instance;
        /// <summary>
        /// Gets or sets the default instance. Can be null.
        /// </summary>
        /// <remarks>You can only set the instance when it is null, otherwise an exception is thrown. 
        /// This is done to guard against accidental overwrites. When you first assign null and then a new reference, no exception is thrown.</remarks>
        /// <exception cref="InvalidOperationException">Thrown when an instance is already assigned.</exception>
        public T Instance
        {
            get { return _instance; }
            set
            {
                if (_instance != null && value != null)
                {
                    throw new InvalidOperationException(Properties.Resources.ExtensibleInterfaceRef_InstanceAlreadySet);
                }

                _instance = value;
                _threadId = (value != null) ? Thread.CurrentThread.ManagedThreadId : 0;
            }
        }

        /// <summary>
        /// Gets the instance that is appropriate for the calling Thread.
        /// </summary>
        /// <remarks>When the call is made on the construction Thread the <see cref="Instance"/> 
        /// reference is returned, otherwise the <see cref="ThreadSafeInstance"/> is returned.</remarks>
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

        private T _threadSafeInstance;
        /// <summary>
        /// Gets or sets the thread safe instance. Can be null.
        /// </summary>
        /// <remarks>You can only set the instance when it is null, otherwise an exception is thrown. 
        /// This is done to guard against accidental overwrites. When you first assign null and then a new reference, no exception is thrown.</remarks>
        /// <exception cref="InvalidOperationException">Thrown when an instance is already assigned.</exception>
        public T ThreadSafeInstance
        {
            get { return _threadSafeInstance; }
            set
            {
                if (_threadSafeInstance != null && value != null)
                {
                    throw new InvalidOperationException(Properties.Resources.ExtensibleInterfaceRef_InstanceAlreadySet);
                }

                _threadSafeInstance = value;
            }
        }

        /// <summary>
        /// Indicates if the current Thread is the construction Thread (true).
        /// </summary>
        public bool IsConstructionThread
        {
            get { return (Thread.CurrentThread.ManagedThreadId == _threadId); }
        }

        private bool _disposeInstance = true;
        /// <summary>
        /// Indicates if the <see cref="Instance"/> should be <see cref="IDisposable.Dispose"/>d when this instance is disposed.
        /// </summary>
        /// <remarks>A recursion problem (stack overflow) can arrise when the interface reference is also the instance that calls <see cref="Dispose"/>.</remarks>
        public bool DisposeInstance
        {
            get { return _disposeInstance; }
            set { _disposeInstance = value; }
        }

        private bool _disposeThreadSafeInstance = true;
        /// <summary>
        /// Indicates if the <see cref="ThreadSafeInstance"/> should be <see cref="IDisposable.Dispose"/>d when this instance is disposed.
        /// </summary>
        /// <remarks>A recursion problem (stack overflow) can arrise when the interface reference is also the instance that calls <see cref="Dispose"/>.</remarks>
        public bool DisposeThreadSafeInstance
        {
            get { return _disposeThreadSafeInstance; }
            set { _disposeThreadSafeInstance = value; }
        }

        /// <summary>
        /// Performs a check to see if the <typeparamref name="TIntf"/> matches the <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="TIntf">The type of interface to match.</typeparam>
        /// <returns>Returns true when a match exists, otherwise false is returned.</returns>
        public static bool IsMatch<TIntf>() where TIntf : class
        {
            return (typeof(T).IsAssignableFrom(typeof(TIntf)));
        }

        #region IDisposable Members
        /// <summary>
        /// Disposes the instance.
        /// </summary>
        /// <remarks>Both the <see cref="Instance"/> and <see cref="ThreadSafeInstance"/> are disposed, 
        /// but only when <see cref="DisposeInstance"/> and <see cref="DisposeThreadSafeInstance"/> are true.
        /// All internal vars are reset. This instance can no longer be used.</remarks>
        public void Dispose()
        {
            IDisposable disposableObject;

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
