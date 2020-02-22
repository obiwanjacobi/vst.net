using Microsoft.VisualStudio.TestTools.UnitTesting;

using Jacobi.Vst.Core;

namespace Jacobi.Vst.UnitTest.Core
{
    
    
    /// <summary>
    ///This is a test class for VstPatchChunkInfoTest and is intended
    ///to contain all VstPatchChunkInfoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VstPatchChunkInfoTest
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
        ///A test for VstPatchChunkInfo Constructor
        ///</summary>
        [TestMethod()]
        public void Test_VstPatchChunkInfoConstructor()
        {
            int version = 1000;
            int pluginId = 0x41424344;
            int pluginVersion = 1234;
            int elementCount = 56;
            
            VstPatchChunkInfo pci = new VstPatchChunkInfo(version, pluginId, pluginVersion, elementCount);

            Assert.AreEqual(version, pci.Version, "VstPatchChunkInfo.Version");
            Assert.AreEqual(pluginId, pci.PluginID, "VstPatchChunkInfo.PluginID");
            Assert.AreEqual(pluginVersion, pci.PluginVersion, "VstPatchChunkInfo.PluginVersion");
            Assert.AreEqual(elementCount, pci.ElementCount, "VstPatchChunkInfo.ElementCount");
        }
    }
}
