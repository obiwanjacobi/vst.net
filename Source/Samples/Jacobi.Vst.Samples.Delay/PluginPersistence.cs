namespace Jacobi.Vst.Samples.Delay
{
    using Jacobi.Vst.Plugin.Framework;
    using Jacobi.Vst.Plugin.Framework.Plugin;
    using Jacobi.Vst.Plugin.Framework.Plugin.IO;
    using System.IO;
    using System.Text;


    /// <summary>
    /// This class manages custom persistence for all the plugin's programs and parameters.
    /// </summary>
    internal sealed class PluginPersistence : VstPluginPersistence
    {
        private readonly Plugin _plugin;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public PluginPersistence(Plugin plugin)
        {
            _plugin = plugin;
        }

        protected override VstProgramReader CreateProgramReader(Stream input)
        {
            return new DelayProgramReader(_plugin, input, Encoding);
        }

        private sealed class DelayProgramReader : VstProgramReader
        {
            private readonly Plugin _plugin;

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
