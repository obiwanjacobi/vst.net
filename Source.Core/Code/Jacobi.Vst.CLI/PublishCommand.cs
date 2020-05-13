using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Jacobi.Vst.CLI
{
    internal class PublishCommand
    {
        public string NuGetPath { get; set; }
        public string DeployPath { get; set; }

        public void Publish(string fileName)
        {
            using var stream = File.OpenRead(fileName);
            var json = Parse(stream);
            var paths = json.Targets.First().Value.GetFilePaths();

            foreach (var path in paths)
            {
                var filePath = Path.Combine(NuGetPath, path);
                if (File.Exists(filePath))
                {
                    Console.WriteLine(filePath);
                    File.Copy(filePath, Path.Combine(DeployPath, Path.GetFileName(filePath)), overwrite: true);
                }
            }
        }

        private static DepsJson Parse(Stream stream)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,

            };
            var reader = new StreamReader(stream);
            return JsonSerializer.Deserialize<DepsJson>(reader.ReadToEnd(), options);
        }
    }
}
