namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    public interface IVstPluginConnections
    {
        VstSpeakerArrangement InputSpeakerArrangement {get; set;}
        VstSpeakerArrangement OutputSpeakerArrangement { get; set; }
    }
}
