namespace Jacobi.Vst.Framework.TestPlugin
{
    class PluginPrograms : IVstPluginPrograms
    {
        FxTestPlugin _plugin;

        public PluginPrograms(FxTestPlugin plugin)
        {
            _plugin = plugin;
        }

        #region IVstPluginPrograms Members

        private VstProgramCollection _programs;
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

        public void BeginSetProgram()
        {
            
        }

        public void EndSetProgram()
        {
            
        }

        #endregion

        private void FillPrograms(VstProgramCollection programs)
        {
            
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
        }

    }
}
