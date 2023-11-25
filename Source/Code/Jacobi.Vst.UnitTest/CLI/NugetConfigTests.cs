using FluentAssertions;
using Jacobi.Vst.CLI;

namespace Jacobi.Vst.UnitTest.CLI;

public class NugetConfigTests
{
    [Fact]
    public void ReadConfig()
    {
        var configPath = TestFile.FullPath("CLI", "TestNuGet.config.xml");
        var cfg = NugetConfig.Load(configPath);

        cfg.PackagePath.Should().Be("PathToGlobalPackages");
    }

    [Fact]
    public void GetNuGetLocation()
    {
        var location = FileExtensions.GetNuGetLocation();
        location.Should().NotBeNullOrEmpty();
    }
}
