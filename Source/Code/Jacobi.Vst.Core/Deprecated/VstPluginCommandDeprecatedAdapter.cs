namespace Jacobi.Vst.Core.Deprecated
{
    using System;
    using System.Drawing;

    using Jacobi.Vst.Core.Host;

    public class VstPluginCommandDeprecatedAdapter : VstPluginCommandAdapter, IVstPluginCommandsDeprecated20
    {
        private IVstPluginCommandsDeprecated20 _deprecatedStub;

        public VstPluginCommandDeprecatedAdapter(Plugin.IVstPluginCommandStub pluginCmdStub)
            : base(pluginCmdStub)
        {
            _deprecatedStub = (IVstPluginCommandsDeprecated20)pluginCmdStub;
        }

        #region IVstPluginCommandsDeprecated20 Members

        public int GetProgramCategoriesCount()
        {
            return _deprecatedStub.GetProgramCategoriesCount();
        }

        public bool CopyCurrentProgramTo(int programIndex)
        {
            return _deprecatedStub.CopyCurrentProgramTo(programIndex);
        }

        public bool ConnectInput(int inputIndex, bool connected)
        {
            return _deprecatedStub.ConnectInput(inputIndex, connected);
        }

        public bool ConnectOutput(int outputIndex, bool connected)
        {
            return _deprecatedStub.ConnectOutput(outputIndex, connected);
        }

        public int GetCurrentPosition()
        {
            return _deprecatedStub.GetCurrentPosition();
        }

        public VstAudioBuffer GetDestinationBuffer()
        {
            return _deprecatedStub.GetDestinationBuffer();
        }

        public bool SetBlockSizeAndSampleRate(int blockSize, float sampleRate)
        {
            return _deprecatedStub.SetBlockSizeAndSampleRate(blockSize, sampleRate);
        }

        public string GetErrorText()
        {
            return _deprecatedStub.GetErrorText();
        }

        public bool Idle()
        {
            return _deprecatedStub.Idle();
        }

        public Icon GetIcon()
        {
            return _deprecatedStub.GetIcon();
        }

        public bool SetViewPosition(ref Point position)
        {
            return _deprecatedStub.SetViewPosition(ref position);
        }

        public bool KeysRequired()
        {
            return _deprecatedStub.KeysRequired();
        }

        #endregion

        #region IVstPluginCommandsDeprecated10 Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public float GetVu()
        {
            return _deprecatedStub.GetVu();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keycode"></param>
        /// <returns></returns>
        public bool EditorKey(int keycode)
        {
            return _deprecatedStub.EditorKey(keycode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EditorTop()
        {
            return _deprecatedStub.EditorTop();
        }

        public bool EditorSleep()
        {
            return _deprecatedStub.EditorSleep();
        }

        public int Identify()
        {
            return _deprecatedStub.Identify();
        }

        #endregion

        #region IVstPluginCommandsDeprecatedBase Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="inputs">Passed with the forwarded call.</param>
        /// <param name="outputs">Passed with the forwarded call.</param>
        public void ProcessAcc(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            _deprecatedStub.ProcessAcc(inputs, outputs);
        }

        #endregion
    }
}
