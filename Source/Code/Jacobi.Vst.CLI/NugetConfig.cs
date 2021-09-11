
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Jacobi.Vst.CLI
{
    public sealed class NugetConfig
    {
        public static NugetConfig Load(string filePath)
        {
            var cfg = new NugetConfig();
            cfg.ReadConfig(filePath);
            return cfg;
        }

        public string PackagePath { get; private set; }

        private void ReadConfig(string filePath)
        {
            var xDoc = XDocument.Load(filePath);

            PackagePath = xDoc.XPathSelectElements("configuration/config/add")
                .Where(n => n.Attribute("key")?.Value == "globalPackagesFolder")
                .Select(n => n.Attribute("value"))
                .FirstOrDefault()?.Value;
        }
    }
}