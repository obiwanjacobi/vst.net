namespace Jacobi.Vst.Samples.MidiNoteSampler
{
    using System;
    using System.Collections.Generic;
    
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;

    internal class SampleManager
    {
        private Dictionary<byte, StereoBuffer> _noteMap = new Dictionary<byte, StereoBuffer>();
        
        public void ProcessNoteOnEvent(byte noteNo)
        {
            System.Diagnostics.Debug.WriteLine("Note On event for note:" + noteNo, "VST.NET");

            if (_noteMap.ContainsKey(noteNo))
            {
                _player = new SamplePlayer(_noteMap[noteNo]);

                System.Diagnostics.Debug.WriteLine("Playing Sample for note:" + noteNo, "VST.NET");
            }
            else if(_recorder == null)
            {
                _recorder = new SampleRecorder(noteNo);

                System.Diagnostics.Debug.WriteLine("Recording Sample for note:" + noteNo, "VST.NET");
            }
        }

        public void ProcessNoteOffEvent(byte noteNo)
        {
            System.Diagnostics.Debug.WriteLine("Note Off event for note:" + noteNo, "VST.NET");

            if (_recorder != null && _recorder.Buffer.NoteNo == noteNo)
            {
                _noteMap.Add(noteNo, _recorder.Buffer);
                _recorder = null;

                System.Diagnostics.Debug.WriteLine("Stop Recording Sample for note:" + noteNo, "VST.NET");
            }

            if (_player != null && _player.Buffer.NoteNo == noteNo)
            {
                _player = null;

                System.Diagnostics.Debug.WriteLine("Stop Playing Sample for note:" + noteNo, "VST.NET");
            }
        }

        private SampleRecorder _recorder;
        public bool IsRecording
        {
            get { return _recorder != null; }
        }

        public void RecordAudio(VstAudioChannel[] channels)
        {
            if (IsRecording)
            {
                _recorder.Record(channels[0], channels[1]);
            }
        }

        private SamplePlayer _player;
        public bool IsPlaying
        {
            get { return _player != null; }
        }

        public void PlayAudio(VstAudioChannel[] channels)
        {
            if (IsPlaying)
            {
                _player.Play(channels[0], channels[1]);

                if (_player.IsFinished)
                {
                    _player = null;
                }
            }
        }

        //---------------------------------------------------------------------

        private class SampleRecorder
        {
            public SampleRecorder(byte noteNo)
            {
                Buffer = new StereoBuffer(noteNo);
            }

            public StereoBuffer Buffer { get; private set; }

            public void Record(VstAudioChannel left, VstAudioChannel right)
            {
                for (int index = 0; index < left.SampleCount; index++)
                {
                    Buffer.LeftSamples.Add(left[index]);
                }

                for (int index = 0; index < right.SampleCount; index++)
                {
                    Buffer.RightSamples.Add(right[index]);
                }
            }
        }

        //---------------------------------------------------------------------

        private class SamplePlayer
        {
            public SamplePlayer(StereoBuffer buffer)
            {
                Buffer = buffer;
            }

            private int _bufferIndex;

            public StereoBuffer Buffer { get; private set; }

            public void Play(VstAudioChannel left, VstAudioChannel right)
            {
                if (IsFinished) return;

                int count = Math.Min(left.SampleCount, Buffer.LeftSamples.Count - _bufferIndex);

                for (int index = 0; index < count; index++)
                {
                    left[index] = Buffer.LeftSamples[_bufferIndex + index];
                }

                for (int index = 0; index < count; index++)
                {
                    right[index] = Buffer.RightSamples[_bufferIndex + index];
                }

                _bufferIndex += left.SampleCount;
            }

            public bool IsFinished
            {
                get { return (_bufferIndex >= Buffer.LeftSamples.Count); }
            }
        }

        //---------------------------------------------------------------------

        private class StereoBuffer
        {
            public StereoBuffer(byte noteNo)
            {
                NoteNo = noteNo;
            }

            public byte NoteNo;
            public List<float> LeftSamples = new List<float>();
            public List<float> RightSamples = new List<float>();
        }
    }
}
