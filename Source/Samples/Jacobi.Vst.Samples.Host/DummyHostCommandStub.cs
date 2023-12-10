﻿using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using System;

namespace Jacobi.Vst.Samples.Host;

/// <summary>
/// The HostCommandStub class represents the part of the host that a plugin can call.
/// This is a dummy implementation that simply logs each call to a delegate.
/// </summary>
internal sealed class DummyHostCommandStub : IVstHostCommandStub
{
    public DummyHostCommandStub()
    {
        Commands = new DummyHostCommands(this);
    }

    /// <summary>
    /// Only there to show a log on the screen.
    /// You would not normally do this, instead handle each call directly.
    /// </summary>
    public event EventHandler<PluginCalledEventArgs> PluginCalled;

    private void RaisePluginCalled(string message)
    {
        PluginCalled?.Invoke(this, new PluginCalledEventArgs(message));
    }

    /// <summary>
    /// Attached to the EditorFrame for a plugin.
    /// </summary>
    public event EventHandler<SizeWindowEventArgs> SizeWindow;

    private void RaiseSizeWindow(int width, int height)
    {
        SizeWindow?.Invoke(this, new SizeWindowEventArgs(width, height));
    }

    #region IVstHostCommandsStub Members

    /// <inheritdoc />
    public IVstPluginContext PluginContext { get; set; }

    public IVstHostCommands20 Commands { get; private set; }

    #endregion

    private sealed class DummyHostCommands : IVstHostCommands20
    {
        private readonly DummyHostCommandStub _cmdStub;

        public DummyHostCommands(DummyHostCommandStub cmdStub)
        {
            _cmdStub = cmdStub;
        }

        #region IVstHostCommands20 Members

        /// <inheritdoc />
        public bool BeginEdit(int index)
        {
            _cmdStub.RaisePluginCalled("BeginEdit(" + index + ")");
            return false;
        }

        /// <inheritdoc />
        public Jacobi.Vst.Core.VstCanDoResult CanDo(string cando)
        {
            _cmdStub.RaisePluginCalled("CanDo(" + cando + ")");
            return Jacobi.Vst.Core.VstCanDoResult.Unknown;
        }

        /// <inheritdoc />
        public bool CloseFileSelector(Jacobi.Vst.Core.VstFileSelect fileSelect)
        {
            _cmdStub.RaisePluginCalled("CloseFileSelector(" + fileSelect.Command + ")");
            return false;
        }

        /// <inheritdoc />
        public bool EndEdit(int index)
        {
            _cmdStub.RaisePluginCalled("EndEdit(" + index + ")");
            return false;
        }

        /// <inheritdoc />
        public Jacobi.Vst.Core.VstAutomationStates GetAutomationState()
        {
            _cmdStub.RaisePluginCalled("GetAutomationState()");
            return Jacobi.Vst.Core.VstAutomationStates.Off;
        }

        /// <inheritdoc />
        public int GetBlockSize()
        {
            _cmdStub.RaisePluginCalled("GetBlockSize()");
            return 1024;
        }

        /// <inheritdoc />
        public string GetDirectory()
        {
            _cmdStub.RaisePluginCalled("GetDirectory()");
            return null;
        }

        /// <inheritdoc />
        public int GetInputLatency()
        {
            _cmdStub.RaisePluginCalled("GetInputLatency()");
            return 0;
        }

        /// <inheritdoc />
        public Jacobi.Vst.Core.VstHostLanguage GetLanguage()
        {
            _cmdStub.RaisePluginCalled("GetLanguage()");
            return Jacobi.Vst.Core.VstHostLanguage.NotSupported;
        }

        /// <inheritdoc />
        public int GetOutputLatency()
        {
            _cmdStub.RaisePluginCalled("GetOutputLatency()");
            return 0;
        }

