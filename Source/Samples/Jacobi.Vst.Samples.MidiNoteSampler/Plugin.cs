using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace Jacobi.Vst.Samples.MidiNoteSampler
{
    /// <summary>
    /// The Plugin root class that derives from the framework provided base class that also include the interface manager.
    /// </summary>
    internal sealed class Plugin : VstPluginWithServices
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Plugin()
                : base("VST.NET Midi Note Sampler",
                    new VstProductInfo("VST.NET Code Samples", "Jacobi Software © 2008-2020", 2000),
                    VstPluginCategory.Synth,
                    VstPluginCapabilities.NoSoundInStop,
                    0,
                    36373435)
        {
            SampleManager = new SampleManager();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddPluginComponent(new AudioProcessor(this));
            services.AddPluginComponent(new MidiProcessor(this));
        }

        /// <summary>
        /// Gets the sample manager.
        /// </summary>
        public SampleManager SampleManager { get; private set; }
    }
}
