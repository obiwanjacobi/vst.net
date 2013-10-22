using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jacobi.Vst3.Interop;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.TestPlugin
{
    [System.ComponentModel.DisplayName("My Plugin Component")]
    [Guid("599B4AD4-932E-4B35-B8A7-E01508FD1AAB")]
    [ClassInterface(ClassInterfaceType.None)]
    class PluginComponent : IComponent
    {
        #region IPluginBase Members

        public int Initialize(object context)
        {
            return TResult.S_OK;
        }

        public int Terminate()
        {
            return TResult.S_OK;
        }

        #endregion

        #region IComponent Members


        public int GetControllerClassId(ref Guid controllerClassId)
        {
            return TResult.E_NotImplemented;
        }

        public int SetIoMode(IoModes mode)
        {
            return TResult.E_NotImplemented;
        }

        public int GetBusCount(MediaTypes type, BusDirections dir)
        {
            return 0;
        }

        public int GetBusInfo(MediaTypes type, BusDirections dir, int index, ref BusInfo bus)
        {
            return TResult.E_NotImplemented;
        }

        public int GetRoutingInfo(ref RoutingInfo inInfo, ref RoutingInfo outInfo)
        {
            return TResult.E_NotImplemented;
        }

        public int ActivateBus(MediaTypes type, BusDirections dir, int index, bool state)
        {
            return TResult.E_NotImplemented;
        }

        public int SetActive(bool state)
        {
            return TResult.E_NotImplemented;
        }

        public int SetState(IBStream state)
        {
            return TResult.E_NotImplemented;
        }

        public int GetState(IBStream state)
        {
            return TResult.E_NotImplemented;
        }

        #endregion
    }
}
