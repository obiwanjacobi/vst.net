using System;
using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Framework
{
    /// <summary>
    /// Summary description for MaxLengthTest
    /// </summary>
    [TestClass]
    public class MaxLengthTest
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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        private string CreateString(int length)
        {
            return new string('x', length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstConnectionInfo_Label()
        {
            VstConnectionInfo ci = new VstConnectionInfo();
            Assert.AreEqual(null, ci.Label);

            ci.Label = String.Empty;
            Assert.AreEqual(String.Empty, ci.Label);

            string testData = CreateString(Constants.MaxLabelLength);
            ci.Label = testData;
            Assert.AreEqual(testData, ci.Label);

            testData += "X";
            ci.Label = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstConnectionInfo_ShortLabel()
        {
            VstConnectionInfo ci = new VstConnectionInfo();
            Assert.AreEqual(null, ci.ShortLabel);

            ci.ShortLabel = String.Empty;
            Assert.AreEqual(String.Empty, ci.ShortLabel);

            string testData = CreateString(Constants.MaxShortLabelLength);
            ci.ShortLabel = testData;
            Assert.AreEqual(testData, ci.ShortLabel);

            testData += "X";
            ci.ShortLabel = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstMidiCategory_Name()
        {
            VstMidiCategory mc = new VstMidiCategory();
            Assert.AreEqual(null, mc.Name);

            mc.Name = String.Empty;
            Assert.AreEqual(String.Empty, mc.Name);

            string testData = CreateString(Constants.MaxMidiNameLength);
            mc.Name = testData;
            Assert.AreEqual(testData, mc.Name);

            testData += "X";
            mc.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstMidiProgram_Name()
        {
            VstMidiProgram mp = new VstMidiProgram();
            Assert.AreEqual(null, mp.Name);

            mp.Name = String.Empty;
            Assert.AreEqual(String.Empty, mp.Name);

            string testData = CreateString(Constants.MaxMidiNameLength);
            mp.Name = testData;
            Assert.AreEqual(testData, mp.Name);

            testData += "X";
            mp.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterCategory_Name()
        {
            VstParameterCategory pc = new VstParameterCategory();
            Assert.AreEqual(null, pc.Name);

            pc.Name = String.Empty;
            Assert.AreEqual(String.Empty, pc.Name);

            string testData = CreateString(Constants.MaxCategoryLabelLength);
            pc.Name = testData;
            Assert.AreEqual(testData, pc.Name);

            testData += "X";
            pc.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterInfo_Name()
        {
            VstParameterInfo pi = new VstParameterInfo();
            Assert.AreEqual(null, pi.Name);

            pi.Name = String.Empty;
            Assert.AreEqual(String.Empty, pi.Name);

            string testData = CreateString(Constants.MaxParameterStringLength);
            pi.Name = testData;
            Assert.AreEqual(testData, pi.Name);

            testData += "X";
            pi.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterInfo_Label()
        {
            VstParameterInfo pi = new VstParameterInfo();
            Assert.AreEqual(null, pi.Label);

            pi.Label = String.Empty;
            Assert.AreEqual(String.Empty, pi.Label);

            string testData = CreateString(Constants.MaxLabelLength);
            pi.Label = testData;
            Assert.AreEqual(testData, pi.Label);

            testData += "X";
            pi.Label = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterInfo_ShortLabel()
        {
            VstParameterInfo pi = new VstParameterInfo();
            Assert.AreEqual(null, pi.ShortLabel);

            pi.ShortLabel = String.Empty;
            Assert.AreEqual(String.Empty, pi.ShortLabel);

            string testData = CreateString(Constants.MaxShortLabelLength);
            pi.ShortLabel = testData;
            Assert.AreEqual(testData, pi.ShortLabel);

            testData += "X";
            pi.ShortLabel = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstProgram_Name()
        {
            VstProgram p = new VstProgram();
            Assert.AreEqual(null, p.Name);

            p.Name = String.Empty;
            Assert.AreEqual(String.Empty, p.Name);

            string testData = CreateString(Constants.MaxProgramNameLength);
            p.Name = testData;
            Assert.AreEqual(testData, p.Name);

            testData += "X";
            p.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }
    }
}
