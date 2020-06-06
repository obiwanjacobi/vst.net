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
            if (paramChanges == null) return TResult.E_Pointer;

            var paramCount = paramChanges.GetParameterCount();
            //var paramCount = data.InputEvents.GetEventCount();
            //var paramCount = data.OutputEvents.GetEventCount();

            if (paramCount > 0)
            {
                System.Diagnostics.Trace.WriteLine("IAudioProcessor.Process: InputParameterChanges.GetParameterCount() = " + paramCount);
            }

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

            // hard-coded on one stereo input and one stereo output bus
            var inputBus = new AudioBusAccessor(ref data, BusDirections.Input, 0);
            var outputBus = new AudioBusAccessor(ref data, BusDirections.Output, 0);

            unsafe
            {
                // can return null!
                var inputLeft = inputBus.GetUnsafeBuffer32(0);
                //var inputRight = inputBus.GetUnsafeBuffer32(1);
                // TODO: check max num channels
                float* inputRight = null;

                var outputLeft = outputBus.GetUnsafeBuffer32(0);
                //var outputRight = outputBus.GetUnsafeBuffer32(1);
                // TODO: check max num channels
                float* outputRight = null;

                // silent inputs result in silent outputs
                outputBus.SetChannelSilent(0, inputLeft == null);
                outputBus.SetChannelSilent(1, inputRight == null);

                if (outputLeft != null && inputLeft != null &&
                    outputRight != null && inputRight != null)
                {
                    // copy samples
                    for (int i = 0; i < outputBus.SampleCount; i++)
                    {
                        outputLeft[i] = inputLeft[i];
                        outputRight[i] = inputRight[i];
                    }
                }
                else if (outputLeft != null && inputLeft != null)
                {
                    // copy samples
                    for (int i = 0; i < outputBus.SampleCount; i++)
                    {
                        outputLeft[i] = inputLeft[i];
                    }
                }
                else if (outputRight != null && inputRight != null)
                {
                    // copy samples
                    for (int i = 0; i < outputBus.SampleCount; i++)
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
