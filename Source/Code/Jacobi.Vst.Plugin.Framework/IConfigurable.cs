using Microsoft.Extensions.Configuration;
using System;

namespace Jacobi.Vst.Plugin.Framework
{
    /// <summary>
    /// Plugin Configuration
    /// </summary>
    [CLSCompliant(false)]
    public interface IConfigurable
    {
        /// <summary>
        /// Gets or sets the configuration object.
        /// </summary>
        /// <remarks>Can be null if there is no config file deployed.</remarks>
        public IConfiguration? Configuration { get; set; }
    }
}
