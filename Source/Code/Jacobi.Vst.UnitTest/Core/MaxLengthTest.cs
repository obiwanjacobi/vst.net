using FluentAssertions;
using Jacobi.Vst.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Vst.UnitTest.Core
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
        public void Test_MaxLength_VstFileSelect_Title()
        {
            var fs = new VstFileSelect();
            fs.Title.Should().BeEmpty();

            fs.Title = String.Empty;
            fs.Title.Should().BeEmpty();

            var testData = CreateString(Constants.MaxFileSelectorTitle);
            fs.Title = testData;
            fs.Title.Should().Be(testData);

            testData += "X";
            fs.Title = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstFileType_Name()
        {
            var ft = new VstFileType();
            ft.Name.Should().BeEmpty();

            ft.Name = String.Empty;
            ft.Name.Should().BeEmpty();

            string testData = CreateString(Constants.MaxFileTypeName);
            ft.Name = testData;
            ft.Name.Should().Be(testData);

            testData += "X";
            ft.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstFileType_Extension()
        {
            var ft = new VstFileType();
            ft.Extension.Should().BeEmpty();

            ft.Extension = String.Empty;
            ft.Extension.Should().BeEmpty();

            string testData = CreateString(Constants.MaxFileTypeExtension);
            ft.Extension = testData;
            ft.Extension.Should().Be(testData);

            testData += "X";
            ft.Extension = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstVstMidiKeyName_Name()
        {
            var mkn = new VstMidiKeyName();
            mkn.Name.Should().BeEmpty();

            mkn.Name = String.Empty;
            mkn.Name.Should().BeEmpty();

            string testData = CreateString(Constants.MaxMidiNameLength);
            mkn.Name = testData;
            mkn.Name.Should().Be(testData);

            testData += "X";
            mkn.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstMidiProgramCategory_Name()
        {
            var mpc = new VstMidiProgramCategory();
            mpc.Name.Should().BeEmpty();

            mpc.Name = String.Empty;
            mpc.Name.Should().BeEmpty();

            string testData = CreateString(Constants.MaxMidiNameLength);
            mpc.Name = testData;
            mpc.Name.Should().Be(testData);

            testData += "X";
            mpc.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstMidiProgramName_Name()
        {
            var mpn = new VstMidiProgramName();
            mpn.Name.Should().BeEmpty();

            mpn.Name = String.Empty;
            mpn.Name.Should().BeEmpty();

            string testData = CreateString(Constants.MaxMidiNameLength);
            mpn.Name = testData;
            mpn.Name.Should().Be(testData);

            testData += "X";
            mpn.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterProperties_Label()
        {
            var mpn = new VstParameterProperties();
            mpn.Label.Should().BeEmpty();

            mpn.Label = String.Empty;
            mpn.Label.Should().BeEmpty();

            string testData = CreateString(Constants.MaxLabelLength);
            mpn.Label = testData;
            mpn.Label.Should().Be(testData);

            testData += "X";
            mpn.Label = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterProperties_ShortLabel()
        {
            var mpn = new VstParameterProperties();
            mpn.ShortLabel.Should().BeEmpty();

            mpn.ShortLabel = String.Empty;
            mpn.ShortLabel.Should().BeEmpty();

            string testData = CreateString(Constants.MaxShortLabelLength);
            mpn.ShortLabel = testData;
            mpn.ShortLabel.Should().Be(testData);

            testData += "X";
            mpn.ShortLabel = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstParameterProperties_CategoryLabel()
        {
            var mpn = new VstParameterProperties();
            mpn.CategoryLabel.Should().BeEmpty();

            mpn.CategoryLabel = String.Empty;
            mpn.CategoryLabel.Should().BeEmpty();

            string testData = CreateString(Constants.MaxLabelLength);
            mpn.CategoryLabel = testData;
            mpn.CategoryLabel.Should().Be(testData);

            testData += "X";
            mpn.CategoryLabel = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstPinProperties_Label()
        {
            var pp = new VstPinProperties();
            pp.Label.Should().BeEmpty();

            pp.Label = String.Empty;
            pp.Label.Should().BeEmpty();

            string testData = CreateString(Constants.MaxLabelLength);
            pp.Label = testData;
            pp.Label.Should().Be(testData);

            testData += "X";
            pp.Label = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstPinProperties_ShortLabel()
        {
            var mpn = new VstPinProperties();
            mpn.ShortLabel.Should().BeEmpty();

            mpn.ShortLabel = String.Empty;
            mpn.ShortLabel.Should().BeEmpty();

            string testData = CreateString(Constants.MaxShortLabelLength);
            mpn.ShortLabel = testData;
            mpn.ShortLabel.Should().Be(testData);

            testData += "X";
            mpn.ShortLabel = testData;
            Assert.Fail("should have thrown an exception.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_MaxLength_VstSpeakerProperties_Name()
        {
            var mpn = new VstSpeakerProperties();
            mpn.Name.Should().BeEmpty();

            mpn.Name = String.Empty;
            mpn.Name.Should().BeEmpty();

            string testData = CreateString(Constants.MaxMidiNameLength);
            mpn.Name = testData;
            mpn.Name.Should().Be(testData);

            testData += "X";
            mpn.Name = testData;
            Assert.Fail("should have thrown an exception.");
        }
    }
}
