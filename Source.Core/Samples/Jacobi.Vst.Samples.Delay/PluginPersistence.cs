namespace Jacobi.Vst.Samples.Delay
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;
    using Jacobi.Vst.Framework.Plugin;
    using Jacobi.Vst.Framework.Plugin.IO;
    

    /// <summary>
    /// This class manages custom persistence for all the plugin's programs and parameters.
    /// </summary>
    internal class PluginPersistence : VstPluginPersistenceBase
    {
        private Plugin _plugin;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public PluginPersistence(Plugin plugin)
        {
            _plugin = plugin;
        }

        protected override VstProgramReaderBase CreateProgramReader(Stream input)
        {
            return new DelayProgramReader(_plugin, input, Encoding);
        }

        private class DelayProgramReader : VstProgramReaderBase
        {
            private Plugin _plugin;

            public DelayProgramReader(Plugin plugin, Stream input, Encoding encoding)
                : base(input, encoding)
            {
                _plugin = plugin;
            }

            protected override VstProgram CreateProgram()
            {    
                VstProgram program = new VstProgram(_plugin.ParameterFactory.Categories);

                _plugin.ParameterFactory.CreateParameters(program.Parameters);

                return program;
            }
        }
    }
}
