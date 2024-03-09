using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;

namespace VstNetAudioPlugin.Dsp;

/// <summary>
/// Encapsulated delay parameters.
/// </summary>
internal sealed class DelayParameters
{
    private const string ParameterCategoryName = "Delay";

    /// <summary>
    /// Initializes the paramaters for the Delay component.
    /// </summary>
    /// <param name="parameters"></param>
    public DelayParameters(PluginParameters parameters)
    {
        Throw.IfArgumentIsNull(parameters, nameof(parameters));

        InitializeParameters(parameters);
    }

    public VstParameterManager DelayTimeMgr { get; private set; }
    public VstParameterManager FeedbackMgr { get; private set; }
    public VstParameterManager DryLevelMgr { get; private set; }
    public VstParameterManager WetLevelMgr { get; private set; }

    // This method initializes the plugin parameters this Dsp component owns.
    private void InitializeParameters(PluginParameters parameters)
    {
        // all parameter definitions are added to a central list.
        VstParameterInfoCollection parameterInfos = parameters.ParameterInfos;

        // retrieve the category for all delay parameters.
        VstParameterCategory paramCategory =
            parameters.GetParameterCategory(ParameterCategoryName);

        // delay time parameter
        var paramInfo = new VstParameterInfo
        {
            Category = paramCategory,
            CanBeAutomated = true,
            Name = "Time",
            Label = "MilSecs",
            ShortLabel = "ms",
            MinInteger = 0,
            MaxInteger = 1000,
            LargeStepInteger = 100,
            StepInteger = 10,
            DefaultValue = 200f
        };
        DelayTimeMgr = paramInfo
            .Normalize()
            .ToManager();

        parameterInfos.Add(paramInfo);

        // feedback parameter
        paramInfo = new VstParameterInfo
        {
            Category = paramCategory,
            CanBeAutomated = true,
            Name = "Feedbck",
            Label = "Factor",
            ShortLabel = "*",
            LargeStepFloat = 0.1f,
            SmallStepFloat = 0.01f,
            StepFloat = 0.05f,
            DefaultValue = 0.4f
        };
        FeedbackMgr = paramInfo
            .Normalize()
            .ToManager();

        parameterInfos.Add(paramInfo);

        // dry Level parameter
        paramInfo = new VstParameterInfo
        {
            Category = paramCategory,
            CanBeAutomated = true,
            Name = "Dry Lvl",
            Label = "Decibel",
            ShortLabel = "Db",
            LargeStepFloat = 0.1f,
            SmallStepFloat = 0.01f,
            StepFloat = 0.05f,
            DefaultValue = 0.8f
        };
        DryLevelMgr = paramInfo
            .Normalize()
            .ToManager();

        parameterInfos.Add(paramInfo);

        // wet Level parameter
        paramInfo = new VstParameterInfo
        {
            Category = paramCategory,
            CanBeAutomated = true,
            Name = "Wet Lvl",
            Label = "Decibel",
            ShortLabel = "Db",
            LargeStepFloat = 0.1f,
            SmallStepFloat = 0.01f,
            StepFloat = 0.05f,
            DefaultValue = 0.4f
        };
        WetLevelMgr = paramInfo
            .Normalize()
            .ToManager();

        parameterInfos.Add(paramInfo);
    }
}
