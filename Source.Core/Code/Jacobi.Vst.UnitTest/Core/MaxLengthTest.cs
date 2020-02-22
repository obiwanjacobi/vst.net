using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Vst.Core;
using System;

namespace Jacobi.Vst.UnitTest.Core
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
        public void Test_MaxLength_VstFileSelect_Title()
        {
            VstFileSelect fs = new VstFileSelect();
            Assert.AreEqual(null, fs.Title);

            fs.Title = String.Empty;
            Assert.AreEqual(String.Empty, fs.Title);

            string testData = CreateString(Constants.MaxFileSelectorTitle);
            fs.Title = testData;
            Assert.AreEqual(testData, fs.Title);

            testData += "X";
            fs.Title = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstFileType_Name()
        {
            VstFileType ft = new VstFileType();
            Assert.AreEqual(null, ft.Name);

            ft.Name = String.Empty;
            Assert.AreEqual(String.Empty, ft.Name);

            string testData = CreateString(Constants.MaxFileTypeName);
            ft.Name = testData;
            Assert.AreEqual(testData, ft.Name);

            testData += "X";
            ft.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstFileType_Extension()
        {
            VstFileType ft = new VstFileType();
            Assert.AreEqual(null, ft.Extension);

            ft.Extension = String.Empty;
            Assert.AreEqual(String.Empty, ft.Extension);

            string testData = CreateString(Constants.MaxFileTypeExtension);
            ft.Extension = testData;
            Assert.AreEqual(testData, ft.Extension);

            testData += "X";
            ft.Extension = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstVstMidiKeyName_Name()
        {
            VstMidiKeyName mkn = new VstMidiKeyName();
            Assert.AreEqual(null, mkn.Name);

            mkn.Name = String.Empty;
            Assert.AreEqual(String.Empty, mkn.Name);

            string testData = CreateString(Constants.MaxMidiNameLength);
            mkn.Name = testData;
            Assert.AreEqual(testData, mkn.Name);

            testData += "X";
            mkn.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstMidiProgramCategory_Name()
        {
            VstMidiProgramCategory mpc = new VstMidiProgramCategory();
            Assert.AreEqual(null, mpc.Name);

            mpc.Name = String.Empty;
            Assert.AreEqual(String.Empty, mpc.Name);

            string testData = CreateString(Constants.MaxMidiNameLength);
            mpc.Name = testData;
            Assert.AreEqual(testData, mpc.Name);

            testData += "X";
            mpc.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstMidiProgramName_Name()
        {
            VstMidiProgramName mpn = new VstMidiProgramName();
            Assert.AreEqual(null, mpn.Name);

            mpn.Name = String.Empty;
            Assert.AreEqual(String.Empty, mpn.Name);

            string testData = CreateString(Constants.MaxMidiNameLength);
            mpn.Name = testData;
            Assert.AreEqual(testData, mpn.Name);

            testData += "X";
            mpn.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterProperties_Label()
        {
            VstParameterProperties mpn = new VstParameterProperties();
            Assert.AreEqual(null, mpn.Label);

            mpn.Label = String.Empty;
            Assert.AreEqual(String.Empty, mpn.Label);

            string testData = CreateString(Constants.MaxLabelLength);
            mpn.Label = testData;
            Assert.AreEqual(testData, mpn.Label);

            testData += "X";
            mpn.Label = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterProperties_ShortLabel()
        {
            VstParameterProperties mpn = new VstParameterProperties();
            Assert.AreEqual(null, mpn.ShortLabel);

            mpn.ShortLabel = String.Empty;
            Assert.AreEqual(String.Empty, mpn.ShortLabel);

            string testData = CreateString(Constants.MaxShortLabelLength);
            mpn.ShortLabel = testData;
            Assert.AreEqual(testData, mpn.ShortLabel);

            testData += "X";
            mpn.ShortLabel = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterProperties_CategoryLabel()
        {
            VstParameterProperties mpn = new VstParameterProperties();
            Assert.AreEqual(null, mpn.CategoryLabel);

            mpn.CategoryLabel = String.Empty;
            Assert.AreEqual(String.Empty, mpn.CategoryLabel);

            string testData = CreateString(Constants.MaxLabelLength);
            mpn.CategoryLabel = testData;
            Assert.AreEqual(testData, mpn.CategoryLabel);

            testData += "X";
            mpn.CategoryLabel = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstPinProperties_Label()
        {
            VstPinProperties pp = new VstPinProperties();
            Assert.AreEqual(null, pp.Label);

            pp.Label = String.Empty;
            Assert.AreEqual(String.Empty, pp.Label);

            string testData = CreateString(Constants.MaxLabelLength);
            pp.Label = testData;
            Assert.AreEqual(testData, pp.Label);

            testData += "X";
            pp.Label = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstPinProperties_ShortLabel()
        {
            VstPinProperties mpn = new VstPinProperties();
            Assert.AreEqual(null, mpn.ShortLabel);

            mpn.ShortLabel = String.Empty;
            Assert.AreEqual(String.Empty, mpn.ShortLabel);

            string testData = CreateString(Constants.MaxShortLabelLength);
            mpn.ShortLabel = testData;
            Assert.AreEqual(testData, mpn.ShortLabel);

            testData += "X";
            mpn.ShortLabel = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstSpeakerProperties_Name()
        {
            VstSpeakerProperties mpn = new VstSpeakerProperties();
            Assert.AreEqual(null, mpn.Name);

            mpn.Name = String.Empty;
            Assert.AreEqual(String.Empty, mpn.Name);

            string testData = CreateString(Constants.MaxMidiNameLength);
            mpn.Name = testData;
            Assert.AreEqual(testData, mpn.Name);

            testData += "X";
            mpn.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }
    }
}
