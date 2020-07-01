using Jacobi.Vst3.Common;
using Jacobi.Vst3.Core;
using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Plugin
{
    public class AudioBusAccessor
    {
        private readonly SymbolicSampleSizes _sampleSize;
        private readonly BusDirections _busDir;
        private readonly int _numSamples;
        private AudioBusBuffers _audioBuffers;

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
            IntPtr bufferPtr = IntPtr.Add(arrayPtr, index * AudioBusBuffers.Size);

            _audioBuffers = (AudioBusBuffers)Marshal.PtrToStructure(bufferPtr, typeof(AudioBusBuffers));
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

        public void SetChannelSilent(int channelIndex, bool silent)
        {
            Guard.ThrowIfOutOfRange("channel", channelIndex, 0, _audioBuffers.NumChannels);

            ulong mask = (ulong)(1 << channelIndex);

            // reset (not-silent)
            _audioBuffers.SilenceFlags &= ~mask;

            if (silent)
            {
                // set
                _audioBuffers.SilenceFlags |= mask;
            }
        }

        public int Read32(int channelIndex, float[] buffer, int length)
        {
            if (length > _numSamples)
            {
                length = _numSamples;
            }
            if (length > buffer.Length)
            {
                length = buffer.Length;
            }

            unsafe
            {
                float* ptr = GetUnsafeBuffer32(channelIndex);

                if (ptr != null)
                {
                    for (int i = 0; i < length; i++)
                    {
                        buffer[i] = ptr[i];
                    }

                    return length;
                }
            }

            return 0;
        }

        public int Write32(int channelIndex, float[] buffer, int length)
        {
            if (_busDir == BusDirections.Input)
            {
                return 0;
            }
            if (length > _numSamples)
            {
                length = _numSamples;
            }
            if (length > buffer.Length)
            {
                length = buffer.Length;
            }

            unsafe
            {
                float* ptr = GetUnsafeBuffer32(channelIndex);

                if (ptr != null)
                {
                    for (int i = 0; i < length; i++)
                    {
                        ptr[i] = buffer[i];
                    }

                    return length;
                }
            }

            return 0;
        }

        public int Read64(int channelIndex, double[] buffer, int length)
        {
            if (length > _numSamples)
            {
                length = _numSamples;
            }
            if (length > buffer.Length)
            {
                length = buffer.Length;
            }

            unsafe
            {
                double* ptr = GetUnsafeBuffer64(channelIndex);

                if (ptr != null)
                {
                    for (int i = 0; i < length; i++)
                    {
                        buffer[i] = ptr[i];
                    }

                    return length;
                }
            }

            return 0;
        }

        public int Write64(int channelIndex, double[] buffer, int length)
        {
            if (_busDir == BusDirections.Input)
            {
                return 0;
            }
            if (length > _numSamples)
            {
                length = _numSamples;
            }
            if (length > buffer.Length)
            {
                length = buffer.Length;
            }

            unsafe
            {
                double* ptr = GetUnsafeBuffer64(channelIndex);

                if (ptr != null)
                {
                    for (int i = 0; i < length; i++)
                    {
                        ptr[i] = buffer[i];
                    }

                    return length;
                }
            }

            return 0;
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
                float** ptr = (float**)_audioBuffers.ChannelBuffers32.ToPointer();

                return ptr[channelIndex];
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
                double** ptr = (double**)_audioBuffers.ChannelBuffers64.ToPointer();

                return ptr[channelIndex];
            }

            return null;
        }
    }
}
