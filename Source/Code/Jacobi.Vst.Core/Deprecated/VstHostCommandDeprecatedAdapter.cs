namespace Jacobi.Vst.Core.Deprecated
{
    using System;
    using System.Drawing;

    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Host;

    public class VstHostCommandDeprecatedAdapter : VstHostCommandAdapter, Deprecated.IVstHostCommandsDeprecated20
    {
        private IVstHostCommandsDeprecated20 _deprecatedStub;

        public VstHostCommandDeprecatedAdapter(IVstHostCommandStub hostCmdStub)
            : base(hostCmdStub)
        {
            _deprecatedStub = (IVstHostCommandsDeprecated20)hostCmdStub;
        }

        #region IVstHostCommandsDeprecated20 Members

        public bool WantMidi()
        {
            return _deprecatedStub.WantMidi();
        }

        public bool SetTime(VstTimeInfo timeInfo, VstTimeInfoFlags filterFlags)
        {
            return _deprecatedStub.SetTime(timeInfo, filterFlags);
        }

        public int GetTempoAt(int sampleIndex)
        {
            return _deprecatedStub.GetTempoAt(sampleIndex);
        }

        public int GetAutomatableParameterCount()
        {
            return _deprecatedStub.GetAutomatableParameterCount();
        }

        public int GetParameterQuantization(int parameterIndex)
        {
            return _deprecatedStub.GetParameterQuantization(parameterIndex);
        }

        public bool NeedIdle()
        {
            return _deprecatedStub.NeedIdle();
        }

        public IntPtr GetPreviousPlugin(int pinIndex)
        {
            return _deprecatedStub.GetPreviousPlugin(pinIndex);
        }

        public IntPtr GetNextPlugin(int pinIndex)
        {
            return _deprecatedStub.GetNextPlugin(pinIndex);
        }

        public int WillReplaceOrAccumulate()
        {
            return _deprecatedStub.WillReplaceOrAccumulate();
        }

        public bool SetOutputSampleRate(float sampleRate)
        {
            return _deprecatedStub.SetOutputSampleRate(sampleRate);
        }

        public VstSpeakerArrangement GetOutputSpeakerArrangement()
        {
            return _deprecatedStub.GetOutputSpeakerArrangement();
        }

        public bool SetIcon(Icon icon)
        {
            return _deprecatedStub.SetIcon(icon);
        }

        public IntPtr OpenWindow()
        {
            return _deprecatedStub.OpenWindow();
        }

        public bool CloseWindow(IntPtr wnd)
        {
            return _deprecatedStub.CloseWindow(wnd);
        }

        public bool EditFile(string xml)
        {
            return _deprecatedStub.EditFile(xml);
        }

        public string GetChunkFile()
        {
            return _deprecatedStub.GetChunkFile();
        }

        public VstSpeakerArrangement GetInputSpeakerArrangement()
        {
            return _deprecatedStub.GetInputSpeakerArrangement();
        }

        #endregion

        #region IVstHostCommandsDeprecated10 Members

        public bool PinConnected(int connectionIndex, bool output)
        {
            return _deprecatedStub.PinConnected(connectionIndex, output);
        }

        #endregion

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
