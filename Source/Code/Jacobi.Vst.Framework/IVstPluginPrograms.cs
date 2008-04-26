namespace Jacobi.Vst.Framework
{
    public interface IVstPluginPrograms
    {
        VstProgramCollection Programs { get; }
        VstProgram ActiveProgram { get; set; }

        void BeginSetProgram();
        void EndSetProgram();
    }
}
