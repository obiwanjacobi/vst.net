using System;
using System.IO;

namespace Jacobi.Vst.CLI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // var cmdLine = new CommandLineArgs(args);

            var deployPath = @".\deploy";
            EnsureDirectoryExists(deployPath);

            Publish(deployPath, args[0]);
        }

        private static void Publish(string deployPath, string fileName)
        {
            var cmd = new PublishCommand
            {
                NuGetPath = GetNuGetLocation(),
                DeployPath = deployPath,
                FilePath = fileName
            };

            cmd.Execute();
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
