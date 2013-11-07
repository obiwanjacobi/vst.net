using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jacobi.Vst3.Interop;
using Jacobi.Vst3.Interop.Plugin;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.TestPlugin
{
    [System.ComponentModel.DisplayName("My Plugin Component")]
    [Guid("599B4AD4-932E-4B35-B8A7-E01508FD1AAB")]
    [ClassInterface(ClassInterfaceType.None)]
    class PluginComponent : AudioEffect, IAudioProcessor, IComponent, IPluginBase
    {
        private BusCollection _audioInputs = new BusCollection(MediaTypes.Audio, BusDirections.Input);
        private BusCollection _audioOutputs = new BusCollection(MediaTypes.Audio, BusDirections.Output);

        public PluginComponent()
        {
            this.ControlledClassId = new Guid(typeof(EditController).GetClassGuid());

            _audioInputs.Add(new AudioBus(SpeakerArrangement.ArrStereo, "Main Input", BusTypes.Main, BusInfo.BusFlags.DefaultActive));
            _audioOutputs.Add(new AudioBus(SpeakerArrangement.ArrStereo, "Main Output", BusTypes.Main, BusInfo.BusFlags.DefaultActive));
        }

        public override int CanProcessSampleSize(SymbolicSampleSizes symbolicSampleSize)
        {
            return symbolicSampleSize == SymbolicSampleSizes.Sample32 ? TResult.S_True : TResult.S_False;
        }

        public override int Process(ref ProcessData data)
        {
            // TODO

            return TResult.E_NotImplemented;
        }

        protected override BusCollection GetBusCollection(MediaTypes mediaType, BusDirections busDir)
        {
            if (mediaType == MediaTypes.Audio)
            {
                return busDir == BusDirections.Input ? _audioInputs : _audioOutputs;
            }

            return null;
        }
    }
}
