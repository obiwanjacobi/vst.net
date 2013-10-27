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
    class PluginComponent : IComponent, IAudioProcessor
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
            return TResult.S_False;
        }

        public int GetBusCount(MediaTypes type, BusDirections dir)
        {
            if (type == MediaTypes.Audio && dir == BusDirections.Input)
                return 1;

            return 0;
        }

        public int GetBusInfo(MediaTypes type, BusDirections dir, int index, ref BusInfo busInfo)
        {
            if (type == MediaTypes.Audio && dir == BusDirections.Input && index == 0)
            {
                busInfo.BusType = BusTypes.Main;
                busInfo.ChannelCount = 1;
                busInfo.Direction = dir;
                busInfo.Flags = BusInfo.BusFlags.DefaultActive;
                busInfo.MediaType = type;
                busInfo.Name = "Input";

                return TResult.S_OK;
            }

            return TResult.E_Unexpected;
        }

        public int GetRoutingInfo(ref RoutingInfo inInfo, ref RoutingInfo outInfo)
        {
            return TResult.E_NotImplemented;
        }

        public int ActivateBus(MediaTypes type, BusDirections dir, int index, bool state)
        {
            if (type == MediaTypes.Audio && dir == BusDirections.Input && index == 0)
            {
                return TResult.S_OK;
            }

            return TResult.E_Fail;
        }

        public int SetActive(bool state)
        {
            return TResult.S_OK;
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

        #region IAudioProcessor Members

        public int SetBusArrangements(ulong[] inputs, int numIns, ulong[] outputs, int numOuts)
        {
            return TResult.E_NotImplemented;
        }

        public int GetBusArrangement(BusDirections dir, int index, ref ulong arr)
        {
            return TResult.E_NotImplemented;
        }

        public int CanProcessSampleSize(int symbolicSampleSize)
        {
            return TResult.S_OK;
        }

        public uint GetLatencySamples()
        {
            return 0;
        }

        public int SetupProcessing(ref ProcessSetup setup)
        {
            return TResult.S_OK;
        }

        public int SetProcessing(byte state)
        {
            return TResult.E_NotImplemented;
        }

        public int Process(ref ProcessData data)
        {
            return TResult.S_OK;
        }

        #endregion
    }
}
