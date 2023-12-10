using Jacobi.Vst.Plugin.Framework;
using System.Linq;
using VstNetMidiPlugin.Dmp;

namespace VstNetMidiPlugin;

/// <summary>
/// A central location for all plugin parameters
/// </summary>
internal sealed class PluginParameters
{
    /// <summary>
    /// Initializes all plugin parameters (one component at a time).
    /// </summary>
    public PluginParameters()
    {
        // register the parameters of all plugin (sub) components
        GainParameters = new GainParameters(this);
        TransposeParameters = new TransposeParameters(this);
    }

    public GainParameters GainParameters { get; private set; }
    public TransposeParameters TransposeParameters { get; private set; }

    /// <summary>
    /// Gets the central list of parameter categories.
    /// </summary>
    public VstParameterCategoryCollection Categories { get; } = new VstParameterCategoryCollection();

    /// <summary>
    /// Gets the central list of parameter definitions.
    /// </summary>
    public VstParameterInfoCollection ParameterInfos { get; } = new VstParameterInfoCollection();

    /// <summary>
    /// Retrieves a parameter category object for the specified <paramref name="categoryName"/>.
    /// </summary>
    /// <param name="categoryName">The name of the parameter category.
    /// Typically the name of a Dsp component, an effect or function.</param>
    /// <returns>Never returns null.</returns>
    public VstParameterCategory GetParameterCategory(string categoryName)
    {
        if (Categories.Contains(categoryName))
        {
            return Categories[categoryName];
        }

        // create a new parameter category object
        var paramCategory = new VstParameterCategory
        {
            Name = categoryName
        };

        Categories.Add(paramCategory);

        return paramCategory;
    }

    /// <summary>
    /// Assigns the <paramref name="hostAutomation"/> to all <see cref="VstParameterManager"/>s.
    /// </summary>
    /// <param name="hostAutomation"></param>
    public void SetHostAutomation(IVstHostAutomation? hostAutomation)
    {
        foreach (var paramMgr in ParameterInfos.Select(p => p.ParameterManager))
        {
            if (paramMgr != null)
                paramMgr.HostAutomation = hostAutomation;
        }
    }
}

