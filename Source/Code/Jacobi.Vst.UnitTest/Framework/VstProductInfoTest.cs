using FluentAssertions;
using Jacobi.Vst.Plugin.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Framework
{
    /// <summary>
    ///This is a test class for VstProductInfoTest and is intended
    ///to contain all VstProductInfoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VstProductInfoTest
    {
        [TestMethod()]
        public void Test_VstProductInfoConstructor()
        {
            string product = "UnitTestProduct";
            string vendor = "UnitTestVendor";
            int version = 1200;

            var pi = new VstProductInfo(product, vendor, version);

            pi.IsValid.Should().BeTrue();
            pi.Product.Should().Be(product);
            pi.Vendor.Should().Be(vendor);
            pi.Version.Should().Be(version);
            pi.FormattedVersion.Should().Be("1.2.0.0");
        }
    }
}
