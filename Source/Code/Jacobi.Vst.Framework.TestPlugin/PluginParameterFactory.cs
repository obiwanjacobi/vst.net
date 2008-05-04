namespace Jacobi.Vst.Framework.TestPlugin
{
    using System.Collections.ObjectModel;
    using Jacobi.Vst.Core;
    
    static class PluginParameterFactory
    {
        // we bypass thread safety concerns for now...
        public static VstParameterCategoryCollection Categories = new VstParameterCategoryCollection();
        public static VstParameterInfoCollection ParameterInfos = new VstParameterInfoCollection();

        static PluginParameterFactory()
        {
            #region Create Parameter Categories
            
            VstParameterCategory paramCat = new VstParameterCategory();
            paramCat.Name = "Category1";

            Categories.Add(paramCat);

            #endregion

            #region Create Parameter Infos

            VstParameterInfo paramInfo = new VstParameterInfo();
            paramInfo.CanBeAutomated = true;
            paramInfo.Label = "Volume";
            paramInfo.ShortLabel = "Vol.";

            paramInfo.SmallStepFloat = 0.001f;
            paramInfo.StepFloat = 0.01f;
            paramInfo.LargeStepFloat = 0.1f;

            ParameterInfos.Add(paramInfo);
            
            #endregion
        }

        public static void CreateParameters(VstParameterCollection parameters)
        {
            foreach (VstParameterInfo paramInfo in ParameterInfos)
            {
                VstParameter param = new VstParameter(paramInfo);

                param.Category = Categories[0];

                if (param.Category != null)
                {
                    param.Name = param.Category.Name + "-" + param.Info.Label;
                }
                else
                {
                    param.Name = param.Info.Label;
                }

                parameters.Add(param);
            }
        }
    }

    internal class VstParameterInfoCollection : Collection<VstParameterInfo>
    {
    }
}
