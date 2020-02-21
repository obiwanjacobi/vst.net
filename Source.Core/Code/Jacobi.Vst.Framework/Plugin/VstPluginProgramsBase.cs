using System;

namespace Jacobi.Vst.Framework.Plugin
{
    /// <summary>
    /// The VstPluginProgramsBase class implements the <see cref="IVstPluginPrograms"/>
    /// interface and provides a basis for implementing Programs in a plugin.
    /// </summary>
    /// <remarks>
    /// The class must be derived and the abstract <see cref="CreateProgramCollection"/>
    /// method must be implemented.
    /// </remarks>
    public abstract class VstPluginProgramsBase : IVstPluginPrograms
    {
        #region IVstPluginPrograms Members

        private VstProgramCollection _programs;
        /// <inheritdoc />
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
        /// <inheritdoc />
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
                        _activeProgram.IsActive = false;
                    }

                    _activeProgram = value;

                    if (_activeProgram != null)
                    {
                        _activeProgram.IsActive = true;
                    }
                }
            }
        }

        /// <inheritdoc />
        public virtual void BeginSetProgram()
        {
        }

        /// <inheritdoc />
        public virtual void EndSetProgram()
        {
        }

        #endregion

        /// <summary>
        /// Called on first access on the <see cref="Programs"/> property and returns
        /// a completely filled collection of Programs.
        /// </summary>
        /// <returns>Never returns null.</returns>
        protected abstract VstProgramCollection CreateProgramCollection();
    }
}
