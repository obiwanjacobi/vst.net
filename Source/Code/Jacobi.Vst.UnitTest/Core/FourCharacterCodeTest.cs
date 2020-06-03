using FluentAssertions;
using Jacobi.Vst.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Core
{
    /// <summary>
    /// This is a test class for FourCharacterCodeTest and is intended
    /// to contain all FourCharacterCodeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FourCharacterCodeTest
    {
        [TestMethod()]
        public void Test_FourCharacterCode_Value()
        {
            FourCharacterCode fcc = new FourCharacterCode
            {
                Value = "ABCD"
            };
            fcc.Value.Should().Be("ABCD");
            fcc.ToString().Should().Be("ABCD");
        }

        [TestMethod()]
        public void Test_FourCharacterCode_ToInt32()
        {
            FourCharacterCode fcc = new FourCharacterCode("ABCD");
            fcc.ToInt32().Should().Be(0x41424344);
        }

        [TestMethod()]
        public void Test_FourCharacterCode_ConstructChars()
        {
            FourCharacterCode fcc = new FourCharacterCode('A', 'B', 'C', 'D');
            fcc.ToString().Should().Be("ABCD");
        }

        [TestMethod()]
        public void Test_FourCharacterCode_ConstructString()
        {
            FourCharacterCode fcc = new FourCharacterCode("ABCD");
            fcc.ToString().Should().Be("ABCD");
        }
    }
}
