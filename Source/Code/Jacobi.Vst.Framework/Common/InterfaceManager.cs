namespace Jacobi.Vst.Framework.Common
{
    using System;

    /// <summary>
    /// This class manages one interface.
    /// </summary>
    /// <typeparam name="T">The type of the interface.</typeparam>
    internal class InterfaceManager<T> : IDisposable
            where T : class
    {
        /// <summary>
        /// A callback delegate that will be used to create an instance of the interface.
        /// </summary>
        /// <param name="instance">The default interface instance or null.</param>
        /// <returns>Returns a type safe instance when <paramref name="instance"/> 
        /// is not null, otherwise the default instance is returned.</returns>
        public delegate T CreateInterface(T instance);

        private ExtensibleInterfaceRef<T> _interfaceRef;
        private CreateInterface _createCallback;

        /// <summary>
        /// Constructs a new instance on the provided callback.
        /// </summary>
        /// <param name="createCallback"></param>
        public InterfaceManager(CreateInterface createCallback)
        {
            _createCallback = createCallback;
        }

        /// <summary>
        /// Gets or sets the reference to the parent instance that will call dispose.
        /// </summary>
        /// <remarks>When a reference returned from the CreateInstance callback matches the
        /// DisposeParent, Dispose will not be called on the reference to avoid recursion and a stackoverflow.
        /// </remarks>
        public object DisposeParent { get; set; }
        
        /// <summary>
        /// Returns the interface reference when it matches <typeparamref name="T_Intf"/>.
        /// </summary>
        /// <typeparam name="T_Intf">The type of interface to match.</typeparam>
        /// <returns>Returns null when no match could be made.</returns>
        public ExtensibleInterfaceRef<T> MatchInterface<T_Intf>() where T_Intf : class
        {
            if (ExtensibleInterfaceRef<T>.IsMatch<T_Intf>())
            {
                return GetInterface();
            }

            return null;
        }

        /// <summary>
        /// Returns the interface reference.
        /// </summary>
        /// <returns>Never returns null.</returns>
        public ExtensibleInterfaceRef<T> GetInterface()
        {
            if (_interfaceRef == null)
            {
                _interfaceRef = new ExtensibleInterfaceRef<T>();

                _interfaceRef.Instance = _createCallback(null);
                _interfaceRef.DisposeInstance = (DisposeParent != _interfaceRef.Instance);
            }
            else if (!_interfaceRef.IsConstructionThread && _interfaceRef.ThreadSafeInstance == null)
            {
                _interfaceRef.ThreadSafeInstance = _createCallback(_interfaceRef.Instance);
                _interfaceRef.DisposeThreadSafeInstance = (DisposeParent != _interfaceRef.ThreadSafeInstance);
            }

            return _interfaceRef;
        }

        #region IDisposable Members
        
        /// <summary>
        /// Disposes the interface reference.
        /// </summary>
        public void Dispose()
        {
            if (_interfaceRef != null)
            {
                _interfaceRef.Dispose();
                _interfaceRef = null;
            }

            _createCallback = null;
        }

        #endregion
    }
}
