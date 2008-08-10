namespace Jacobi.Vst.Framework
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Manages a collection of <see cref="VstProgram"/> instances.
    /// </summary>
    public class VstProgramCollection : Collection<VstProgram>
    {
        /// <summary>
        /// Adds a range of <see cref="VstProgram"/> instances to the collection.
        /// </summary>
        /// <param name="programs">Must not be null.</param>
        public void AddRange(VstProgramCollection programs)
        {
            Throw.IfArgumentIsNull(programs, "programs");

            foreach (VstProgram program in programs)
            {
                Add(program);
            }
        }

        /// <summary>
        /// Overridden to <see cref="VstProgram.Dispose"/> the instances.
        /// </summary>
        protected override void ClearItems()
        {
            foreach (VstProgram program in this)
            {
                program.Dispose();
            }

            base.ClearItems();
        }

        /// <summary>
        /// Overridden to <see cref="VstProgram.Dispose"/> the removed instance.
        /// </summary>
        protected override void RemoveItem(int index)
        {
            VstProgram program = this[index];

            base.RemoveItem(index);

            program.Dispose();
        }

        /// <summary>
        /// Overridden to <see cref="VstProgram.Dispose"/> the replaced instance.
        /// </summary>
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
