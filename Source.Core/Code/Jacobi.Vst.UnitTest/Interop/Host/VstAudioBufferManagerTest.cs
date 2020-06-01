
using Jacobi.Vst.Core;
using Jacobi.Vst.Host.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Interop.Host
{
    /// <summary>
    /// Summary description for VstAudioBufferManagerTest
    /// </summary>
    [TestClass]
    public class VstAudioBufferManagerTest
    {
        private const int _bufferCount = 24;
        private const int _bufferSize = 1024;
        private const float _testValue = 0xFF;

        private VstAudioBufferManager CreateNew()
        {
            VstAudioBufferManager bufferMgr = new VstAudioBufferManager(_bufferCount, _bufferSize);

            Assert.AreEqual(_bufferCount, bufferMgr.BufferCount, "Buffer Count was not set correctly.");
            Assert.AreEqual(_bufferSize, bufferMgr.BufferSize, "Buffer Size was not set correctly.");

            return bufferMgr;
        }

        private VstAudioBufferManager CreateNew(float value)
        {
            VstAudioBufferManager bufferMgr = CreateNew();

            foreach (VstAudioBuffer buffer in bufferMgr)
            {
                for (int i = 0; i < buffer.SampleCount; i++)
                {
                    buffer[i] = value;
                }
            }

            return bufferMgr;
        }

        private void AssertAllBuffersHasValue(VstAudioBufferManager bufferMgr, float value)
        {
            foreach (VstAudioBuffer buffer in bufferMgr)
            {
                AssertBufferHasValue(buffer, value);
            }
        }

        private void AssertBufferHasValue(VstAudioBuffer buffer, float value)
        {
            for (int i = 0; i < buffer.SampleCount; i++)
            {
                Assert.AreEqual(value, buffer[i]);
            }
        }

        [TestMethod]
        public void Test_VstAudioBufferManager_Construction()
        {
            VstAudioBufferManager bufferMgr = CreateNew(_testValue);

            AssertAllBuffersHasValue(bufferMgr, _testValue);
        }

        [TestMethod]
        public void Test_VstAudioBufferManager_ClearAllBuffers()
        {
            VstAudioBufferManager bufferMgr = CreateNew(_testValue);

            AssertAllBuffersHasValue(bufferMgr, _testValue);

            bufferMgr.ClearAllBuffers();

            AssertAllBuffersHasValue(bufferMgr, 0);
        }

        [TestMethod]
        public void Test_VstAudioBufferManager_ClearIndividualBuffers()
        {
            VstAudioBufferManager bufferMgr = CreateNew(_testValue);

            AssertAllBuffersHasValue(bufferMgr, _testValue);

            foreach (VstAudioBuffer buffer in bufferMgr)
            {
                bufferMgr.ClearBuffer(buffer);
            }

            AssertAllBuffersHasValue(bufferMgr, 0);
        }

        [TestMethod]
        public void Test_VstAudioBufferManager_EnumerateBuffers()
        {
            VstAudioBufferManager bufferMgr = CreateNew(_testValue);

            int counter = 0;
            foreach (VstAudioBuffer buffer in bufferMgr)
            {
                counter++;
            }

            Assert.AreEqual(_bufferCount, counter, "The number of buffers in the enumerator do not match.");
        }
    }
}
