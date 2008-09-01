namespace Jacobi.Vst.Samples.Delay
{
    using System.Collections.ObjectModel;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;
    
    /// <summary>
    /// This class manages all parameters used by the plugin.
    /// </summary>
    internal class PluginParameterFactory
    {
        // we bypass thread safety concerns for now...

        /// <summary>
        /// A collection of parameter categories.
        /// </summary>
        public VstParameterCategoryCollection Categories = new VstParameterCategoryCollection();
        /// <summary>
        /// A collection of parameter definitions.
        /// </summary>
        public VstParameterInfoCollection ParameterInfos = new VstParameterInfoCollection();

        /// <summary>
        /// Initializes the new instance.
        /// </summary>
        public PluginParameterFactory()
        {
            VstParameterCategory paramCat = new VstParameterCategory();
            paramCat.Name = "Delay";

            Categories.Add(paramCat);
        }

        /// <summary>
        /// Fills the <paramref name="parameters"/> collection with all parameters.
        /// </summary>
        /// <param name="parameters">Must not be null.</param>
        /// <remarks>A <see cref="VstParameter"/> instance is created and linked up for each
        /// <see cref="VstParameterInfo"/> instance found in the <see cref="ParameterInfos"/> collection.</remarks>
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
