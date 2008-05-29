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

        protected override void ClearItems()
        {
            foreach (VstProgram program in this)
            {
                program.Dispose();
            }

            base.ClearItems();
        }

        protected override void RemoveItem(int index)
        {
            VstProgram program = this[index];

            base.RemoveItem(index);

            program.Dispose();
        }

        protected override void SetItem(int index, VstProgram item)
        {
            VstProgram program = this[index];

            if (program != item)
            {
                program.Dispose();
            }

            base.SetItem(index, item);
        }
    }
}
