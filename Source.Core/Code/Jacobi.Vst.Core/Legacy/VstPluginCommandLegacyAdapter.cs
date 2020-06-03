namespace Jacobi.Vst.Core.Legacy
{
    using Jacobi.Vst.Core.Host;
    using System;
    using System.Drawing;

    /// <summary>
    /// This class implements an extension to the <see cref="VstPluginCommandAdapter"/> to include all legacy Host members.
    /// </summary>
    /// <remarks>
    /// Only instantiate this class when you have a reference to an implementation of the <see cref="IVstPluginCommandsLegacy20"/> interface.
    /// </remarks>
    public class VstPluginCommandLegacyAdapter : VstPluginCommandAdapter, IVstPluginCommandsLegacy20
    {
        private readonly IVstPluginCommandsLegacy20 _legacyStub;

        /// <summary>
        /// Constructs a new instance on the passed <paramref name="pluginCmdStub"/>.
        /// </summary>
        /// <param name="pluginCmdStub">An implementation of the <see cref="IVstPluginCommandsLegacy20"/> interface. Must not be null.</param>
        [CLSCompliant(false)]
        public VstPluginCommandLegacyAdapter(Plugin.IVstPluginCommandStub pluginCmdStub)
            : base(pluginCmdStub)
        {
            _legacyStub = (IVstPluginCommandsLegacy20)pluginCmdStub;
        }

        #region IVstPluginCommandsLegacy20 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetProgramCategoriesCount()
        {
            return _legacyStub.GetProgramCategoriesCount();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool CopyCurrentProgramTo(int programIndex)
        {
            return _legacyStub.CopyCurrentProgramTo(programIndex);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool ConnectInput(int inputIndex, bool connected)
        {
            return _legacyStub.ConnectInput(inputIndex, connected);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool ConnectOutput(int outputIndex, bool connected)
        {
            return _legacyStub.ConnectOutput(outputIndex, connected);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetCurrentPosition()
        {
            return _legacyStub.GetCurrentPosition();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstAudioBuffer? GetDestinationBuffer()
        {
            return _legacyStub.GetDestinationBuffer();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetBlockSizeAndSampleRate(int blockSize, float sampleRate)
        {
            return _legacyStub.SetBlockSizeAndSampleRate(blockSize, sampleRate);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetErrorText()
        {
            return _legacyStub.GetErrorText();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool Idle()
        {
            return _legacyStub.Idle();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public IntPtr GetIcon()
        {
            return _legacyStub.GetIcon();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetViewPosition(ref Point position)
        {
            return _legacyStub.SetViewPosition(ref position);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool KeysRequired()
        {
            return _legacyStub.KeysRequired();
        }

        #endregion

        #region IVstPluginCommandsLegacy10 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public float GetVu()
        {
            return _legacyStub.GetVu();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EditorKey(int keycode)
        {
            return _legacyStub.EditorKey(keycode);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EditorTop()
        {
            return _legacyStub.EditorTop();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EditorSleep()
        {
            return _legacyStub.EditorSleep();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int Identify()
        {
            return _legacyStub.Identify();
        }

        #endregion

        #region IVstPluginCommandsLegacyBase Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Legacy.IVstPluginCommandsLegacy20"/> implementation.
        /// </summary>
        /// <param name="inputs">Passed with the forwarded call.</param>
        /// <param name="outputs">Passed with the forwarded call.</param>
        public void ProcessAcc(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            _legacyStub.ProcessAcc(inputs, outputs);
        }

        #endregion
    }
}
