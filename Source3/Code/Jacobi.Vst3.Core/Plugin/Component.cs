using Jacobi.Vst3.Core;
using System;

namespace Jacobi.Vst3.Plugin
{
    public abstract class Component : ConnectionPoint, IComponent
    {
        protected abstract BusCollection GetBusCollection(MediaTypes mediaType, BusDirections busDir);

        public Guid ControlledClassId { get; protected set; }

        public bool IsActive { get; protected set; }

        #region IComponent Members

        public virtual int GetControllerClassId(ref Guid controllerClassId)
        {
            System.Diagnostics.Trace.WriteLine("IComponent.GetControllerClassId");

            controllerClassId = ControlledClassId;

            return ControlledClassId == Guid.Empty ? TResult.E_NotImplemented : TResult.S_OK;
        }

        public virtual int SetIoMode(IoModes mode)
        {
            System.Diagnostics.Trace.WriteLine("IComponent.SetIoMode(" + mode + ")");

            return TResult.E_NotImplemented;
        }

        // retval NOT a TResult
        public virtual int GetBusCount(MediaTypes type, BusDirections dir)
        {
            System.Diagnostics.Trace.WriteLine("IComponent.GetBusCount(" + type + ", " + dir + ")");

            var busses = GetBusCollection(type, dir);

            if (busses != null)
            {
                return busses.Count;
            }

            return 0;
        }

        public virtual int GetBusInfo(MediaTypes type, BusDirections dir, int index, ref BusInfo bus)
        {
            System.Diagnostics.Trace.WriteLine("IComponent.GetBusInfo(" + type + ", " + dir + ", " + index + ")");

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
            System.Diagnostics.Trace.WriteLine("IComponent.GetRoutingInfo");

            return TResult.E_NotImplemented;
        }

        public virtual int ActivateBus(MediaTypes type, BusDirections dir, int index, bool state)
        {
            System.Diagnostics.Trace.WriteLine("IComponent.ActivateBus(" + type + ", " + dir + ", " + index + ", " + state + ")");

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
            System.Diagnostics.Trace.WriteLine("IComponent.SetActive(" + state + ")");

            IsActive = state;

            return TResult.S_OK;
        }

        public virtual int SetState(IBStream state)
        {
            System.Diagnostics.Trace.WriteLine("IComponent.SetState");

            return TResult.E_NotImplemented;
        }

        public virtual int GetState(IBStream state)
        {
            System.Diagnostics.Trace.WriteLine("IComponent.GetState");

            return TResult.E_NotImplemented;
        }

        #endregion
    }
}
