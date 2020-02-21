namespace Jacobi.Vst.Framework
{
    using System;

    /// <summary>
    /// Flags that indicate Host capabilities.
    /// </summary>
    [Flags]
    public enum VstHostCapabilities
    {
        /// <summary>Null value.</summary>
        None = 0x0000,
        /// <summary>The host is capable of sending Midi events to the plugin.</summary>
        SendMidiEvents = 0x0001,
        /// <summary>The host is capable of receiving Midi events from the plugin.</summary>
        ReceiveMidiEvents = 0x0002,
        /// <summary>The host supports routing real-time Midi events to the plugin.</summary>
        RealtimeMidiFlag = 0x0004,
        /// <summary>The host reports connection changes (bus assignments) to the plugin.</summary>
        ReportConnectionChanges = 0x0008,
        /// <summary>The host supports <see cref="IVstHostSequencer.UpdatePluginIO"/>.</summary>
        AcceptIoChanges = 0x0010,
        /// <summary>The host supports sizing its window.</summary>
        SizeWindow = 0x0020,
        /// <summary>The host supports offline processing.</summary>
        Offline = 0x0040,
        /// <summary>The host supports the <see cref="IVstHostShell.OpenFileSelector"/> method.</summary>
        OpenFileSelector = 0x0080,
        /// <summary>The host supports calling the <see cref="IVstPluginProcess"/> interface.</summary>
        StartStopProcess = 0x0100,
        /// <summary>The host is capable of working with plugins that host other plugins.</summary>
        PluginHost = 0x0200,
        /// <summary>TODO:</summary>
        SendTimeInfo = 0x0400,
    }
}
