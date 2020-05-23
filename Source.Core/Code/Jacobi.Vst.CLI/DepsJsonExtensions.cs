using System.Collections.Generic;
using System.IO;

namespace Jacobi.Vst.CLI
{
    internal static class DepsJsonExtensions
    {
        public static IEnumerable<string> GetFilePaths(this TargetName targetName)
        {
            var paths = new List<string>();

            foreach (var kvp in targetName)
            {
                foreach (var depInfo in kvp.Value.Runtime)
                {
                    if (depInfo.Value.AssemblyVersion != null ||
                        depInfo.Value.FileVersion != null)
                    {
                        paths.Add(Path.Combine(kvp.Key, depInfo.Key).Replace("/", "\\"));
                    }
                }
            }

            return paths;
        }
    }
}
