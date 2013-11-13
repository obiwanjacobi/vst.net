using System;
using Jacobi.Vst3.Interop;

namespace Jacobi.Vst3.Plugin
{
    public abstract class Component : ComponentBase, IComponent, IPluginBase
    {
        protected abstract BusCollection GetBusCollection(MediaTypes mediaType, BusDirections busDir);

        public Guid ControlledClassId { get; protected set; }

        public bool IsActive { get; protected set; }

        #region IComponent Members

        public virtual int GetControllerClassId(ref Guid controllerClassId)
        {
            controllerClassId = ControlledClassId;

            return ControlledClassId == Guid.Empty ? TResult.E_NotImplemented : TResult.S_OK;
        }

        public virtual int SetIoMode(IoModes mode)
        {
            return TResult.E_NotImplemented;
        }

        // retval NOT a TResult
        public virtual int GetBusCount(MediaTypes type, BusDirections dir)
        {
            var busses = GetBusCollection(type, dir);

            if (busses != null)
            {
                return busses.Count;
            }

            return 0;
        }

        public virtual int GetBusInfo(MediaTypes type, BusDirections dir, int index, ref BusInfo bus)
        {
            var busses = GetBusCollection(type, dir);

            if (busses != null)
            {
                if (index < 0 || index >= busses.Count)
                {
                    return TResult.E_InvalidArg;
                }

                busses[index].GetInfo(ref bus);

                return TResult.S_OK;
            }

            return TResult.E_Unexpected;
        }

        public virtual int GetRoutingInfo(ref RoutingInfo inInfo, ref RoutingInfo outInfo)
        {
            return TResult.E_NotImplemented;
        }

        public virtual int ActivateBus(MediaTypes type, BusDirections dir, int index, bool state)
        {
            var busses = GetBusCollection(type, dir);

            if (busses != null)
            {
                if (index < 0 || index >= busses.Count)
                {
                    return TResult.E_InvalidArg;
                }

                busses[index].IsActive = state;

                return TResult.S_OK;
            }

            return TResult.E_Unexpected;
        }

        public virtual int SetActive(bool state)
        {
            IsActive = state;

            return TResult.S_OK;
        }

        public virtual int SetState(IBStream state)
        {
            return TResult.E_NotImplemented;
        }

        public virtual int GetState(IBStream state)
        {
            return TResult.E_NotImplemented;
        }

        #endregion
    }
}
