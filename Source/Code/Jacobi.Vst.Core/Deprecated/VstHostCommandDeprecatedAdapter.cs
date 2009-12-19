namespace Jacobi.Vst.Core.Deprecated
{
    using System;
    using System.Drawing;

    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Host;

    /// <summary>
    /// This class implements an extension to the <see cref="VstHostCommandAdapter"/> to include all depricated Host members.
    /// </summary>
    /// <remarks>
    /// Only instantiate this class when you have a reference to an implementation of the <see cref="IVstHostCommandsDeprecated20"/> interface.
    /// </remarks>
    public class VstHostCommandDeprecatedAdapter : VstHostCommandAdapter, Deprecated.IVstHostCommandsDeprecated20
    {
        private IVstHostCommandsDeprecated20 _deprecatedStub;
        /// <summary>
        /// Constructs a new adapter instance on the passed <paramref name="hostCmdStub"/>.
        /// </summary>
        /// <param name="hostCmdStub">An implementation of the <see cref="IVstHostCommandsDeprecated20"/> interface. Must not be null.</param>
        public VstHostCommandDeprecatedAdapter(IVstHostCommandStub hostCmdStub)
            : base(hostCmdStub)
        {
            _deprecatedStub = (IVstHostCommandsDeprecated20)hostCmdStub;
        }

        #region IVstHostCommandsDeprecated20 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool WantMidi()
        {
            return _deprecatedStub.WantMidi();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <param name="filterFlags">Passed with the forwarded call.</param>
        /// <param name="timeInfo">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetTime(VstTimeInfo timeInfo, VstTimeInfoFlags filterFlags)
        {
            return _deprecatedStub.SetTime(timeInfo, filterFlags);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <param name="sampleIndex">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetTempoAt(int sampleIndex)
        {
            return _deprecatedStub.GetTempoAt(sampleIndex);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetAutomatableParameterCount()
        {
            return _deprecatedStub.GetAutomatableParameterCount();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <param name="parameterIndex">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetParameterQuantization(int parameterIndex)
        {
            return _deprecatedStub.GetParameterQuantization(parameterIndex);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool NeedIdle()
        {
            return _deprecatedStub.NeedIdle();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <param name="pinIndex">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public IntPtr GetPreviousPlugin(int pinIndex)
        {
            return _deprecatedStub.GetPreviousPlugin(pinIndex);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <param name="pinIndex">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public IntPtr GetNextPlugin(int pinIndex)
        {
            return _deprecatedStub.GetNextPlugin(pinIndex);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int WillReplaceOrAccumulate()
        {
            return _deprecatedStub.WillReplaceOrAccumulate();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <param name="sampleRate">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetOutputSampleRate(float sampleRate)
        {
            return _deprecatedStub.SetOutputSampleRate(sampleRate);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstSpeakerArrangement GetOutputSpeakerArrangement()
        {
            return _deprecatedStub.GetOutputSpeakerArrangement();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <param name="icon">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetIcon(Icon icon)
        {
            return _deprecatedStub.SetIcon(icon);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public IntPtr OpenWindow()
        {
            return _deprecatedStub.OpenWindow();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <param name="wnd">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool CloseWindow(IntPtr wnd)
        {
            return _deprecatedStub.CloseWindow(wnd);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <param name="xml">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EditFile(string xml)
        {
            return _deprecatedStub.EditFile(xml);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetChunkFile()
        {
            return _deprecatedStub.GetChunkFile();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstSpeakerArrangement GetInputSpeakerArrangement()
        {
            return _deprecatedStub.GetInputSpeakerArrangement();
        }

        #endregion

        #region IVstHostCommandsDeprecated10 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Deprecated.IVstHostCommandsDeprecated20"/> implementation.
        /// </summary>
        /// <param name="connectionIndex">Passed with the forwarded call.</param>
        /// <param name="output">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool PinConnected(int connectionIndex, bool output)
        {
            return _deprecatedStub.PinConnected(connectionIndex, output);
        }

        #endregion

        /// <summary>
        /// Called to dispose of this instance.
        /// </summary>
        /// <param name="disposing">When true also dispose of managed resources. Otherwise only dispose of unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _deprecatedStub = null;
            }

            base.Dispose(disposing);
        }
    }
}
