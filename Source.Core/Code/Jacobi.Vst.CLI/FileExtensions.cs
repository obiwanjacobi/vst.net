using System;
using System.IO;

namespace Jacobi.Vst.CLI
{
    internal static class FileExtensions
    {
        public static string GetNuGetLocation()
        {
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
