namespace Jacobi.Vst.Framework
{
    using System;

    [Flags]
    public enum VstPluginCapabilities
    {
        None, 
        IsSynth,
        NoSoundInStop,
    }
}
