using FluentAssertions;
using Jacobi.Vst.Plugin.Framework;

namespace Jacobi.Vst.UnitTest.Framework;

/// <summary>
/// This test class verifies the correct normalization of different Parameter value ranges.
/// </summary>
public class VstParameterNormalizationInfoTest
{
    private void AssertNormalizationInfo(VstParameterInfo paramInfo)
    {
        VstParameterNormalizationInfo.AttachTo(paramInfo);

        paramInfo.NormalizationInfo.Should().NotBeNull();

        float actual = paramInfo.NormalizationInfo!.GetRawValue(0);
        actual.Should().Be(paramInfo.MinInteger);

        actual = paramInfo.NormalizationInfo.GetRawValue(1);
        actual.Should().Be(paramInfo.MaxInteger);

        actual = paramInfo.NormalizationInfo.GetNormalizedValue(paramInfo.MinInteger);
        actual.Should().Be(0);

        actual = paramInfo.NormalizationInfo.GetNormalizedValue(paramInfo.MaxInteger);
        actual.Should().Be(1);
    }

    [Fact]
    public void Test_VstParameterNormalizationInfo_ZeroMinInteger()
    {
        var paramInfo = new VstParameterInfo
        {
            MinInteger = 0,
            MaxInteger = 10
        };

        AssertNormalizationInfo(paramInfo);
    }

    [Fact]
    public void Test_VstParameterNormalizationInfo_PositiveRange()
    {
        var paramInfo = new VstParameterInfo
        {
            MinInteger = 10,
            MaxInteger = 20
        };

        AssertNormalizationInfo(paramInfo);
    }

    [Fact]
    public void Test_VstParameterNormalizationInfo_NegativeMinInteger()
    {
        var paramInfo = new VstParameterInfo
        {
            MinInteger = -10,
            MaxInteger = 10
        };

        AssertNormalizationInfo(paramInfo);
    }

    [Fact]
    public void Test_VstParameterNormalizationInfo_NegativeRange()
    {
        var paramInfo = new VstParameterInfo
        {
            MinInteger = -20,
            MaxInteger = -10
        };

        AssertNormalizationInfo(paramInfo);
    }
}
