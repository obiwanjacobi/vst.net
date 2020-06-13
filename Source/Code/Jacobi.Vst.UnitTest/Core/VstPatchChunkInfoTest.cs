using FluentAssertions;
using Jacobi.Vst.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Core
{
    /// <summary>
    ///This is a test class for VstPatchChunkInfoTest and is intended
    ///to contain all VstPatchChunkInfoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VstPatchChunkInfoTest
    {
        [TestMethod()]
        public void Test_VstPatchChunkInfoConstructor()
        {
            int version = 1000;
            int pluginId = 0x41424344;
            int pluginVersion = 1234;
            int elementCount = 56;

            var pci = new VstPatchChunkInfo(version, pluginId, pluginVersion, elementCount);

            pci.Version.Should().Be(version);
            pci.PluginID.Should().Be(pluginId);
            pci.PluginVersion.Should().Be(pluginVersion);
            pci.ElementCount.Should().Be(elementCount);
        }
    }
}
