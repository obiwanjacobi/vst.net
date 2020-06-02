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

            VstProductInfo pi = new VstProductInfo(product, vendor, version);

            Assert.IsTrue(pi.IsValid, "VstProductInfo.IsValid");
            Assert.AreEqual(product, pi.Product, "VstProductInfo.Product");
            Assert.AreEqual(vendor, pi.Vendor, "VstProductInfo.Vendor");
            Assert.AreEqual(version, pi.Version, "VstProductInfo.Version");
            Assert.AreEqual("1.2.0.0", pi.FormattedVersion, "VstProductInfo.FormattedVersion");
        }
    }
}
