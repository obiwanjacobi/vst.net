using Jacobi.Vst.Core.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Vst.UnitTest.Interop.Host
{
    class StubHostCommandStub : IVstHostCommandStub
    {
        public IVstPluginContext PluginContext { get; set; }

        public Vst.Core.VstTimeInfo GetTimeInfo(Vst.Core.VstTimeInfoFlags filterFlags)
        {
            throw new NotImplementedException();
        }

        public bool ProcessEvents(Vst.Core.VstEvent[] events)
        {
            return false;
        }

        public bool IoChanged()
        {
            return false;
        }

        public bool SizeWindow(int width, int height)
        {
            return false;
        }

        public float GetSampleRate()
        {
            throw new NotImplementedException();
        }

        public int GetBlockSize()
        {
            throw new NotImplementedException();
        }

        public int GetInputLatency()
        {
            throw new NotImplementedException();
        }

        public int GetOutputLatency()
        {
            throw new NotImplementedException();
        }

        public Vst.Core.VstProcessLevels GetProcessLevel()
        {
            throw new NotImplementedException();
        }

        public Vst.Core.VstAutomationStates GetAutomationState()
        {
            throw new NotImplementedException();
        }

        public string GetVendorString()
        {
            throw new NotImplementedException();
        }

        public string GetProductString()
        {
            throw new NotImplementedException();
        }

        public int GetVendorVersion()
        {
            throw new NotImplementedException();
        }

        public Vst.Core.VstCanDoResult CanDo(string cando)
        {
            throw new NotImplementedException();
        }

        public Vst.Core.VstHostLanguage GetLanguage()
        {
            throw new NotImplementedException();
        }

        public string GetDirectory()
        {
            throw new NotImplementedException();
        }

        public bool UpdateDisplay()
        {
            throw new NotImplementedException();
        }

        public bool BeginEdit(int index)
        {
            throw new NotImplementedException();
        }

        public bool EndEdit(int index)
        {
            throw new NotImplementedException();
        }

        public bool OpenFileSelector(Vst.Core.VstFileSelect fileSelect)
        {
            throw new NotImplementedException();
        }

        public bool CloseFileSelector(Vst.Core.VstFileSelect fileSelect)
        {
            throw new NotImplementedException();
        }

        public void SetParameterAutomated(int index, float value)
        {
            throw new NotImplementedException();
        }

        public int GetVersion()
        {
            throw new NotImplementedException();
        }

        public int GetCurrentPluginID()
        {
            throw new NotImplementedException();
        }

        public void ProcessIdle()
        {
            throw new NotImplementedException();
        }
    }
}
