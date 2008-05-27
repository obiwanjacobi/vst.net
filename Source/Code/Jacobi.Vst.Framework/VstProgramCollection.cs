namespace Jacobi.Vst.Framework
{
    using System.Collections.ObjectModel;

    public class VstProgramCollection : Collection<VstProgram>
    {
        public void AddRange(VstProgramCollection programs)
        {
            Throw.IfArgumentIsNull(programs, "programs");

            foreach (VstProgram program in programs)
            {
                Add(program);
            }
        }
    }
}
