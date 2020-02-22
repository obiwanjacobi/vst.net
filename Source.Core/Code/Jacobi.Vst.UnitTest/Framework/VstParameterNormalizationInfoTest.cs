using Jacobi.Vst.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Framework
{
    /// <summary>
    /// This test class verifies the correct normalization of different Parameter value ranges.
    /// </summary>
    [TestClass]
    public class VstParameterNormalizationInfoTest
    {
        private TestContext testContextInstance;

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

        private void AssertNormalizationInfo(VstParameterInfo paramInfo)
        {
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            float actual = paramInfo.NormalizationInfo.GetRawValue(0);
            Assert.AreEqual(paramInfo.MinInteger, actual, "Raw Null value mismatch.");

            actual = paramInfo.NormalizationInfo.GetRawValue(1);
            Assert.AreEqual(paramInfo.MaxInteger, actual, "Raw Max value mismatch.");

            actual = paramInfo.NormalizationInfo.GetNormalizedValue(paramInfo.MinInteger);
            Assert.AreEqual(0, actual, "Normalized Null value mismatch.");

            actual = paramInfo.NormalizationInfo.GetNormalizedValue(paramInfo.MaxInteger);
            Assert.AreEqual(1, actual, "Normalized Max value mismatch.");
        }

        /// <summary>
        /// Tests normalization for a zero min value and a positive max value.
        /// </summary>
        [TestMethod]
        public void Test_VstParameterNormalizationInfo_ZeroMinInteger()
        {
            var paramInfo = new VstParameterInfo();
            paramInfo.MinInteger = 0;
            paramInfo.MaxInteger = 10;

            AssertNormalizationInfo(paramInfo);
        }

        /// <summary>
        /// Tests normalization for a positive min value and a positive max value.
        /// </summary>
        [TestMethod]
        public void Test_VstParameterNormalizationInfo_PositiveRange()
        {
            var paramInfo = new VstParameterInfo();
            paramInfo.MinInteger = 10;
            paramInfo.MaxInteger = 20;

            AssertNormalizationInfo(paramInfo);
        }

        /// <summary>
        /// Tests normalization for a negative min value and a positive max value.
        /// </summary>
        [TestMethod]
        public void Test_VstParameterNormalizationInfo_NegativeMinInteger()
        {
            var paramInfo = new VstParameterInfo();
            paramInfo.MinInteger = -10;
            paramInfo.MaxInteger = 10;

            AssertNormalizationInfo(paramInfo);
        }

        /// <summary>
        /// Tests normalization for a negative min value and a negative max value.
        /// </summary>
        [TestMethod]
        public void Test_VstParameterNormalizationInfo_NegativeRange()
        {
            var paramInfo = new VstParameterInfo();
            paramInfo.MinInteger = -20;
            paramInfo.MaxInteger = -10;

            AssertNormalizationInfo(paramInfo);
        }
    }
}
