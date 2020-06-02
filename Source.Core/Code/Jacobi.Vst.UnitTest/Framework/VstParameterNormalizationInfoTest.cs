using Jacobi.Vst.Plugin.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Framework
{
    /// <summary>
    /// This test class verifies the correct normalization of different Parameter value ranges.
    /// </summary>
    [TestClass]
    public class VstParameterNormalizationInfoTest
    {
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

        [TestMethod]
        public void Test_VstParameterNormalizationInfo_ZeroMinInteger()
        {
            var paramInfo = new VstParameterInfo();
            paramInfo.MinInteger = 0;
            paramInfo.MaxInteger = 10;

            AssertNormalizationInfo(paramInfo);
        }

        [TestMethod]
        public void Test_VstParameterNormalizationInfo_PositiveRange()
        {
            var paramInfo = new VstParameterInfo();
            paramInfo.MinInteger = 10;
            paramInfo.MaxInteger = 20;

            AssertNormalizationInfo(paramInfo);
        }

        [TestMethod]
        public void Test_VstParameterNormalizationInfo_NegativeMinInteger()
        {
            var paramInfo = new VstParameterInfo();
            paramInfo.MinInteger = -10;
            paramInfo.MaxInteger = 10;

            AssertNormalizationInfo(paramInfo);
        }

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
