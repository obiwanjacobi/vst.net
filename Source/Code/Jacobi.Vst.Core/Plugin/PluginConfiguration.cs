using Microsoft.Extensions.Configuration;

namespace Jacobi.Vst.Core.Plugin
{
    internal static class PluginConfiguration
    {
        public static IConfigurationRoot CreateFrom(string basePath, string name)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(basePath);
            builder.AddJsonFile($"{name}.appsettings.json", optional: true);

            return builder.Build();
        }
    }
}
