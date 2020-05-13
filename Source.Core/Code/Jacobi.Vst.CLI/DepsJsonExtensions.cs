using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jacobi.Vst.CLI
{
    internal static class DepsJsonExtensions
    {
        public static IEnumerable<string> GetFilePaths(this TargetName targetName)
        {
            var paths = new List<string>();

            foreach (var kvp in targetName)
            {
                if (kvp.Value.Runtime.Count > 0)
                {
                    var depInfo = kvp.Value.Runtime.First();
                    if (depInfo.Value.AssemblyVersion != null && depInfo.Value.FileVersion != null)
                    {
                        paths.Add(Path.Combine(kvp.Key, depInfo.Key).Replace("/", "\\"));
                    }
                }
            }

            return paths;
        }
    }
}
