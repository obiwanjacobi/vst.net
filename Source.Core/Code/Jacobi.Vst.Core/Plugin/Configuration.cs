using Microsoft.Extensions.Configuration;
using System;

namespace Jacobi.Vst.Core.Plugin
{
    /// <summary>
    /// VST.NET specific plugin configuration options
    /// </summary>
    public sealed class Configuration
    {
        private readonly string _basePath;
        private IConfigurationRoot _config;

        /// <summary>
        /// Open configuration based on the plugin folder.
        /// </summary>
        /// <param name="basePath">Must not be null.</param>
        public Configuration(string basePath)
        {
            _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
        }

        /// <summary>
        /// Access the plugin config. Returns null if no config is found.
        /// </summary>
        [CLSCompliant(false)]
        public IConfiguration PluginConfig
        {
            get { EnsureConfig(); return _config; }
        }

        private void EnsureConfig()
        {
            if (_config == null)
            {
                _config = new ConfigurationBuilder()
                    .SetBasePath(_basePath)
                    .AddJsonFile("vstsettings.json", true)
                    .Build();
            }
        }

        private string GetAppSetting(string key)
        {
            EnsureConfig();

            if (_config != null)
            {
                return _config[key];
            }

            return null;
        }

    }
}
