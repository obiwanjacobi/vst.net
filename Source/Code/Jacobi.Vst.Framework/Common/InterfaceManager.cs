namespace Jacobi.Vst.Framework.Common
{
    using System;

    internal class InterfaceManager<T> : IDisposable
            where T : class
    {
        public delegate T CreateInterface(T instance);

        private ExtensibleInterfaceRef<T> _interfaceRef;
        private CreateInterface _createCallback;

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

        public ExtensibleInterfaceRef<T> MatchInterface<T_Intf>() where T_Intf : class
        {
            if (ExtensibleInterfaceRef<T>.IsMatch<T_Intf>())
            {
                return GetInterface();
            }

            return null;
        }

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
