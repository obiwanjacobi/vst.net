using Jacobi.Vst.Plugin.Framework;
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
