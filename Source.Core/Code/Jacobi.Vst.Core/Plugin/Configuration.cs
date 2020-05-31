using Microsoft.Extensions.Configuration;
using System;

namespace Jacobi.Vst.Core.Plugin
{
    /// <summary>
    /// VST.NET specific plugin configuration options
    /// </summary>
    public sealed class Configuration
    {
        private const string VstNetProbePaths = "vstnetProbePaths";
        private const string VstNetManagedAssemblyName = "vstnetManagedAssemblyName";

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
        public IConfigurationRoot PluginConfig
        {
            get { EnsureConfig(); return _config; }
        }

        /// <summary>
        /// Custom plugin dependency probing paths. Can return null.
        /// </summary>
        public string ProbePaths
        {
            get { return GetAppSetting(VstNetProbePaths); }
        }

        /// <summary>
        /// Non-standard managed plugin assembly name.
        /// </summary>
        public string ManagedAssemblyName
        {
            get { return GetAppSetting(VstNetManagedAssemblyName); }
        }

        private void EnsureConfig()
        {
            if (_config == null)
            {
                _config = new ConfigurationBuilder()
                    //.SetBasePath(_basePath)
                    //.AddJsonFile("vstsettings.json", true)
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
