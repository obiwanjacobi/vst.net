using FluentAssertions;
using Jacobi.Vst.Core;

namespace Jacobi.Vst.UnitTest.Core
{
    /// <summary>
    ///This is a test class for VstPatchChunkInfoTest and is intended
    ///to contain all VstPatchChunkInfoTest Unit Tests
    ///</summary>
    public class VstPatchChunkInfoTest
    {
        [Fact]
        public void Test_VstPatchChunkInfoConstructor()
        {
            const int version = 1000;
            const int pluginId = 0x41424344;
            const int pluginVersion = 1234;
            const int elementCount = 56;

            var pci = new VstPatchChunkInfo(version, pluginId, pluginVersion, elementCount);

            pci.Version.Should().Be(version);
            pci.PluginID.Should().Be(pluginId);
            pci.PluginVersion.Should().Be(pluginVersion);
            pci.ElementCount.Should().Be(elementCount);
        }
    }
}
