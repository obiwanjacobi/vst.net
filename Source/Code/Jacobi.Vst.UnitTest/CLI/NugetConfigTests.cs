using FluentAssertions;
using Jacobi.Vst.CLI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.CLI
{
    [TestClass]
    public class NugetConfigTests
    {
        [TestMethod]
        public void ReadConfig()
        {
            var configPath = TestFile.FullPath("CLI", "TestNuGet.config.xml");
            var cfg = NugetConfig.Load(configPath);

            cfg.PackagePath.Should().Be("PathToGlobalPackages");
        }

        [TestMethod]
        public void GetNuGetLocation()
        {
            var location = FileExtensions.GetNuGetLocation();
            location.Should().NotBeNullOrEmpty();
        }
    }
}
