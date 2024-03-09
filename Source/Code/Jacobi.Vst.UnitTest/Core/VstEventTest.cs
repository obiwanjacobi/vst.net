using FluentAssertions;
using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Legacy;
using System;

namespace Jacobi.Vst.UnitTest.Core
{
    /// <summary>
    ///This is a test class for VstMidiEventTest and is intended
    ///to contain all VstMidiEventTest Unit Tests
    ///</summary>
    public class VstEventTest
    {
        [Fact]
        public void Test_VstMidiEvent_Constructor()
        {
            int deltaFrames = 12;
            int noteLength = 100;
            int noteOffset = 1;
            byte[] midiData = new byte[] { 0x9C, 0x7F, 0x40 };
            short detune = -24;
            byte noteOffVelocity = 64;

            var me = new VstMidiEvent(deltaFrames, noteLength, noteOffset, midiData, detune, noteOffVelocity);
            me.EventType.Should().Be(VstEventTypes.MidiEvent);
            me.DeltaFrames.Should().Be(deltaFrames);
            me.NoteLength.Should().Be(noteLength);
            me.NoteOffset.Should().Be(noteOffset);
            me.Data.Should().NotBeNull();
            me.Data.Length.Should().Be(midiData.Length);
            me.Data[0].Should().Be(midiData[0]);
            me.Data[1].Should().Be(midiData[1]);
            me.Data[2].Should().Be(midiData[2]);
            me.Detune.Should().Be(detune);
            me.NoteOffVelocity.Should().Be(noteOffVelocity);
        }

        [Fact]
        public void Test_VstMidiSysExEvent_Constructor()
        {
            int deltaFrames = 12;
            byte[] sysexData = new byte[] { 0x9C, 0x7F, 0x40 };

            var me = new VstMidiSysExEvent(deltaFrames, sysexData);
            me.EventType.Should().Be(VstEventTypes.MidiSysExEvent);
            me.DeltaFrames.Should().Be(deltaFrames);
            me.Data.Should().NotBeNull();
            me.Data.Length.Should().Be(sysexData.Length);
            me.Data[0].Should().Be(sysexData[0]);
            me.Data[1].Should().Be(sysexData[1]);
            me.Data[2].Should().Be(sysexData[2]);
        }

        [Fact]
        public void Test_VstGenericEvent_Constructor()
        {
            var eventType = VstEventTypes.LegacyAudioEvent;
            int deltaFrames = 12;
            byte[] data = new byte[] { 0x9C, 0x7F, 0x40 };

            VstGenericEvent ge = new VstGenericEvent(eventType, deltaFrames, data);

            ge.EventType.Should().Be(eventType);
            ge.DeltaFrames.Should().Be(deltaFrames);
            ge.Data.Should().NotBeNull();
            ge.Data.Length.Should().Be(data.Length);
            ge.Data[0].Should().Be(data[0]);
            ge.Data[1].Should().Be(data[1]);
            ge.Data[2].Should().Be(data[2]);
        }

        [Fact]
        public void Test_VstGenericEvent_MidiEvent()
        {
            VstEventTypes eventType = VstEventTypes.MidiEvent;
            int deltaFrames = 12;
            byte[] data = new byte[] { 0x9C, 0x7F, 0x40 };

            Action err = () => new VstGenericEvent(eventType, deltaFrames, data);

            err.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Test_VstGenericEvent_MidiSysExEvent()
        {
            VstEventTypes eventType = VstEventTypes.MidiSysExEvent;
            int deltaFrames = 12;
            byte[] data = new byte[] { 0x9C, 0x7F, 0x40 };

            Action err = () => new VstGenericEvent(eventType, deltaFrames, data);

            err.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Test_VstGenericEvent_Unknown()
        {
            VstEventTypes eventType = VstEventTypes.Unknown;
            int deltaFrames = 12;
            byte[] data = new byte[] { 0x9C, 0x7F, 0x40 };

            Action err = () => new VstGenericEvent(eventType, deltaFrames, data);

            err.Should().Throw<ArgumentException>();
        }
    }
}
