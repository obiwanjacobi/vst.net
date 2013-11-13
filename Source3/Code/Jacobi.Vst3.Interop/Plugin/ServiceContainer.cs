using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Jacobi.Vst3.Plugin
{
    public sealed class ServiceContainer : IServiceProvider, IDisposable
    {
        private Dictionary<Type, ServiceRegistration> _registrations = new Dictionary<Type, ServiceRegistration>();

        public ServiceContainer()
        { }

        public ServiceContainer(object unknown)
        {
            Unknown = unknown;
        }

        public ServiceContainer(ServiceContainer parentContainer)
        {
            ParentContainer = parentContainer;
        }

        public ServiceContainer(object unknown, ServiceContainer parentContainer)
        {
            Unknown = unknown;
            ParentContainer = parentContainer;
        }

        public object Unknown { get; set; }

        public ServiceContainer ParentContainer { get; set; }

        public bool Register<T>()
        {
            return Register(typeof(T));
        }

        public bool Register<T>(object instance)
        {
            return Register(typeof(T), instance);
        }

        public bool Register<T>(ObjectCreatorCallback callback)
        {
            return Register(typeof(T), callback);
        }

        public bool Register(Type svcType)
        {
            if (FindRegistration(svcType) == null)
            {
                var svcReg = CreateServiceRegistration(svcType, null, null);
                
                _registrations.Add(svcType, svcReg);

                return true;
            }

            return false;
        }

        public bool Register(Type svcType, object instance)
        {
            if (FindRegistration(svcType) == null)
            {
                var svcReg = CreateServiceRegistration(svcType, instance, null);

                _registrations.Add(svcType, svcReg);

                return true;
            }

            return false;
        }

        public bool Register(Type svcType, ObjectCreatorCallback callback)
        {
            if (FindRegistration(svcType) == null)
            {
                var svcReg = CreateServiceRegistration(svcType, null, callback);

                _registrations.Add(svcType, svcReg);

                return true;
            }

            return false;
        }


        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            var svcReg = FindRegistration(serviceType);

            if (svcReg != null)
            {
                return GetInstance(svcReg);
            }

            if (serviceType.IsInterface && Unknown != null)
            {
                var intf = Marshal.GetComInterfaceForObject(Unknown, serviceType);
                return Marshal.GetObjectForIUnknown(intf);
            }

            if (ParentContainer != null)
            {
                return ParentContainer.GetService(serviceType);
            }

            return null;
        }

        #endregion

        private ServiceRegistration FindRegistration(Type svcType)
        {
            ServiceRegistration svcReg;

            if (_registrations.TryGetValue(svcType, out svcReg))
            {
                return svcReg;
            }

            return null;
        }

        private object GetInstance(ServiceRegistration svcReg)
        {
            if (svcReg.Instance == null)
            {
                if (svcReg.Callback != null)
                {
                    svcReg.Instance = svcReg.Callback(this, svcReg.ServiceType);
                }
                else
                {
                    svcReg.Instance = Activator.CreateInstance(svcReg.ServiceType);
                }
            }

            return svcReg.Instance;
        }

        private ServiceRegistration CreateServiceRegistration(Type svcType, object instance, ObjectCreatorCallback callback)
        {
            return new ServiceRegistration()
            {
                ServiceType = svcType,
                Instance = instance,
                Callback = callback,
            };
        }

        #region IDisposable Members

        public void Dispose()
        {
            foreach (var svcReg in _registrations.Values)
            {
                var disposable = svcReg.Instance as IDisposable;

                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            _registrations.Clear();
            Unknown = null;
            ParentContainer = null;

            GC.SuppressFinalize(this);
        }

        #endregion

        //---------------------------------------------------------------------

        private class ServiceRegistration
        {
            public Type ServiceType;
            public object Instance;
            public ObjectCreatorCallback Callback;
        }
    }
}
