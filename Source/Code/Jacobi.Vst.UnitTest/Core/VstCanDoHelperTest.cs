using FluentAssertions;
using Jacobi.Vst.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Core
{
    /// <summary>
    ///This is a test class for VstCanDoHelperTest and is intended
    ///to contain all VstCanDoHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VstCanDoHelperTest
    {
        [TestMethod()]
        public void Test_VstCanDoHelper_HostCanDo_ToString()
        {
            VstHostCanDo cando = VstHostCanDo.EditFile;
            var actual = VstCanDoHelper.ToString(cando);
            actual.Should().Be("editFile");

            cando = VstHostCanDo.SupplyIdle;
            actual = VstCanDoHelper.ToString(cando);
            actual.Should().Be("supplyIdle");
        }

        [TestMethod()]
        public void Test_VstCanDoHelper_PluginCanDo_ToString()
        {
            VstPluginCanDo cando = VstPluginCanDo.Bypass;
            var actual = VstCanDoHelper.ToString(cando);
            actual.Should().Be("bypass");

            cando = VstPluginCanDo.x1in1out;
            actual = VstCanDoHelper.ToString(cando);
            actual.Should().Be("1in1out");
        }

        [TestMethod()]
        public void Test_VstCanDoHelper_ParsePluginCanDo()
        {
            string cando = "1in1out";
            var actual = VstCanDoHelper.ParsePluginCanDo(cando);
            actual.Should().Be(VstPluginCanDo.x1in1out);

            cando = "bypass";
            actual = VstCanDoHelper.ParsePluginCanDo(cando);
            actual.Should().Be(VstPluginCanDo.Bypass);
        }

        [TestMethod()]
        public void Test_VstCanDoHelper_ParseHostCanDo()
        {
            string cando = "editFile";
            var actual = VstCanDoHelper.ParseHostCanDo(cando);
            actual.Should().Be(VstHostCanDo.EditFile);

            cando = "supplyIdle";
            actual = VstCanDoHelper.ParseHostCanDo(cando);
            actual.Should().Be(VstHostCanDo.SupplyIdle);
        }
    }
}
