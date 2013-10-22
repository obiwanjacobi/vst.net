using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.TestPlugin
{
    public class ServiceContainer : IServiceProvider
    {
        private object _unknown;

        public ServiceContainer(object unknown)
        {
            _unknown = unknown;
        }

        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            if (serviceType.IsInterface && _unknown != null)
            {
                var intf = Marshal.GetComInterfaceForObject(_unknown, serviceType);
                return Marshal.GetObjectForIUnknown(intf);
            }

            return null;
        }

        #endregion
    }
}
