using FluentAssertions;
using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Vst.UnitTest.Framework
{
    /// <summary>
    /// Summary description for MaxLengthTest
    /// </summary>
    [TestClass]
    public class MaxLengthTest
    {
        private string CreateString(int length)
        {
            return new string('x', length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstConnectionInfo_Label()
        {
            VstConnectionInfo ci = new VstConnectionInfo();
            ci.Label.Should().BeNull();

            ci.Label = String.Empty;
            ci.Label.Should().BeEmpty();

            string testData = CreateString(Constants.MaxLabelLength);
            ci.Label = testData;
            ci.Label.Should().Be(testData);

            testData += "X";
            ci.Label = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstConnectionInfo_ShortLabel()
        {
            VstConnectionInfo ci = new VstConnectionInfo();
            ci.ShortLabel.Should().BeNull();

            ci.ShortLabel = String.Empty;
            ci.ShortLabel.Should().BeEmpty();

            string testData = CreateString(Constants.MaxShortLabelLength);
            ci.ShortLabel = testData;
            ci.ShortLabel.Should().Be(testData);

            testData += "X";
            ci.ShortLabel = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstMidiCategory_Name()
        {
            VstMidiCategory mc = new VstMidiCategory();
            mc.Name.Should().BeNull();

            mc.Name = String.Empty;
            mc.Name.Should().BeEmpty();

            string testData = CreateString(Constants.MaxMidiNameLength);
            mc.Name = testData;
            mc.Name.Should().Be(testData);

            testData += "X";
            mc.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstMidiProgram_Name()
        {
            VstMidiProgram mp = new VstMidiProgram();
            mp.Name.Should().BeNull();

            mp.Name = String.Empty;
            mp.Name.Should().BeEmpty();

            string testData = CreateString(Constants.MaxMidiNameLength);
            mp.Name = testData;
            mp.Name.Should().Be(testData);

            testData += "X";
            mp.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterCategory_Name()
        {
            VstParameterCategory pc = new VstParameterCategory();
            pc.Name.Should().BeNull();

            pc.Name = String.Empty;
            pc.Name.Should().BeEmpty();

            string testData = CreateString(Constants.MaxCategoryLabelLength);
            pc.Name = testData;
            pc.Name.Should().Be(testData);

            testData += "X";
            pc.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterInfo_Name()
        {
            VstParameterInfo pi = new VstParameterInfo();
            pi.Name.Should().BeNull();

            pi.Name = String.Empty;
            pi.Name.Should().BeEmpty();

            string testData = CreateString(Constants.MaxParameterStringLength);
            pi.Name = testData;
            pi.Name.Should().Be(testData);

            testData += "X";
            pi.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterInfo_Label()
        {
            VstParameterInfo pi = new VstParameterInfo();
            pi.Label.Should().BeNull();

            pi.Label = String.Empty;
            pi.Label.Should().BeEmpty();

            string testData = CreateString(Constants.MaxLabelLength);
            pi.Label = testData;
            pi.Label.Should().Be(testData);

            testData += "X";
            pi.Label = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterInfo_ShortLabel()
        {
            VstParameterInfo pi = new VstParameterInfo();
            pi.ShortLabel.Should().BeNull();

            pi.ShortLabel = String.Empty;
            pi.ShortLabel.Should().BeEmpty();

            string testData = CreateString(Constants.MaxShortLabelLength);
            pi.ShortLabel = testData;
            pi.ShortLabel.Should().Be(testData);

            testData += "X";
            pi.ShortLabel = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstProgram_Name()
        {
            VstProgram p = new VstProgram();
            p.Name.Should().BeNull();

            p.Name = String.Empty;
            p.Name.Should().BeEmpty();

            string testData = CreateString(Constants.MaxProgramNameLength);
            p.Name = testData;
            p.Name.Should().Be(testData);

            testData += "X";
            p.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }
    }
}
