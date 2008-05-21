namespace Jacobi.Vst.Framework.TestPlugin
{
    using System.Collections.ObjectModel;
    using Jacobi.Vst.Core;
    
    internal class PluginParameterFactory
    {
        // we bypass thread safety concerns for now...
        public VstParameterCategoryCollection Categories = new VstParameterCategoryCollection();
        public VstParameterInfoCollection ParameterInfos = new VstParameterInfoCollection();

        public PluginParameterFactory()
        {
            #region Create Parameter Categories
            
            VstParameterCategory paramCat = new VstParameterCategory();
            paramCat.Name = "Delay";

            Categories.Add(paramCat);

            #endregion
        }

        public void CreateParameters(VstParameterCollection parameters)
        {
            foreach (VstParameterInfo paramInfo in ParameterInfos)
            {
                VstParameter param = new VstParameter(paramInfo);

                if (Categories.Count > 0)
                {
                    param.Category = Categories[0];
                }

                parameters.Add(param);
            }
        }
    }
}
