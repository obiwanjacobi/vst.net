using System;

namespace Jacobi.Vst.Framework.Plugin
{
    public abstract class VstPluginProgramsBase : IVstPluginPrograms
    {
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
                    _programs = CreateProgramCollection();
                }

                return _programs;
            }
        }

        private VstProgram _activeProgram;
        /// <summary>
        /// Gets or sets the current or active program.
        /// </summary>
        public virtual VstProgram ActiveProgram
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
                if (_activeProgram != value)
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
        }

        public virtual void BeginSetProgram()
        {
        }

        public virtual void EndSetProgram()
        {
        }

        #endregion

        protected abstract VstProgramCollection CreateProgramCollection();
    }
}
