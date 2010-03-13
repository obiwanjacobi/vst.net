using System;

using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Plugin;

namespace Jacobi.Vst.Samples.WrapperPlugin.Host
{
    public class HostCommandStubAdapter : Jacobi.Vst.Core.Host.IVstHostCommandStub
    {
        IVstHostCommandStub _hostCmdStub;

        public HostCommandStubAdapter(IVstHostCommandStub hostCmdStub)
        {
            _hostCmdStub = hostCmdStub;
        }

        #region IVstHostCommandStub Members

        public Jacobi.Vst.Core.Host.IVstPluginContext PluginContext
        { get; set; }

        #endregion

        #region IVstHostCommands20 Members

        public bool BeginEdit(int index)
        {
            return _hostCmdStub.BeginEdit(index);
        }

        public VstCanDoResult CanDo(string cando)
        {
            return _hostCmdStub.CanDo(cando);
        }

        public bool CloseFileSelector(VstFileSelect fileSelect)
        {
            return _hostCmdStub.CloseFileSelector(fileSelect);
        }

        public bool EndEdit(int index)
        {
            return _hostCmdStub.EndEdit(index);
        }

        public VstAutomationStates GetAutomationState()
        {
            return _hostCmdStub.GetAutomationState();
        }

        public int GetBlockSize()
        {
            return _hostCmdStub.GetBlockSize();
        }

        public string GetDirectory()
        {
            return _hostCmdStub.GetDirectory();
        }

        public int GetInputLatency()
        {
            return _hostCmdStub.GetInputLatency();
        }

        public VstHostLanguage GetLanguage()
        {
            return _hostCmdStub.GetLanguage();
        }

        public int GetOutputLatency()
        {
            return _hostCmdStub.GetOutputLatency();
        }

        public VstProcessLevels GetProcessLevel()
        {
            return _hostCmdStub.GetProcessLevel();
        }

        public string GetProductString()
        {
            return _hostCmdStub.GetProductString();
        }

        public float GetSampleRate()
        {
            return _hostCmdStub.GetSampleRate();
        }

        public VstTimeInfo GetTimeInfo(VstTimeInfoFlags filterFlags)
        {
            return _hostCmdStub.GetTimeInfo(filterFlags);
        }

        public string GetVendorString()
        {
            return _hostCmdStub.GetVendorString();
        }

        public int GetVendorVersion()
        {
            return _hostCmdStub.GetVendorVersion();
        }

        public bool IoChanged()
        {
            return _hostCmdStub.IoChanged();
        }

        public bool OpenFileSelector(VstFileSelect fileSelect)
        {
            return _hostCmdStub.OpenFileSelector(fileSelect);
        }

        public bool ProcessEvents(VstEvent[] events)
        {
            return _hostCmdStub.ProcessEvents(events);
        }

        public bool SizeWindow(int width, int height)
        {
            return _hostCmdStub.SizeWindow(width, height);
        }

        public bool UpdateDisplay()
        {
            return _hostCmdStub.UpdateDisplay();
        }

        #endregion

        #region IVstHostCommands10 Members

        public int GetCurrentPluginID()
        {
            return _hostCmdStub.GetCurrentPluginID();
        }

        public int GetVersion()
        {
            return _hostCmdStub.GetVersion();
        }

        public void ProcessIdle()
        {
             _hostCmdStub.ProcessIdle();
        }

        public void SetParameterAutomated(int index, float value)
        {
             _hostCmdStub.SetParameterAutomated(index, value);
        }

        #endregion
    }
}
