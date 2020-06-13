namespace Jacobi.Vst.Plugin.Framework
{
    using Jacobi.Vst.Core;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Manages a collection of <see cref="VstProgram"/> instances.
    /// </summary>
    public class VstProgramCollection : ObservableCollection<VstProgram>
    {
        /// <summary>
        /// Contructs an empty instance.
        /// </summary>
        public VstProgramCollection()
        { }

        /// <summary>
        /// Constructs a prefilled instance.
        /// </summary>
        /// <param name="programs">Must not be null.</param>
        public VstProgramCollection(IEnumerable<VstProgram> programs)
            : base(programs)
        { }

        /// <summary>
        /// Adds a range of <see cref="VstProgram"/> instances to the collection.
        /// </summary>
        /// <param name="programs">Must not be null.</param>
        public void AddRange(VstProgramCollection programs)
        {
            Throw.IfArgumentIsNull(programs, nameof(programs));

            foreach (VstProgram program in programs)
            {
                Add(program);
            }
        }

        /// <summary>
        /// Overridden to <see cref="System.IDisposable.Dispose"/> the instances.
        /// </summary>
        protected override void ClearItems()
        {
            List<VstProgram> programs = new List<VstProgram>(this);

            base.ClearItems();

            foreach (VstProgram program in programs)
            {
                program.Dispose();
            }
        }

        /// <summary>
        /// Overridden to <see cref="System.IDisposable.Dispose"/> the removed instance.
        /// </summary>
        /// <param name="index">A zero-based index that will be removed.</param>
        protected override void RemoveItem(int index)
        {
            VstProgram program = this[index];

            base.RemoveItem(index);

            program.Dispose();
        }

        /// <summary>
        /// Overridden to <see cref="System.IDisposable.Dispose"/> the replaced instance.
        /// </summary>
        /// <param name="index">A zero-based index the new <paramref name="item"/> will be set.</param>
        /// <param name="item">The new item to set. Can be null.</param>
        protected override void SetItem(int index, VstProgram item)
        {
            VstProgram program = this[index];

            base.SetItem(index, item);

            if (program != item)
            {
                program.Dispose();
            }
        }
    }
}
