using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using System;

namespace VstNetAudioPlugin
{
    /// <summary>
    /// This object manages the Plugin programs and its parameters.
    /// </summary>
    internal sealed class PluginPrograms : VstPluginPrograms
    {
        private readonly PluginParameters _parameters;

        /// <summary>
        /// Constructs an instance on the plugin parameter factory
        /// </summary>
        /// <param name="parameters">Must not be null.</param>
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

            var program = CreateProgram("Default");
            programs.Add(program);

            return programs;
        }

        private VstProgram CreateProgram(string name)
        {
            var program = CreateProgram();
            program.Name = name;
            return program;
        }

        // create a program with all parameters.
        private VstProgram CreateProgram()
        {
            var program = new VstProgram(_parameters.Categories);

            CreateParameters(program.Parameters, _parameters.ParameterInfos);

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
            // Advanced: you can derive from VstParameter and add your own properties and logic.
            return new VstParameter(parameterInfo);
        }
    }
}
