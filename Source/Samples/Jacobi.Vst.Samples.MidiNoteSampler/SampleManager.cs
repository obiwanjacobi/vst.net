﻿namespace Jacobi.Vst.Samples.MidiNoteSampler;

using Jacobi.Vst.Core;
using System;
using System.Collections.Generic;

/// <summary>
/// Manages playback, recording and storing audio samples.
/// </summary>
internal sealed class SampleManager
{
    private readonly Dictionary<byte, StereoBuffer> _noteMap = new Dictionary<byte, StereoBuffer>();

    /// <summary>
    /// Starts recording the current audio or playing back the sample buffer.
    /// </summary>
    /// <param name="noteNo">The midi note number.</param>
    public void ProcessNoteOnEvent(byte noteNo)
    {
        if (_noteMap.ContainsKey(noteNo))
        {
            _player = new SamplePlayer(_noteMap[noteNo]);
        }
        else if (_recorder == null)
        {
            _recorder = new SampleRecorder(noteNo);
        }
    }

    /// <summary>
    /// Stops recording the current audio or playing back the the sample buffer.
    /// </summary>
    /// <param name="noteNo">The midi note number.</param>
    public void ProcessNoteOffEvent(byte noteNo)
    {
        if (_recorder != null && _recorder.Buffer.NoteNo == noteNo)
        {
            _noteMap.Add(noteNo, _recorder.Buffer);
            _recorder = null;
        }

        if (_player != null && _player.Buffer.NoteNo == noteNo)
        {
            _player = null;
        }
    }

    private SampleRecorder? _recorder;
    /// <summary>
    /// Indicates if the current audio stream is being recorded.
    /// </summary>
    public bool IsRecording
    {
        get { return _recorder != null; }
    }

    /// <summary>
    /// Copies out the audio samples from the <paramref name="channels"/>.
    /// </summary>
    /// <param name="channels">Input buffers. Must not be null.</param>
    public void RecordAudio(VstAudioBuffer[] channels)
    {
        if (_recorder != null)
        {
            _recorder.Record(channels[0], channels[1]);
        }
    }

    private SamplePlayer? _player;
    /// <summary>
    /// Indicates if a recorded sample buffer is being played back.
    /// </summary>
    public bool IsPlaying
    {
        get { return _player != null; }
    }

    /// <summary>
    /// Plays back the current sample buffer
    /// </summary>
    /// <param name="channels">Output buffers. Must not be null.</param>
    public void PlayAudio(VstAudioBuffer[] channels)
    {
        if (_player != null)
        {
            _player.Play(channels[0], channels[1]);

            if (_player.IsFinished)
            {
                _player = null;
            }
        }
    }

    //---------------------------------------------------------------------

    /// <summary>
    /// Manages recording audio samples.
    /// </summary>
    private class SampleRecorder
    {
        public SampleRecorder(byte noteNo)
        {
            Buffer = new StereoBuffer(noteNo);
        }

        public StereoBuffer Buffer { get; private set; }

        public void Record(VstAudioBuffer left, VstAudioBuffer right)
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

    /// <summary>
    /// Manages playing back a sample buffer
    /// </summary>
    private class SamplePlayer
    {
        public SamplePlayer(StereoBuffer buffer)
        {
            Buffer = buffer;
        }

        private int _bufferIndex;

        public StereoBuffer Buffer { get; private set; }

        public void Play(VstAudioBuffer left, VstAudioBuffer right)
        {
            if (IsFinished)
                return;

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

    /// <summary>
    /// Manages a stereo sample buffer for a specific note number.
    /// </summary>
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
