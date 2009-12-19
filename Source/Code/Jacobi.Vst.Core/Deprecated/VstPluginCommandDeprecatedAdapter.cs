namespace Jacobi.Vst.Core.Deprecated
{
    using System;
    using System.Drawing;

    using Jacobi.Vst.Core.Host;

    /// <summary>
    /// This class implements an extension to the <see cref="VstPluginCommandAdapter"/> to include all depricated Host members.
    /// </summary>
    /// <remarks>
    /// Only instantiate this class when you have a reference to an implementation of the <see cref="IVstPluginCommandsDeprecated20"/> interface.
    /// </remarks>
    public class VstPluginCommandDeprecatedAdapter : VstPluginCommandAdapter, IVstPluginCommandsDeprecated20
    {
        private IVstPluginCommandsDeprecated20 _deprecatedStub;

        /// <summary>
        /// Constructs a new instance on the passed <paramref name="pluginCmdStub"/>.
        /// </summary>
        /// <param name="pluginCmdStub">An implementation of the <see cref="IVstPluginCommandsDeprecated20"/> interface. Must not be null.</param>
        public VstPluginCommandDeprecatedAdapter(Plugin.IVstPluginCommandStub pluginCmdStub)
            : base(pluginCmdStub)
        {
            _deprecatedStub = (IVstPluginCommandsDeprecated20)pluginCmdStub;
        }

        #region IVstPluginCommandsDeprecated20 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetProgramCategoriesCount()
        {
            return _deprecatedStub.GetProgramCategoriesCount();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool CopyCurrentProgramTo(int programIndex)
        {
            return _deprecatedStub.CopyCurrentProgramTo(programIndex);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool ConnectInput(int inputIndex, bool connected)
        {
            return _deprecatedStub.ConnectInput(inputIndex, connected);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool ConnectOutput(int outputIndex, bool connected)
        {
            return _deprecatedStub.ConnectOutput(outputIndex, connected);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetCurrentPosition()
        {
            return _deprecatedStub.GetCurrentPosition();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstAudioBuffer GetDestinationBuffer()
        {
            return _deprecatedStub.GetDestinationBuffer();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetBlockSizeAndSampleRate(int blockSize, float sampleRate)
        {
            return _deprecatedStub.SetBlockSizeAndSampleRate(blockSize, sampleRate);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetErrorText()
        {
            return _deprecatedStub.GetErrorText();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool Idle()
        {
            return _deprecatedStub.Idle();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public Icon GetIcon()
        {
            return _deprecatedStub.GetIcon();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetViewPosition(ref Point position)
        {
            return _deprecatedStub.SetViewPosition(ref position);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool KeysRequired()
        {
            return _deprecatedStub.KeysRequired();
        }

        #endregion

        #region IVstPluginCommandsDeprecated10 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public float GetVu()
        {
            return _deprecatedStub.GetVu();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EditorKey(int keycode)
        {
            return _deprecatedStub.EditorKey(keycode);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EditorTop()
        {
            return _deprecatedStub.EditorTop();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EditorSleep()
        {
            return _deprecatedStub.EditorSleep();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int Identify()
        {
            return _deprecatedStub.Identify();
        }

        #endregion

        #region IVstPluginCommandsDeprecatedBase Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstPluginCommandsDeprecated20"/> implementation.
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
