using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace Jacobi.Vst.Samples.Delay;

/// <summary>
/// The Plugin root object.
/// </summary>
internal sealed class Plugin : VstPluginWithServices
{
    /// <summary>A unique plugin id.</summary>
    private static readonly int UniquePluginId = 0x3A3A3A3A;
    /// <summary>The plugin name.</summary>
    private const string PluginName = "VST.NET 2 Delay Sample Plugin";
    /// <summary>The product name.</summary>
    private const string ProductName = "VST.NET 2 Code Samples";
    /// <summary>The vendor name.</summary>
    private const string VendorName = "Jacobi Software © 2008-2024";
    /// <summary>The plugin version.</summary>
    private const int PluginVersion = 2000;
    /// <summary>Delay => Room Effect</summary>
    private const VstPluginCategory PluginCategory = VstPluginCategory.RoomFx;
    /// <summary>Need nothing special.</summary>
    private const VstPluginCapabilities PluginCapabilities = VstPluginCapabilities.None;
    /// <summary>
    /// The number of samples the Delay plugin lags behind.
    /// </summary>
    private const int InitialDelayInSamples = 0;

    /// <summary>
    /// Initializes the one an only instance of the Plugin root object.
    /// </summary>
    public Plugin()
        : base(PluginName, UniquePluginId,
            new VstProductInfo(ProductName, VendorName, PluginVersion),
            PluginCategory, InitialDelayInSamples, PluginCapabilities)
    { }

    /// <summary>
    /// Called once to get all the plugin components.
    /// </summary>
    /// <param name="services">Is never null.</param>
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<PluginParameters>()
            .AddSingletonAll<PluginPrograms>()
            .AddSingletonAll<AudioProcessor>()
            .AddSingletonAll<PluginEditor>();
    }
}
