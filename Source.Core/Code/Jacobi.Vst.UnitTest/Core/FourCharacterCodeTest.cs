using Jacobi.Vst.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Core
{
    /// <summary>
    ///This is a test class for FourCharacterCodeTest and is intended
    ///to contain all FourCharacterCodeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FourCharacterCodeTest
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
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void Test_FourCharacterCode_Value()
        {
            FourCharacterCode fcc = new FourCharacterCode();
            fcc.Value = "ABCD";
            Assert.AreEqual("ABCD", fcc.Value);
            Assert.AreEqual("ABCD", fcc.ToString());
        }

        /// <summary>
        ///A test for ToInt32
        ///</summary>
        [TestMethod()]
        public void Test_FourCharacterCode_ToInt32()
        {
            FourCharacterCode fcc = new FourCharacterCode("ABCD");
            int value = fcc.ToInt32();
            Assert.AreEqual(0x41424344, value);
        }

        /// <summary>
        ///A test for FourCharacterCode Constructor
        ///</summary>
        [TestMethod()]
        public void Test_FourCharacterCode_ConstructChars()
        {
            FourCharacterCode fcc = new FourCharacterCode('A', 'B', 'C', 'D');
            Assert.AreEqual("ABCD", fcc.ToString());
        }

        /// <summary>
        ///A test for FourCharacterCode Constructor
        ///</summary>
        [TestMethod()]
        public void Test_FourCharacterCode_ConstructString()
        {
            FourCharacterCode fcc = new FourCharacterCode("ABCD");
            Assert.AreEqual("ABCD", fcc.ToString());
        }
    }
}
