using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Deprecated;

namespace Jacobi.Vst.UnitTest.Core
{
    
    
    /// <summary>
    ///This is a test class for VstMidiEventTest and is intended
    ///to contain all VstMidiEventTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VstEventTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for VstMidiEvent Constructor
        ///</summary>
        [TestMethod()]
        public void Test_VstMidiEvent_Constructor()
        {
            int deltaFrames = 12;
            int noteLength = 100;
            int noteOffset = 1;
            byte[] midiData = new byte[]{ 0x9C, 0x7F, 0x40 };
            short detune = -24;
            byte noteOffVelocity = 64;
            
            VstMidiEvent me = new VstMidiEvent(deltaFrames, noteLength, noteOffset, midiData, detune, noteOffVelocity);

            Assert.AreEqual(VstEventTypes.MidiEvent, me.EventType, "VstMidiEvent.EventType");
            Assert.AreEqual(deltaFrames, me.DeltaFrames, "VstMidiEvent.DeltaFrames");
            Assert.AreEqual(noteLength, me.NoteLength, "VstMidiEvent.NoteLength");
            Assert.AreEqual(noteOffset, me.NoteOffset, "VstMidiEvent.NoteOffset");
            Assert.IsNotNull(me.Data, "VstMidiEvent.Data is null");
            Assert.AreEqual(3, me.Data.Length, "VstMidiEvent.Data.Length");
            Assert.AreEqual(0x9C, me.Data[0], "VstMidiEvent.Data[0]");
            Assert.AreEqual(0x7F, me.Data[1], "VstMidiEvent.Data[1]");
            Assert.AreEqual(0x40, me.Data[2], "VstMidiEvent.Data[2]");
            Assert.AreEqual(detune, me.Detune, "VstMidiEvent.Detune");
            Assert.AreEqual(noteOffVelocity, me.NoteOffVelocity, "VstMidiEvent.NoteOffVelocity");
        }

        /// <summary>
        ///A test for VstMidiSysExEvent Constructor
        ///</summary>
        [TestMethod()]
        public void Test_VstMidiSysExEvent_Constructor()
        {
            int deltaFrames = 12;
            byte[] sysexData = new byte[] { 0x9C, 0x7F, 0x40 };

            VstMidiSysExEvent me = new VstMidiSysExEvent(deltaFrames, sysexData);

            Assert.AreEqual(VstEventTypes.MidiSysExEvent, me.EventType, "VstMidiSysExEvent.EventType");
            Assert.AreEqual(deltaFrames, me.DeltaFrames, "VstMidiSysExEvent.DeltaFrames");
            Assert.IsNotNull(me.Data, "VstMidiSysExEvent.Data is null");
            Assert.AreEqual(3, me.Data.Length, "VstMidiSysExEvent.Data.Length");
            Assert.AreEqual(0x9C, me.Data[0], "VstMidiSysExEvent.Data[0]");
            Assert.AreEqual(0x7F, me.Data[1], "VstMidiSysExEvent.Data[1]");
            Assert.AreEqual(0x40, me.Data[2], "VstMidiSysExEvent.Data[2]");
        }


        /// <summary>
        ///A test for VstGenericEvent Constructor
        ///</summary>
        [TestMethod()]
        public void Test_VstGenericEvent_Constructor()
        {
            VstEventTypes eventType = VstEventTypes.DeprecatedAudioEvent;
            int deltaFrames = 12;
            byte[] data = new byte[] { 0x9C, 0x7F, 0x40 };

            VstGenericEvent ge = new VstGenericEvent(eventType, deltaFrames, data);

            Assert.AreEqual(eventType, ge.EventType, "VstGenericEvent.EventType");
            Assert.AreEqual(deltaFrames, ge.DeltaFrames, "VstGenericEvent.DeltaFrames");
            Assert.IsNotNull(ge.Data, "VstGenericEvent.Data is null");
            Assert.AreEqual(3, ge.Data.Length, "VstGenericEvent.Data.Length");
            Assert.AreEqual(0x9C, ge.Data[0], "VstGenericEvent.Data[0]");
            Assert.AreEqual(0x7F, ge.Data[1], "VstGenericEvent.Data[1]");
            Assert.AreEqual(0x40, ge.Data[2], "VstGenericEvent.Data[2]");
        }

        /// <summary>
        ///A test for VstGenericEvent Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_VstGenericEvent_MidiEvent()
        {
            VstEventTypes eventType = VstEventTypes.MidiEvent;
            int deltaFrames = 12;
            byte[] data = new byte[] { 0x9C, 0x7F, 0x40 };

            VstGenericEvent ge = new VstGenericEvent(eventType, deltaFrames, data);
            
            Assert.Fail("Should have thrown an excepction.");
        }

        /// <summary>
        ///A test for VstGenericEvent Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_VstGenericEvent_MidiSysExEvent()
        {
            VstEventTypes eventType = VstEventTypes.MidiSysExEvent;
            int deltaFrames = 12;
            byte[] data = new byte[] { 0x9C, 0x7F, 0x40 };

            VstGenericEvent ge = new VstGenericEvent(eventType, deltaFrames, data);

            Assert.Fail("Should have thrown an excepction.");
        }

        /// <summary>
        ///A test for VstGenericEvent Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_VstGenericEvent_Unknown()
        {
            VstEventTypes eventType = VstEventTypes.Unknown;
            int deltaFrames = 12;
            byte[] data = new byte[] { 0x9C, 0x7F, 0x40 };

            VstGenericEvent ge = new VstGenericEvent(eventType, deltaFrames, data);

            Assert.Fail("Should have thrown an excepction.");
        }
    }
}
