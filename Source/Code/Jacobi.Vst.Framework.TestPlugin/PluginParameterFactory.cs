namespace Jacobi.Vst.Framework.TestPlugin
{
    using Jacobi.Vst.Core;

    static class PluginParameterFactory
    {
        public static void CreateParameters(VstParameterCollection parameters)
        {
            VstParameter invertorParam = new VstParameter();
            invertorParam.CanBeAutomated = true;
            //invertorParam.Label = "Invert Audio";
            //invertorParam.Name = "InvertAudio";
            invertorParam.Properties.Flags = VstParameterPropertiesFlags.ParameterIsSwitch;
            invertorParam.Properties.Label = "Invert";
            //invertorParam.Properties.MaxInteger = 1;
            //invertorParam.Properties.MinInteger = 0;
            //invertorParam.Properties.StepInteger = 1;
            //invertorParam.Properties.DisplayIndex = 0;

            parameters.Add(invertorParam);
        }
    }
}
