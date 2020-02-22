using Microsoft.VisualStudio.TestTools.UnitTesting;

using Jacobi.Vst.Framework;

namespace Jacobi.Vst.UnitTest.Framework
{
    
    
    /// <summary>
    ///This is a test class for VstProductInfoTest and is intended
    ///to contain all VstProductInfoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VstProductInfoTest
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
        ///A test for VstProductInfo Constructor
        ///</summary>
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
