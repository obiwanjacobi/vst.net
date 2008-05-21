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
            }
            else if (!_interfaceRef.IsConstructionThread && _interfaceRef.ThreadSafeInstance == null)
            {
                _interfaceRef.ThreadSafeInstance = _createCallback(_interfaceRef.Instance);
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
