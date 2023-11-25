using FluentAssertions;
using Jacobi.Vst.Core;
using System;

namespace Jacobi.Vst.UnitTest.Core;

/// <summary>
/// Summary description for MaxLengthTest
/// </summary>
public class MaxLengthTest
{
    private string CreateString(int length)
    {
        return new string('x', length);
    }

    [Fact]
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
        Action err = () => fs.Title = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
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
        Action err = () => ft.Name = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
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
        Action err = () => ft.Extension = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
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
        Action err = () => mkn.Name = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
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
        Action err = () => mpc.Name = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
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
        Action err = () => mpn.Name = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
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
        Action err = () => mpn.Label = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
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
        Action err = () => mpn.ShortLabel = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
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
        Action err = () => mpn.CategoryLabel = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
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
        Action err = () => pp.Label = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
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
        Action err = () => mpn.ShortLabel = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
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
        Action err = () => mpn.Name = testData;
        err.Should().Throw<ArgumentException>();
    }
}
