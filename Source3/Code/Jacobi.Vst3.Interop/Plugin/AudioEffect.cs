using System;
using Jacobi.Vst3.Interop;

namespace Jacobi.Vst3.Plugin
{
    public abstract class AudioEffect : Component, IAudioProcessor, IComponent, IPluginBase
    {
        public bool IsProcessing { get; set; }

        public ProcessModes ProcessMode { get; set; }

        public SymbolicSampleSizes SampleSize { get; set; }

        public int MaxSamplesPerBlock { get; set; }

        public double SampleRate { get; set; }

        #region IAudioProcessor Members

        public virtual int SetBusArrangements(SpeakerArrangement[] inputs, int numIns, SpeakerArrangement[] outputs, int numOuts)
        {
            System.Diagnostics.Trace.WriteLine("IAudioProcessor.SetBusArrangements");

            int index = 0;
            var busses = GetBusCollection(MediaTypes.Audio, BusDirections.Input);

            if (busses != null)
            {
                foreach (AudioBus bus in busses)
                {
                    if (index < numIns)
                    {
                        bus.SpeakerArrangement = inputs[index];
                    }

                    index++;
                }
            }

            busses = GetBusCollection(MediaTypes.Audio, BusDirections.Output);

            if (busses != null)
            {
                index = 0;
                foreach (AudioBus bus in busses)
                {
                    if (index < numOuts)
                    {
                        bus.SpeakerArrangement = outputs[index];
                    }

                    index++;
                }
            }

            return TResult.S_OK;
        }

        public virtual int GetBusArrangement(BusDirections dir, int index, ref SpeakerArrangement arr)
        {
            System.Diagnostics.Trace.WriteLine("IAudioProcessor.GetBusArrangement(" + dir + ", " + index + ")");

            var busses = GetBusCollection(MediaTypes.Audio, dir);

            if (busses == null)
            {
                return TResult.E_NotImplemented;
            }
            if (index < 0 || index > busses.Count)
            {
                return TResult.E_InvalidArg;
            }

            arr = ((AudioBus)busses[index]).SpeakerArrangement;

            return TResult.S_OK;
        }

        public abstract int CanProcessSampleSize(SymbolicSampleSizes symbolicSampleSize);

        public virtual uint GetLatencySamples()
        {
            System.Diagnostics.Trace.WriteLine("IAudioProcessor.CanProcessSampleSize");

            return 0;
        }

        public virtual int SetupProcessing(ref ProcessSetup setup)
        {
            System.Diagnostics.Trace.WriteLine("IAudioProcessor.SetupProcessing");

            if (this.IsActive)
            {
                return TResult.E_Unexpected;
            }
            if (!TResult.IsTrue(CanProcessSampleSize(setup.SymbolicSampleSize)))
            {
                return TResult.S_False;
            }

            this.MaxSamplesPerBlock = setup.MaxSamplesPerBlock;
            this.ProcessMode = setup.ProcessMode;
            this.SampleRate = setup.SampleRate;
            this.SampleSize = setup.SymbolicSampleSize;

            return TResult.S_True;
        }

        public virtual int SetProcessing(bool state)
        {
            System.Diagnostics.Trace.WriteLine("IAudioProcessor.SetProcessing(" + state + ")");

            if (!this.IsActive)
            {
                return TResult.E_Unexpected;
            }

            this.IsProcessing = state;

            return TResult.S_OK;
        }

        public abstract int Process(ref ProcessData data);

        public uint GetTailSamples()
        {
            System.Diagnostics.Trace.WriteLine("IAudioProcessor.GetTailSamples");

            return Constants.NoTailSamples;
        }

        #endregion
    }
}
