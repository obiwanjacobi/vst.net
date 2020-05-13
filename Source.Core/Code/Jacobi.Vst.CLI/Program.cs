using System;
using System.IO;

namespace Jacobi.Vst.CLI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var deployPath = ".\\deploy";
            EnsureDirectoryExists(deployPath);

            var cmd = new PublishCommand
            {
                NuGetPath = GetNuGetLocation(),
                DeployPath = deployPath
            };

            foreach (var fileName in Directory.EnumerateFiles(".", "*.deps.json"))
            {
                Console.WriteLine($"Publishing {fileName}.");
                cmd.Publish(fileName);
            }
        }

        private static string GetNuGetLocation()
        {
            var userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(userPath, ".nuget", "packages");
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
