﻿using System;
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
            // flushing parameters
            if (data.NumInputs == 0 || data.NumOutputs == 0)
            {
                return TResult.S_False;
            }
            
            if (data.NumInputs != _audioInputs.Count || data.NumOutputs != _audioOutputs.Count)
            {
                return TResult.E_Unexpected;
            }

            var inputBusInfo = _audioInputs[0];
            var outputBusInfo = _audioOutputs[0];

            if (!inputBusInfo.IsActive || !outputBusInfo.IsActive)
            {
                return TResult.S_False;
            }

            // hard-coded on one stereo input and one stereo output bus
            var inputBus = new AudioBusAccessor(ref data, BusDirections.Input, 0);
            var outputBus = new AudioBusAccessor(ref data, BusDirections.Output, 0);

            unsafe
            {
                // can return null!
                var inputLeft = inputBus.GetUnsafeBuffer32(0);
                var inputRight = inputBus.GetUnsafeBuffer32(1);

                var outputLeft = outputBus.GetUnsafeBuffer32(0);
                var outputRight = outputBus.GetUnsafeBuffer32(1);

                // copy samples
                for (int i = 0; i < outputBus.SampleCount; i++)
                {
                    if (outputLeft != null && inputLeft != null)
                    {
                        outputLeft[i] = inputLeft[i];
                    }
                    if (outputRight != null && inputRight != null)
                    {
                        outputRight[i] = inputRight[i];
                    }
                }
            }

            return TResult.S_OK;
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
