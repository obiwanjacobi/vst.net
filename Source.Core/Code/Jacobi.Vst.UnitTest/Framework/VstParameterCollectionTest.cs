using Jacobi.Vst.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Framework
{
    /// <summary>
    ///This is a test class for VstParameterCollectionTest and is intended
    ///to contain all VstParameterCollectionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VstParameterCollectionTest
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
        ///A test for VstParameterCollection Constructor
        ///</summary>
        [TestMethod()]
        public void Test_VstParameterCollection_Index()
        {
            var paramInfo1 = new VstParameterInfo();
            paramInfo1.Name = "Test1";
            paramInfo1.ShortLabel = "Tst1";
            paramInfo1.MaxInteger = 10;

            var col = new VstParameterCollection();

            var param1 = new VstParameter(paramInfo1);
            col.Add(param1);

            Assert.AreEqual(0, param1.Index, "Index of param1 is not as expected.");

            var paramInfo2 = new VstParameterInfo();
            paramInfo2.Name = "Test2";
            paramInfo2.ShortLabel = "Tst2";
            paramInfo2.MaxInteger = 10;

            var param2 = new VstParameter(paramInfo2);
            col.Insert(0, param2);

            Assert.AreEqual(0, param2.Index, "Index of param2 is not as expected.");
            Assert.AreEqual(1, param1.Index, "Index of param1 is not as expected.");
        }
    }
}
