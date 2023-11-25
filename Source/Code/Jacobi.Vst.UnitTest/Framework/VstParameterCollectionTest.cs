using FluentAssertions;
using Jacobi.Vst.Plugin.Framework;

namespace Jacobi.Vst.UnitTest.Framework;

/// <summary>
/// This is a test class for VstParameterCollectionTest and is intended
/// to contain all VstParameterCollectionTest Unit Tests
/// </summary>
public class VstParameterCollectionTest
{
    [Fact]
    public void Test_VstParameterCollection_Index()
    {
        var target = new VstParameterCollection();

        var paramInfo1 = new VstParameterInfo
        {
            Name = "Test1",
            ShortLabel = "Tst1",
            MaxInteger = 10
        };

        var param1 = new VstParameter(paramInfo1);
        target.Add(param1);

        param1.Index.Should().Be(0);

        var paramInfo2 = new VstParameterInfo
        {
            Name = "Test2",
            ShortLabel = "Tst2",
            MaxInteger = 10
        };

        var param2 = new VstParameter(paramInfo2);
        target.Insert(0, param2);

        param1.Index.Should().Be(1);
        param2.Index.Should().Be(0);
    }
}
