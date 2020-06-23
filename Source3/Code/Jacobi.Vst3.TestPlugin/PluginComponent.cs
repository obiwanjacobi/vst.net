using Jacobi.Vst3.Core;
using Jacobi.Vst3.Plugin;
using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.TestPlugin
{
    [System.ComponentModel.DisplayName("My Plugin Component")]
    [Guid("599B4AD4-932E-4B35-B8A7-E01508FD1AAB")]
    [ClassInterface(ClassInterfaceType.None)]
    class PluginComponent : AudioEffect
    {
        private readonly BusCollection _audioInputs = new BusCollection(MediaTypes.Audio, BusDirections.Input);
        private readonly BusCollection _audioOutputs = new BusCollection(MediaTypes.Audio, BusDirections.Output);

        public PluginComponent()
        {
            this.ControlledClassId = typeof(MyEditController).GUID;

            _audioInputs.Add(new AudioBus(SpeakerArrangement.ArrStereo, "Main Input", BusTypes.Main));
            _audioOutputs.Add(new AudioBus(SpeakerArrangement.ArrStereo, "Main Output", BusTypes.Main));
        }

        public override int CanProcessSampleSize(SymbolicSampleSizes symbolicSampleSize)
        {
            System.Diagnostics.Trace.WriteLine("IAudioProcessor.CanProcessSampleSize(" + symbolicSampleSize + ")");

            return symbolicSampleSize == SymbolicSampleSizes.Sample32 ? TResult.S_True : TResult.S_False;
        }

        public override int Process(ref ProcessData data)
        {
            //System.Diagnostics.Trace.WriteLine("IAudioProcessor.Process: numSamples=" + data.NumSamples);

            int result = ProcessInParameters(ref data);

            if (TResult.Failed(result))
            {
                return result;
            }

            return ProcessAudio(ref data);
        }

        private int ProcessInParameters(ref ProcessData data)
        {
            // TODO: Parameter handling
            var paramChanges = data.GetInputParameterChanges();
            if (paramChanges != null)
            {
                var paramCount = paramChanges.GetParameterCount();

                if (paramCount > 0)
                {
                    System.Diagnostics.Trace.WriteLine("IAudioProcessor.Process: InputParameterChanges.GetParameterCount() = " + paramCount);

                    for (int i = 0; i < paramCount; i++)
                    {
                        //    var paramValue = paramChanges.GetParameterData(i);
                        //    Marshal.ChangeWrapperHandleStrength(paramValue, fIsWeak: true);

                        //    for (int p = 0; p < paramValue.GetPointCount(); p++)
                        //    {
                        //        int sampleOffset = 0;
                        //        double val = 0;

                        //        paramValue.GetPoint(p, ref sampleOffset, ref val);
                        //    }
                    }
                }
            }

            //var paramCount = data.InputEvents.GetEventCount();
            //var paramCount = data.OutputEvents.GetEventCount();

            return TResult.S_OK;
        }

        private int ProcessAudio(ref ProcessData data)
        {
            // flushing parameters
            if (data.NumInputs == 0 || data.NumOutputs == 0)
            {
                return TResult.S_OK;
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

            // hard-coded on one stereo input and one stereo output bus (see ctor)
            var inputBus = new AudioBusAccessor(ref data, BusDirections.Input, 0);
            var outputBus = new AudioBusAccessor(ref data, BusDirections.Output, 0);

            unsafe
            {
                for (int c = 0; c < inputBus.ChannelCount; c++)
                {
                    var input = inputBus.GetUnsafeBuffer32(c);
                    var output = outputBus.GetUnsafeBuffer32(c);
                    outputBus.SetChannelSilent(0, input == null);

                    if (input != null && output != null)
                    {
                        for (int i = 0; i < outputBus.SampleCount; i++)
                        {
                            output[i] = input[i];
                        }
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
