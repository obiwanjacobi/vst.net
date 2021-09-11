using System.IO;
using System.Reflection;

namespace Jacobi.Vst.UnitTest
{
    internal static class TestFile
    {
        private static string BinFolder =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string FullPath(string folders, string fileName)
            => Path.Combine(BinFolder, folders, fileName);
    }
}
