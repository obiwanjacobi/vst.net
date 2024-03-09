using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace Jacobi.Vst.Samples.MidiNoteSampler;

/// <summary>
/// The Plugin root class that derives from the framework provided base class that also include the interface manager.
/// </summary>
internal sealed class Plugin : VstPluginWithServices
{
    /// <summary>
    /// Constructs a new instance.
    /// </summary>
    public Plugin()
        : base("VST.NET 2 Midi Note Sampler", 36373435,
            new VstProductInfo("VST.NET 2 Code Samples", "Jacobi Software © 2008-2024", 2000),
            VstPluginCategory.Synth)
    { }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<SampleManager>()
            .AddSingletonAll<AudioProcessor>()
            .AddSingletonAll<MidiProcessor>();
    }
}
