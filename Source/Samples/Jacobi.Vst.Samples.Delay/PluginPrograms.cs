namespace Jacobi.Vst.Samples.Delay
{
    using System.Diagnostics;
    using Jacobi.Vst.Framework;

    /// <summary>
    /// This class manages the plugin programs.
    /// </summary>
    class PluginPrograms : IVstPluginPrograms
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

        #region IVstPluginPrograms Members

        private VstProgramCollection _programs;
        /// <summary>
        /// Gets all the programs.
        /// </summary>
        public VstProgramCollection Programs
        {
            get
            {
                if (_programs == null)
                {
                    _programs = new VstProgramCollection();
                    FillPrograms(_programs);
                }

                return _programs;
            }
        }

        private VstProgram _activeProgram;
        /// <summary>
        /// Gets or sets the current or active program.
        /// </summary>
        public VstProgram ActiveProgram
        {
            get
            {
                if (_activeProgram == null && Programs.Count > 0)
                {
                    ActiveProgram = Programs[0];
                }

                return _activeProgram;
            }
            set
            {
                if (_activeProgram != null)
                {
                    _activeProgram.Parameters.Deactivate();
                }

                _activeProgram = value;

                if (_activeProgram != null)
                {
                    _activeProgram.Parameters.Activate();
                }
            }
        }

        /// <summary>
        /// Called by the host (if supported) just before the program is set.
        /// </summary>
        public void BeginSetProgram()
        {
            Trace.WriteLine("BeginSetProgram", "Jacobi.Vst.Framework.TestPlugin");
        }

        /// <summary>
        /// Called by the host (if supported) just after the program is set.
        /// </summary>
        public void EndSetProgram()
        {
            Trace.WriteLine("EndSetProgram", "Jacobi.Vst.Framework.TestPlugin");
        }

        #endregion

        /// <summary>
        /// Initializes the plugin program collection.
        /// </summary>
        /// <param name="programs">A program collection that gets filled.</param>
        private void FillPrograms(VstProgramCollection programs)
        {
            // NOTE: use the new VstProgram constructor that discovers the categories.

            VstProgram prog = new VstProgram();//_plugin.ParameterFactory.Categories);
            prog.Name = "Fx Program 1";
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            prog = new VstProgram();//_plugin.ParameterFactory.Categories);
            prog.Name = "Fx Program 2";
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            prog = new VstProgram();//_plugin.ParameterFactory.Categories);
            prog.Name = "Fx Program 3";
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);
        }

    }
}
