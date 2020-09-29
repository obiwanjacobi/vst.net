using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using System;

namespace VstNetMidiPlugin
{
    /// <summary>
    /// This object manages the Plugin programs and its parameters.
    /// </summary>
    internal sealed class PluginPrograms : VstPluginPrograms
    {
        private readonly PluginParameters _parameters;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">A reference to the plugin root object.</param>
        public PluginPrograms(PluginParameters parameters)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        /// <summary>
        /// Called to initialize the collection of programs for the plugin.
        /// </summary>
        /// <returns>Never returns null or an empty collection.</returns>
        protected override VstProgramCollection CreateProgramCollection()
        {
            var programs = new VstProgramCollection();

            // TODO: add a number of programs for your plugin.

            VstProgram program = CreateProgram(_parameters.ParameterInfos);
            program.Name = "Default";
            programs.Add(program);

            return programs;
        }

        // create a program with all parameters.
        private VstProgram CreateProgram(VstParameterInfoCollection parameterInfos)
        {
            var program = new VstProgram(_parameters.Categories);

            CreateParameters(program.Parameters, parameterInfos);

            return program;
        }

        // create all parameters
        private void CreateParameters(VstParameterCollection desitnation, VstParameterInfoCollection parameterInfos)
        {
            foreach (VstParameterInfo paramInfo in parameterInfos)
            {
                desitnation.Add(CreateParameter(paramInfo));
            }
        }

        // create one parameter
        private VstParameter CreateParameter(VstParameterInfo parameterInfo)
        {
            var parameter = new VstParameter(parameterInfo);
            return parameter;
        }
    }
}
