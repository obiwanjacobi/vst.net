namespace Jacobi.Vst.Core.Host
{
    /// <summary>
    /// The VstHostCommandAdapter class implements the Plugin <see cref="Jacobi.Vst.Core.Plugin.IVstHostCommandStub"/>
    /// interface and forwards those calls to a <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation
    /// provided when the adapter class is constructed.
    /// </summary>
    public class VstHostCommandAdapter : Plugin.IVstHostCommandStub
    {
        private IVstHostCommandStub _hostCmdStub;

        /// <summary>
        /// Constructs a new instance based on the <paramref name="hostCmdStub"/>
        /// </summary>
        /// <param name="hostCmdStub">Will be used to forward calls to. Must not be null.</param>
        public VstHostCommandAdapter(IVstHostCommandStub hostCmdStub)
        {
            Throw.IfArgumentIsNull(hostCmdStub, "hostCmdStub");

            _hostCmdStub = hostCmdStub;
        }

        #region IVstHostCommandStub Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool IsInitialized()
        {
            return (_hostCmdStub != null);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <param name="pluginInfo">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool UpdatePluginInfo(Plugin.VstPluginInfo pluginInfo)
        {
            _hostCmdStub.PluginContext.PluginInfo = pluginInfo;

            return true;
        }

        #endregion

        #region IVstHostCommands20 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <param name="filterFlags">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstTimeInfo GetTimeInfo(VstTimeInfoFlags filterFlags)
        {
            return _hostCmdStub.GetTimeInfo(filterFlags);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <param name="events">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool ProcessEvents(VstEvent[] events)
        {
            return _hostCmdStub.ProcessEvents(events);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool IoChanged()
        {
            return _hostCmdStub.IoChanged();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <param name="width">Passed with the forwarded call.</param>
        /// <param name="height">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SizeWindow(int width, int height)
        {
            return _hostCmdStub.SizeWindow(width, height);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public float GetSampleRate()
        {
            return _hostCmdStub.GetSampleRate();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetBlockSize()
        {
            return _hostCmdStub.GetBlockSize();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetInputLatency()
        {
            return _hostCmdStub.GetInputLatency();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetOutputLatency()
        {
            return _hostCmdStub.GetOutputLatency();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstProcessLevels GetProcessLevel()
        {
            return _hostCmdStub.GetProcessLevel();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstAutomationStates GetAutomationState()
        {
            return _hostCmdStub.GetAutomationState();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetVendorString()
        {
            return _hostCmdStub.GetVendorString();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetProductString()
        {
            return _hostCmdStub.GetProductString();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetVendorVersion()
        {
            return _hostCmdStub.GetVendorVersion();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <param name="cando">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstCanDoResult CanDo(string cando)
        {
            return _hostCmdStub.CanDo(cando);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstHostLanguage GetLanguage()
        {
            return _hostCmdStub.GetLanguage();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetDirectory()
        {
            return _hostCmdStub.GetDirectory();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool UpdateDisplay()
        {
            return _hostCmdStub.UpdateDisplay();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool BeginEdit(int index)
        {
            return _hostCmdStub.BeginEdit(index);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EndEdit(int index)
        {
            return _hostCmdStub.EndEdit(index);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <param name="fileSelect">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool OpenFileSelector(VstFileSelect fileSelect)
        {
            return _hostCmdStub.OpenFileSelector(fileSelect);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool CloseFileSelector(VstFileSelect fileSelect)
        {
            return _hostCmdStub.CloseFileSelector(fileSelect);
        }

        #endregion

        #region IVstHostCommands10 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <param name="value">Passed with the forwarded call.</param>
        public void SetParameterAutomated(int index, float value)
        {
            _hostCmdStub.SetParameterAutomated(index, value);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetVersion()
        {
            return _hostCmdStub.GetVersion();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetCurrentPluginID()
        {
            return _hostCmdStub.GetCurrentPluginID();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        public void ProcessIdle()
        {
            _hostCmdStub.ProcessIdle();
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Clears the reference to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Called to dispose this object instance.
        /// </summary>
        /// <param name="disposing">When true also disposes of managed resources. Otherwise only unmanaged resources are disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hostCmdStub = null;
            }
        }
        #endregion

        /// <summary>
        /// A factory method to create the correct <see cref="VstHostCommandAdapter"/> class type.
        /// </summary>
        /// <param name="hostCmdStub">A reference to the host command stub. Must not be null.</param>
        /// <returns>Returns an instance of <see cref="Deprecated.VstHostCommandDeprecatedAdapter"/> when the <paramref name="hostCmdStub"/> supports deprecated methods.</returns>
        public static VstHostCommandAdapter Create(IVstHostCommandStub hostCmdStub)
        {
            if (hostCmdStub is Deprecated.IVstHostCommandsDeprecated20)
            {
                return new Deprecated.VstHostCommandDeprecatedAdapter(hostCmdStub);
            }

            return new VstHostCommandAdapter(hostCmdStub);
        }
    }
}
