using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop.Plugin
{
    public class AudioBusAccessor
    {
        private static int SizeOfAudioBusBuffers = Marshal.SizeOf(typeof(AudioBusBuffers));
        private static int SizeOfSinglePtr = Marshal.SizeOf(typeof(Single*));
        private static int SizeOfDoublePtr = Marshal.SizeOf(typeof(Double*));

        private SymbolicSampleSizes _sampleSize;
        private AudioBusBuffers _audioBuffers = new AudioBusBuffers();
        private BusDirections _busDir;
        private int _numSamples;

        public AudioBusAccessor(ref ProcessData processData, BusDirections busDir, int busIndex)
        {
            _busDir = busDir;
            _numSamples = processData.NumSamples;
            _sampleSize = processData.SymbolicSampleSize;

            if (busDir == BusDirections.Input)
            {
                Guard.ThrowIfOutOfRange("busIndex", busIndex, 0, processData.NumInputs);

                InitializeAudioBuffers(processData.Inputs, busIndex);
            }
            else
            {
                Guard.ThrowIfOutOfRange("busIndex", busIndex, 0, processData.NumOutputs);

                InitializeAudioBuffers(processData.Outputs, busIndex);
            }
        }

        private void InitializeAudioBuffers(IntPtr arrayPtr, int index)
        {
            IntPtr bufferPtr = IntPtr.Add(arrayPtr, index * SizeOfAudioBusBuffers);
            
            Marshal.PtrToStructure(bufferPtr, _audioBuffers);
        }

        public BusDirections BusDirection
        {
            get { return _busDir; }
        }

        public SymbolicSampleSizes SampleSize
        {
            get { return _sampleSize; }
        }

        public int SampleCount
        {
            get { return _numSamples; }
        }

        public int ChannelCount
        {
            get { return _audioBuffers.NumChannels; }
        }

        public bool IsChannelSilent(int channelIndex)
        {
            Guard.ThrowIfOutOfRange("channel", channelIndex, 0, _audioBuffers.NumChannels);

            return (_audioBuffers.SilenceFlags & (ulong)(1 << channelIndex)) != 0;
        }

        public unsafe float* GetUnsafeBuffer32(int channelIndex)
        {
            if (_sampleSize != SymbolicSampleSizes.Sample32)
            {
                throw new InvalidOperationException("32 bit sample size is not supported.");
            }
            Guard.ThrowIfOutOfRange("channel", channelIndex, 0, _audioBuffers.NumChannels);

            if (_audioBuffers.ChannelBuffers32 != IntPtr.Zero &&
                !IsChannelSilent(channelIndex))
            {
                IntPtr bufferPtr = IntPtr.Add(_audioBuffers.ChannelBuffers32, channelIndex * SizeOfSinglePtr);

                return (float*)bufferPtr.ToPointer();
            }

            return null;
        }

        public unsafe double* GetUnsafeBuffer64(int channelIndex)
        {
            if (_sampleSize != SymbolicSampleSizes.Sample64)
            {
                throw new InvalidOperationException("64 bit sample size is not supported.");
            }
            Guard.ThrowIfOutOfRange("channel", channelIndex, 0, _audioBuffers.NumChannels);

            if (_audioBuffers.ChannelBuffers64 != IntPtr.Zero &&
                !IsChannelSilent(channelIndex))
            {
                IntPtr bufferPtr = IntPtr.Add(_audioBuffers.ChannelBuffers64, channelIndex * SizeOfDoublePtr);

                return (double*)bufferPtr.ToPointer();
            }

            return null;
        }
    }
}
