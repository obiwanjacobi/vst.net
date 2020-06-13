
using FluentAssertions;
using Jacobi.Vst.Core;
using Jacobi.Vst.Host.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Interop.Host
{
    /// <summary>
    /// Summary description for VstAudioPrecisionBufferManagerTest
    /// </summary>
    [TestClass]
    public class VstAudioPrecisionBufferManagerTest
    {
        private const int _bufferCount = 24;
        private const int _bufferSize = 1024;
        private const float _testValue = 0xFF;

        private VstAudioPrecisionBufferManager CreateNew()
        {
            var bufferMgr = new VstAudioPrecisionBufferManager(_bufferCount, _bufferSize);

            bufferMgr.BufferCount.Should().Be(_bufferCount);
            bufferMgr.BufferSize.Should().Be(_bufferSize);

            return bufferMgr;
        }

        private VstAudioPrecisionBufferManager CreateNew(float value)
        {
            var bufferMgr = CreateNew();

            foreach (VstAudioPrecisionBuffer buffer in bufferMgr.Buffers)
            {
                for (int i = 0; i < buffer.SampleCount; i++)
                {
                    buffer[i] = value;
                }
            }

            return bufferMgr;
        }

        private void AssertAllBuffersHasValue(VstAudioPrecisionBufferManager bufferMgr, float value)
        {
            foreach (VstAudioPrecisionBuffer buffer in bufferMgr.Buffers)
            {
                AssertBufferHasValue(buffer, value);
            }
        }

        private void AssertBufferHasValue(VstAudioPrecisionBuffer buffer, float value)
        {
            for (int i = 0; i < buffer.SampleCount; i++)
            {
                buffer[i].Should().Be(value);
            }
        }

        [TestMethod]
        public void Test_VstAudioPrecisionBufferManager_Construction()
        {
            var bufferMgr = CreateNew(_testValue);

            AssertAllBuffersHasValue(bufferMgr, _testValue);
        }

        [TestMethod]
        public void Test_VstAudioPrecisionBufferManager_ClearAllBuffers()
        {
            var bufferMgr = CreateNew(_testValue);

            AssertAllBuffersHasValue(bufferMgr, _testValue);

            bufferMgr.ClearAllBuffers();

            AssertAllBuffersHasValue(bufferMgr, 0);
        }

        [TestMethod]
        public void Test_VstAudioPrecisionBufferManager_ClearIndividualBuffers()
        {
            var bufferMgr = CreateNew(_testValue);

            AssertAllBuffersHasValue(bufferMgr, _testValue);

            foreach (VstAudioPrecisionBuffer buffer in bufferMgr.Buffers)
            {
                bufferMgr.ClearBuffer(buffer);
            }

            AssertAllBuffersHasValue(bufferMgr, 0);
        }

        [TestMethod]
        public void Test_VstAudioBufferManager_EnumerateBuffers()
        {
            var bufferMgr = CreateNew(_testValue);

            int counter = 0;
            foreach (VstAudioPrecisionBuffer buffer in bufferMgr.Buffers)
            {
                counter++;
            }

            counter.Should().Be(_bufferCount);
            bufferMgr.Buffers.Should().HaveCount(_bufferCount);
        }
    }
}
