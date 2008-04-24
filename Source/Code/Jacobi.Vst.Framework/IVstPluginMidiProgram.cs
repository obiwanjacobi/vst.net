namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    public interface IVstPluginMidiProgram
    {
        bool Fill(VstMidiProgramName midiProgram);
        bool FillCurrent(VstMidiProgramName midiProgram);
        bool FillCategory(VstMidiProgramCategory midiProgCat);
    }
}
