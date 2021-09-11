using System;
using System.IO;

namespace Jacobi.Vst.CLI
{
    public static class FileExtensions
    {
        public static string GetNuGetLocation()
        {
            var envPath = Environment.GetEnvironmentVariable("NUGET_PACKAGES");
            if (!String.IsNullOrEmpty(envPath))
                return envPath;

            var userData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var configPath = Path.Combine(userData, "NuGet", "nuget.config");
            if (File.Exists(configPath))
            {
                var cfg = NugetConfig.Load(configPath);
                if (!String.IsNullOrEmpty(cfg.PackagePath))
                    return cfg.PackagePath;
            }

            var userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(userPath, ".nuget", "packages");
        }

        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
