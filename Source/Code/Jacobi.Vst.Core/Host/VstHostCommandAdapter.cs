using System;

using Jacobi.Vst.Core;

namespace Jacobi.Vst.Core.Host
{
    public class VstHostCommandAdapter : Plugin.IVstHostCommandStub
    {
        private IVstHostCommandStub _hostCmdStub;

        public VstHostCommandAdapter(IVstHostCommandStub hostCmdStub)
        {
            Throw.IfArgumentIsNull(hostCmdStub, "hostCmdStub");

            _hostCmdStub = hostCmdStub;
        }

        #region IVstHostCommandStub Members

        public bool IsInitialized()
        {
            return (_hostCmdStub != null);
        }

        public bool UpdatePluginInfo(Plugin.VstPluginInfo pluginInfo)
        {
            _hostCmdStub.PluginContext.PluginInfo = pluginInfo;

            return true;
        }

        #endregion

        #region IVstHostCommands20 Members

        public VstTimeInfo GetTimeInfo(VstTimeInfoFlags filterFlags)
        {
            return _hostCmdStub.GetTimeInfo(filterFlags);
        }

        public bool ProcessEvents(VstEvent[] events)
        {
            return _hostCmdStub.ProcessEvents(events);
        }

        public bool IoChanged()
        {
            return _hostCmdStub.IoChanged();
        }

        public bool SizeWindow(int width, int height)
        {
            return _hostCmdStub.SizeWindow(width, height);
        }

        public float GetSampleRate()
        {
            return _hostCmdStub.GetSampleRate();
        }

        public int GetBlockSize()
        {
            return _hostCmdStub.GetBlockSize();
        }

        public int GetInputLatency()
        {
            return _hostCmdStub.GetInputLatency();
        }

        public int GetOutputLatency()
        {
            return _hostCmdStub.GetOutputLatency();
        }

        public VstProcessLevels GetProcessLevel()
        {
            return _hostCmdStub.GetProcessLevel();
        }

        public VstAutomationStates GetAutomationState()
        {
            return _hostCmdStub.GetAutomationState();
        }

        public string GetVendorString()
        {
            return _hostCmdStub.GetVendorString();
        }

        public string GetProductString()
        {
            return _hostCmdStub.GetProductString();
        }

        public int GetVendorVersion()
        {
            return _hostCmdStub.GetVendorVersion();
        }

        public VstCanDoResult CanDo(VstHostCanDo cando)
        {
            return _hostCmdStub.CanDo(cando);
        }

        public VstHostLanguage GetLanguage()
        {
            return _hostCmdStub.GetLanguage();
        }

        public string GetDirectory()
        {
            return _hostCmdStub.GetDirectory();
        }

        public bool UpdateDisplay()
        {
            return _hostCmdStub.UpdateDisplay();
        }

        public bool BeginEdit(int index)
        {
            return _hostCmdStub.BeginEdit(index);
        }

        public bool EndEdit(int index)
        {
            return _hostCmdStub.EndEdit(index);
        }

        public bool OpenFileSelector(VstFileSelect fileSelect)
        {
            return _hostCmdStub.OpenFileSelector(fileSelect);
        }

        public bool CloseFileSelector(VstFileSelect fileSelect)
        {
            return _hostCmdStub.CloseFileSelector(fileSelect);
        }

        #endregion

        #region IVstHostCommands10 Members

        public void SetParameterAutomated(int index, float value)
        {
            _hostCmdStub.SetParameterAutomated(index, value);
        }

        public int GetVersion()
        {
            return _hostCmdStub.GetVersion();
        }

        public int GetCurrentPluginID()
        {
            return _hostCmdStub.GetCurrentPluginID();
        }

        public void ProcessIdle()
        {
            _hostCmdStub.ProcessIdle();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _hostCmdStub = null; 
        }

        #endregion
    }
}
