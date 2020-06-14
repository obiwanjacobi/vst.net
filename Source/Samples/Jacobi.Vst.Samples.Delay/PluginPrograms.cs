namespace Jacobi.Vst.Samples.Delay
{
    using Jacobi.Vst.Plugin.Framework;
    using Jacobi.Vst.Plugin.Framework.Plugin;

    /// <summary>
    /// This class manages the plugin programs.
    /// </summary>
    internal sealed class PluginPrograms : VstPluginPrograms
    {
        private readonly Plugin _plugin;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public PluginPrograms(Plugin plugin)
        {
            _plugin = plugin;
        }

        /// <summary>
        /// Initializes the plugin program collection.
        /// </summary>
        /// <returns>A filled program collection.</returns>
        protected override VstProgramCollection CreateProgramCollection()
        {
            var programs = new VstProgramCollection();

            var prog = new VstProgram(_plugin.ParameterFactory.Categories)
            {
                Name = "Fx Program 1"
            };
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            prog = new VstProgram(_plugin.ParameterFactory.Categories)
            {
                Name = "Fx Program 2"
            };
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            prog = new VstProgram(_plugin.ParameterFactory.Categories)
            {
                Name = "Fx Program 3"
            };
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            return programs;
        }
    }
}
