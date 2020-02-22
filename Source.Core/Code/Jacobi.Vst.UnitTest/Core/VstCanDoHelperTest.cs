using Microsoft.VisualStudio.TestTools.UnitTesting;

using Jacobi.Vst.Core;

namespace Jacobi.Vst.UnitTest.Core
{
    
    
    /// <summary>
    ///This is a test class for VstCanDoHelperTest and is intended
    ///to contain all VstCanDoHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VstCanDoHelperTest
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
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void Test_VstCanDoHelper_HostCanDo_ToString()
        {
            VstHostCanDo cando = VstHostCanDo.EditFile;
            string actual = VstCanDoHelper.ToString(cando);
            Assert.AreEqual("editFile", actual, "HostCanDo.EditFile failed.");

            cando = VstHostCanDo.SupplyIdle;
            actual = VstCanDoHelper.ToString(cando);
            Assert.AreEqual("supplyIdle", actual, "HostCanDo.SupplyIdle failed.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void Test_VstCanDoHelper_PluginCanDo_ToString()
        {
            VstPluginCanDo cando = VstPluginCanDo.Bypass;
            string actual = VstCanDoHelper.ToString(cando);
            Assert.AreEqual("bypass", actual, "PluginCanDo.Bypass failed.");

            cando = VstPluginCanDo.x1in1out;
            actual = VstCanDoHelper.ToString(cando);
            Assert.AreEqual("1in1out", actual, "PluginCanDo.x1in1out failed.");
        }

        /// <summary>
        ///A test for ParsePluginCanDo
        ///</summary>
        [TestMethod()]
        public void Test_VstCanDoHelper_ParsePluginCanDo()
        {
            string cando = "1in1out";
            VstPluginCanDo actual = VstCanDoHelper.ParsePluginCanDo(cando);
            Assert.AreEqual(VstPluginCanDo.x1in1out, actual, "PluginCanDo.x1in1out failed.");

            cando = "bypass";
            actual = VstCanDoHelper.ParsePluginCanDo(cando);
            Assert.AreEqual(VstPluginCanDo.Bypass, actual, "PluginCanDo.Bypass failed.");
        }

        /// <summary>
        ///A test for ParseHostCanDo
        ///</summary>
        [TestMethod()]
        public void Test_VstCanDoHelper_ParseHostCanDo()
        {
            string cando = "editFile";
            VstHostCanDo actual = VstCanDoHelper.ParseHostCanDo(cando);
            Assert.AreEqual(VstHostCanDo.EditFile, actual, "HostCanDo.EditFile failed.");

            cando = "supplyIdle";
            actual = VstCanDoHelper.ParseHostCanDo(cando);
            Assert.AreEqual(VstHostCanDo.SupplyIdle, actual, "HostCanDo.SupplyIdle failed.");
        }
    }
}
