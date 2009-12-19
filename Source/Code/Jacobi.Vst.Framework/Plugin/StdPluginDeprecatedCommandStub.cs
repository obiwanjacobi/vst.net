namespace Jacobi.Vst.Framework.Plugin
{
    using System;
    using System.Drawing;

    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Deprecated;

    /// <summary>
    /// Implements the Plugin command stub containing the deprecated methods.
    /// </summary>
    /// <remarks>Derive your public plugin command stub class from this class if you need to support older VST versions.
    /// Most methods are not implemented. You should override and implement the specific methods you need to support.</remarks>
    public abstract class StdPluginDeprecatedCommandStub : StdPluginCommandStub, IVstPluginCommandsDeprecated20
    {
        #region IVstPluginCommandsDeprecated20 Members

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns>Always returns zero.</returns>
        public virtual int GetProgramCategoriesCount()
        {
            return 0;
        }

        /// <summary>
        /// Copies the parameter values of the current <see cref="VstProgram"/> to the program indicated by <paramref name="programIndex"/>.
        /// </summary>
        /// <param name="programIndex">A zero-based index into the program collection.</param>
        /// <returns>Returns true when the program parameter values were successfully copied.</returns>
        /// <remarks>The name of the program itself is also copied.</remarks>
        public virtual bool CopyCurrentProgramTo(int programIndex)
        {
            IVstPluginPrograms programs = Plugin.GetInstance<IVstPluginPrograms>();

            if (programs != null && programs.ActiveProgram != null)
            {
                VstProgram targetProgram = programs.Programs[programIndex];
                // targetProgram.Categories is alwasy the same between programs
                targetProgram.Name = programs.ActiveProgram.Name;

                // copy parameter values.
                for (int i = 0; i < programs.ActiveProgram.Parameters.Count; i++)
                {
                    targetProgram.Parameters[i].Value = programs.ActiveProgram.Parameters[i].Value;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="inputIndex">Not used.</param>
        /// <param name="connected">Not used.</param>
        /// <returns>Always returns false.</returns>
        public virtual bool ConnectInput(int inputIndex, bool connected)
        {
            return false;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="outputIndex">Not used.</param>
        /// <param name="connected">Not used.</param>
        /// <returns>Always returns false.</returns>
        public virtual bool ConnectOutput(int outputIndex, bool connected)
        {
            return false;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns>Always returns zero.</returns>
        public virtual int GetCurrentPosition()
        {
            return 0;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns>Always returns null.</returns>
        public virtual VstAudioBuffer GetDestinationBuffer()
        {
            return null;
        }

        /// <summary>
        /// Assigns the <paramref name="blockSize"/> and <paramref name="sampleRate"/> to the audio processor.
        /// </summary>
        /// <param name="blockSize">The number of samples to be expected in each audio processing cycle.</param>
        /// <param name="sampleRate">The nuumber of samples per second.</param>
        /// <returns>Returns true when the information was assigned to the audio processor. 
        /// When the plugin does not implement the audio processor, false is returned.</returns>
        public virtual bool SetBlockSizeAndSampleRate(int blockSize, float sampleRate)
        {
            IVstPluginAudioProcessor audioProcessor = Plugin.GetInstance<IVstPluginAudioProcessor>();

            if (audioProcessor != null)
            {
                audioProcessor.BlockSize = blockSize;
                audioProcessor.SampleRate = sampleRate;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns>Always returns null.</returns>
        public virtual string GetErrorText()
        {
            return null;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns>Always returns false.</returns>
        public virtual bool Idle()
        {
            return false;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns>Always returns null.</returns>
        public virtual Icon GetIcon()
        {
            return null;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="position">Not used.</param>
        /// <returns>Always returns false.</returns>
        public virtual bool SetViewPosition(ref Point position)
        {
            return false;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns>Always returns false.</returns>
        public virtual bool KeysRequired()
        {
            return false;
        }

        #endregion

        #region IVstPluginCommandsDeprecated10 Members

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns>Always returns 0.0.</returns>
        public virtual float GetVu()
        {
            return 0.0f;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="keycode">Not used.</param>
        /// <returns>Always returns false.</returns>
        public virtual bool EditorKey(int keycode)
        {
            return false;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns>Always returns false.</returns>
        public virtual bool EditorTop()
        {
            return false;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <returns>Always returns false.</returns>
        public virtual bool EditorSleep()
        {
            return false;
        }

        /// <summary>
        /// Identifies with 'NvEf'.
        /// </summary>
        /// <returns>Always returns the integer value for 'NvEf'.</returns>
        public virtual int Identify()
        {
            return new FourCharacterCode('N', 'v', 'E', 'f').ToInt32();
        }

        #endregion

        #region IVstPluginCommandsDeprecatedBase Members

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="inputs">Not used.</param>
        /// <param name="outputs">Not used.</param>
        /// <remarks>Method does nothing.</remarks>
        public virtual void ProcessAcc(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            // nop
        }

        #endregion
    }
}