        /// <inheritdoc />
        public Jacobi.Vst.Core.VstProcessLevels GetProcessLevel()
        {
            _cmdStub.RaisePluginCalled("GetProcessLevel()");
            return Jacobi.Vst.Core.VstProcessLevels.Unknown;
        }

        /// <inheritdoc />
        public string GetProductString()
        {
            _cmdStub.RaisePluginCalled("GetProductString()");
            return "VST.NET";
        }

        /// <inheritdoc />
        public float GetSampleRate()
        {
            _cmdStub.RaisePluginCalled("GetSampleRate()");
            return 44.8f;
        }

        /// <inheritdoc />
        public Jacobi.Vst.Core.VstTimeInfo GetTimeInfo(Jacobi.Vst.Core.VstTimeInfoFlags filterFlags)
        {
            _cmdStub.RaisePluginCalled("GetTimeInfo(" + filterFlags + ")");
            return null;
        }

        /// <inheritdoc />
        public string GetVendorString()
        {
            _cmdStub.RaisePluginCalled("GetVendorString()");
            return "Jacobi Software";
        }

        /// <inheritdoc />
        public int GetVendorVersion()
        {
            _cmdStub.RaisePluginCalled("GetVendorVersion()");
            return 1000;
        }

        /// <inheritdoc />
        public bool IoChanged()
        {
            _cmdStub.RaisePluginCalled("IoChanged()");
            return false;
        }

        /// <inheritdoc />
        public bool OpenFileSelector(Jacobi.Vst.Core.VstFileSelect fileSelect)
        {
            _cmdStub.RaisePluginCalled("OpenFileSelector(" + fileSelect.Command + ")");
            return false;
        }

        /// <inheritdoc />
        public bool ProcessEvents(Jacobi.Vst.Core.VstEvent[] events)
        {
            _cmdStub.RaisePluginCalled("ProcessEvents(" + events.Length + ")");
            return false;
        }

        /// <inheritdoc />
        public bool SizeWindow(int width, int height)
        {
            _cmdStub.RaisePluginCalled("SizeWindow(" + width + ", " + height + ")");
            _cmdStub.RaiseSizeWindow(width, height);
            return false;
        }

        /// <inheritdoc />
        public bool UpdateDisplay()
        {
            _cmdStub.RaisePluginCalled("UpdateDisplay()");
            return false;
        }

        #endregion

        #region IVstHostCommands10 Members

        /// <inheritdoc />
        public int GetCurrentPluginID()
        {
            _cmdStub.RaisePluginCalled("GetCurrentPluginID()");
            // this is the plugin Id the host wants to load
            // for shell plugins (a plugin that hosts other plugins)
            return 0;
        }

        /// <inheritdoc />
        public int GetVersion()
        {
            _cmdStub.RaisePluginCalled("GetVersion()");
            return 1000;
        }

        /// <inheritdoc />
        public void ProcessIdle()
        {
            _cmdStub.RaisePluginCalled("ProcessIdle()");
        }

        /// <inheritdoc />
        public void SetParameterAutomated(int index, float value)
        {
            _cmdStub.RaisePluginCalled("SetParameterAutomated(" + index + ", " + value + ")");
        }

        #endregion
    }
}

/// <summary>
/// Event arguments used when one of the methods is called.
/// </summary>
internal sealed class PluginCalledEventArgs : EventArgs
{
    /// <summary>
    /// Constructs a new instance with a <paramref name="message"/>.
    /// </summary>
    /// <param name="message"></param>
    public PluginCalledEventArgs(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Gets the message.
    /// </summary>
    public string Message { get; private set; }
}

/// <summary>
/// Event arguments used when the SizeWindow method is called.
/// </summary>
internal sealed class SizeWindowEventArgs : EventArgs
{
    /// <summary>
    /// Constructs a new instance with a <paramref name="message"/>.
    /// </summary>
    /// <param name="message"></param>
    public SizeWindowEventArgs(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int Width { get; }
    public int Height { get; }
}
