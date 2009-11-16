namespace Jacobi.Vst.Framework.Plugin
{
    using System;
    using System.Drawing;

    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Deprecated;

    public abstract class StdPluginDeprecatedCommandStub : StdPluginCommandStub, IVstPluginCommandsDeprecated20
    {
        #region IVstPluginCommandsDeprecated20 Members

        public virtual int GetProgramCategoriesCount()
        {
            return 0;
        }

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

        public virtual bool ConnectInput(int inputIndex, bool connected)
        {
            return false;
        }

        public virtual bool ConnectOutput(int outputIndex, bool connected)
        {
            return false;
        }

        public virtual int GetCurrentPosition()
        {
            return 0;
        }

        public virtual VstAudioBuffer GetDestinationBuffer()
        {
            return null;
        }

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

        public virtual string GetErrorText()
        {
            return null;
        }

        public virtual bool Idle()
        {
            return false;
        }

        public virtual Icon GetIcon()
        {
            return null;
        }

        public virtual bool SetViewPosition(ref Point position)
        {
            return false;
        }

        public virtual bool KeysRequired()
        {
            return false;
        }

        #endregion

        #region IVstPluginCommandsDeprecated10 Members

        public virtual float GetVu()
        {
            return 0.0f;
        }

        public virtual bool EditorKey(int keycode)
        {
            return false;
        }

        public virtual bool EditorTop()
        {
            return false;
        }

        public virtual bool EditorSleep()
        {
            return false;
        }

        public virtual int Identify()
        {
            return new FourCharacterCode('N', 'v', 'E', 'f').ToInt32();
        }

        #endregion

        #region IVstPluginCommandsDeprecatedBase Members

        public virtual void ProcessAcc(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            // nop
        }

        #endregion
    }
}
