namespace Jacobi.Vst.Samples.Delay
{
    using Jacobi.Vst.Framework;
    using Jacobi.Vst.Framework.Plugin;

    /// <summary>
    /// This class manages the plugin programs.
    /// </summary>
    class PluginPrograms : VstPluginProgramsBase
    {
        FxTestPlugin _plugin;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public PluginPrograms(FxTestPlugin plugin)
        {
            _plugin = plugin;
        }

        /// <summary>
        /// Initializes the plugin program collection.
        /// </summary>
        /// <returns>A filled program collection.</returns>
        protected override VstProgramCollection CreateProgramCollection()
        {
            VstProgramCollection programs = new VstProgramCollection();

            VstProgram prog = new VstProgram(_plugin.ParameterFactory.Categories);
            prog.Name = "Fx Program 1";
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            prog = new VstProgram(_plugin.ParameterFactory.Categories);
            prog.Name = "Fx Program 2";
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            prog = new VstProgram(_plugin.ParameterFactory.Categories);
            prog.Name = "Fx Program 3";
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            return programs;
        }
    }
}
