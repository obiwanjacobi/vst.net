﻿using FluentAssertions;
using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using System;

namespace Jacobi.Vst.UnitTest.Framework;

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
    public void Test_MaxLength_VstConnectionInfo_Label()
    {
        var ci = new VstConnectionInfo();
        ci.Label.Should().BeEmpty();

        ci.Label = String.Empty;
        ci.Label.Should().BeEmpty();

        var testData = CreateString(Constants.MaxLabelLength);
        ci.Label = testData;
        ci.Label.Should().Be(testData);

        testData += "X";
        Action err = () => ci.Label = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_MaxLength_VstConnectionInfo_ShortLabel()
    {
        var ci = new VstConnectionInfo();
        ci.ShortLabel.Should().BeEmpty();

        ci.ShortLabel = String.Empty;
        ci.ShortLabel.Should().BeEmpty();

        var testData = CreateString(Constants.MaxShortLabelLength);
        ci.ShortLabel = testData;
        ci.ShortLabel.Should().Be(testData);

        testData += "X";
        Action err = () => ci.ShortLabel = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_MaxLength_VstMidiCategory_Name()
    {
        var mc = new VstMidiCategory();
        mc.Name.Should().BeEmpty();

        mc.Name = String.Empty;
        mc.Name.Should().BeEmpty();

        var testData = CreateString(Constants.MaxMidiNameLength);
        mc.Name = testData;
        mc.Name.Should().Be(testData);

        testData += "X";
        Action err = () => mc.Name = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_MaxLength_VstMidiProgram_Name()
    {
        var mp = new VstMidiProgram();
        mp.Name.Should().BeEmpty();

        mp.Name = String.Empty;
        mp.Name.Should().BeEmpty();

        var testData = CreateString(Constants.MaxMidiNameLength);
        mp.Name = testData;
        mp.Name.Should().Be(testData);

        testData += "X";
        Action err = () => mp.Name = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_MaxLength_VstParameterCategory_Name()
    {
        var pc = new VstParameterCategory();
        pc.Name.Should().BeEmpty();

        pc.Name = String.Empty;
        pc.Name.Should().BeEmpty();

        var testData = CreateString(Constants.MaxCategoryLabelLength);
        pc.Name = testData;
        pc.Name.Should().Be(testData);

        testData += "X";
        Action err = () => pc.Name = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_MaxLength_VstParameterInfo_Name()
    {
        var pi = new VstParameterInfo();
        pi.Name.Should().BeEmpty();

        pi.Name = String.Empty;
        pi.Name.Should().BeEmpty();

        var testData = CreateString(Constants.MaxParameterStringLength);
        pi.Name = testData;
        pi.Name.Should().Be(testData);

        testData += "X";
        Action err = () => pi.Name = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_MaxLength_VstParameterInfo_Label()
    {
        var pi = new VstParameterInfo();
        pi.Label.Should().BeEmpty();

        pi.Label = String.Empty;
        pi.Label.Should().BeEmpty();

        var testData = CreateString(Constants.MaxLabelLength);
        pi.Label = testData;
        pi.Label.Should().Be(testData);

        testData += "X";
        Action err = () => pi.Label = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_MaxLength_VstParameterInfo_ShortLabel()
    {
        var pi = new VstParameterInfo();
        pi.ShortLabel.Should().BeEmpty();

        pi.ShortLabel = String.Empty;
        pi.ShortLabel.Should().BeEmpty();

        var testData = CreateString(Constants.MaxShortLabelLength);
        pi.ShortLabel = testData;
        pi.ShortLabel.Should().Be(testData);

        testData += "X";
        Action err = () => pi.ShortLabel = testData;
        err.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test_MaxLength_VstProgram_Name()
    {
        var p = new VstProgram();
        p.Name.Should().BeEmpty();

        p.Name = String.Empty;
        p.Name.Should().BeEmpty();

        var testData = CreateString(Constants.MaxProgramNameLength);
        p.Name = testData;
        p.Name.Should().Be(testData);

        testData += "X";
        Action err = () => p.Name = testData;
        err.Should().Throw<ArgumentException>();
    }
}
