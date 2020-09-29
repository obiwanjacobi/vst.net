using Jacobi.Vst.Plugin.Framework;

namespace VstNetMidiPlugin.Dmp
{
    internal sealed class TransposeParameters
    {
        private const string ParameterCategoryName = "Transpose";

        public TransposeParameters(PluginParameters parameters)
        {
            InitializeParameters(parameters);
        }

        public VstParameterManager TransposeMgr { get; private set; }

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
                Name = "Transp.",
                Label = "Halfs",
                ShortLabel = "#",
                MinInteger = -100,
                MaxInteger = 100,
                LargeStepFloat = 5.0f,
                SmallStepFloat = 1.0f,
                StepFloat = 2.0f,
                DefaultValue = 0.0f
            };

            TransposeMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);
        }
    }
}
