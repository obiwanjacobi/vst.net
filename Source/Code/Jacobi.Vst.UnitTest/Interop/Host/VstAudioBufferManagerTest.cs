
using FluentAssertions;
using Jacobi.Vst.Core;
using Jacobi.Vst.Host.Interop;

namespace Jacobi.Vst.UnitTest.Interop.Host;

/// <summary>
/// Summary description for VstAudioBufferManagerTest
/// </summary>
public class VstAudioBufferManagerTest
{
    private const int _bufferCount = 24;
    private const int _bufferSize = 1024;
    private const float _testValue = 0xFF;

    private VstAudioBufferManager CreateNew()
    {
        var bufferMgr = new VstAudioBufferManager(_bufferCount, _bufferSize);

        bufferMgr.BufferCount.Should().Be(_bufferCount);
        bufferMgr.BufferSize.Should().Be(_bufferSize);

        return bufferMgr;
    }

    private VstAudioBufferManager CreateNew(float value)
    {
        var bufferMgr = CreateNew();

        foreach (VstAudioBuffer buffer in bufferMgr.Buffers)
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
        foreach (VstAudioBuffer buffer in bufferMgr.Buffers)
        {
            AssertBufferHasValue(buffer, value);
        }
    }

    private void AssertBufferHasValue(VstAudioBuffer buffer, float value)
    {
        for (int i = 0; i < buffer.SampleCount; i++)
        {
            buffer[i].Should().Be(value);
        }
    }

    [Fact]
    public void Test_VstAudioBufferManager_Construction()
    {
        var bufferMgr = CreateNew(_testValue);

        AssertAllBuffersHasValue(bufferMgr, _testValue);
    }

    [Fact]
    public void Test_VstAudioBufferManager_ClearAllBuffers()
    {
        var bufferMgr = CreateNew(_testValue);

        AssertAllBuffersHasValue(bufferMgr, _testValue);

        bufferMgr.ClearAllBuffers();

        AssertAllBuffersHasValue(bufferMgr, 0);
    }

    [Fact]
    public void Test_VstAudioBufferManager_ClearIndividualBuffers()
    {
        var bufferMgr = CreateNew(_testValue);

        AssertAllBuffersHasValue(bufferMgr, _testValue);

        foreach (VstAudioBuffer buffer in bufferMgr.Buffers)
        {
            bufferMgr.ClearBuffer(buffer);
        }

        AssertAllBuffersHasValue(bufferMgr, 0);
    }

    [Fact]
    public void Test_VstAudioBufferManager_EnumerateBuffers()
    {
        var bufferMgr = CreateNew(_testValue);

        int counter = 0;
        foreach (VstAudioBuffer buffer in bufferMgr.Buffers)
        {
            counter++;
        }

        counter.Should().Be(_bufferCount);
        bufferMgr.Buffers.Should().HaveCount(_bufferCount);
    }
}
