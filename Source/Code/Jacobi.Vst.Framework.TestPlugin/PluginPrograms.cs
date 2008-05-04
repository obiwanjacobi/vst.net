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
                    _activeProgram = Programs[0];
                }

                return _activeProgram;
            }
            set { _activeProgram = value; }
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
            VstProgram prog = new VstProgram(PluginParameterFactory.Categories);
            prog.Name = "Fx Program 1";
            PluginParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            prog = new VstProgram(PluginParameterFactory.Categories);
            prog.Name = "Fx Program 2";
            PluginParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            prog = new VstProgram(PluginParameterFactory.Categories);
            prog.Name = "Fx Program 3";
            PluginParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);
        }

    }
}
