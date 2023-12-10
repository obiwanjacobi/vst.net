using Jacobi.Vst.Plugin.Framework;

namespace VstNetMidiPlugin.Dmp;

internal sealed class GainParameters
{
    private const string ParameterCategoryName = "Gain";

    public GainParameters(PluginParameters parameters)
    {
        InitializeParameters(parameters);
    }

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
            Name = "Gain",
            Label = "Db",
            ShortLabel = "Db",
            MinInteger = -100,
            MaxInteger = 100,
            LargeStepFloat = 20.0f,
            SmallStepFloat = 1.0f,
            StepFloat = 10.0f,
            DefaultValue = 0.0f
        };

        GainMgr = paramInfo
            .Normalize()
            .ToManager();

        parameterInfos.Add(paramInfo);
    }

    public VstParameterManager GainMgr { get; private set; }
}
