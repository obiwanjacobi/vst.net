using System;
using Jacobi.Vst3.Interop;

namespace Jacobi.Vst3.Plugin
{
    public abstract class AudioEffect : Component, IAudioProcessor, IComponent, IPluginBase
    {
        #region IAudioProcessor Members

        public bool IsProcessing { get; set; }

        public ProcessModes ProcessMode { get; set; }

        public SymbolicSampleSizes SampleSize { get; set; }

        public int MaxSamplesPerBlock { get; set; }

        public double SampleRate { get; set; }

        public virtual int SetBusArrangements(SpeakerArrangement[] inputs, int numIns, SpeakerArrangement[] outputs, int numOuts)
        {
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
            return 0;
        }

        public virtual int SetupProcessing(ref ProcessSetup setup)
        {
            if (CanProcessSampleSize(setup.SymbolicSampleSize) != TResult.S_True)
            {
                return TResult.S_False;
            }

            this.MaxSamplesPerBlock = setup.MaxSamplesPerBlock;
            this.ProcessMode = setup.ProcessMode;
            this.SampleRate = setup.SampleRate;
            this.SampleSize = setup.SymbolicSampleSize;

            return TResult.S_True;
        }

        public virtual int SetProcessing(byte state)
        {
            if (!this.IsActive) return TResult.E_Unexpected;

            this.IsProcessing = state != 0;

            return TResult.S_OK;
        }

        public abstract int Process(ref ProcessData data);

        #endregion

        
    }
}
