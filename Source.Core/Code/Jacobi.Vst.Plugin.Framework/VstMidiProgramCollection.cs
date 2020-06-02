namespace Jacobi.Vst.Plugin.Framework
{
    using Jacobi.Vst.Plugin.Framework.Common;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Manages a collection of <see cref="VstMidiProgram"/> instances.
    /// </summary>
    public class VstMidiProgramCollection : ObservableKeyedCollection<string, VstMidiProgram>
    {
        /// <summary>
        /// Returns a unique key for the specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item in the collection a key is needed for.</param>
        /// <returns>Returns the <see cref="VstMidiProgram.Name"/> property.</returns>
        protected override string GetKeyForItem(VstMidiProgram item)
        {
            if (item == null) return null;

            return item.Name;
        }

        /// <summary>
        /// Called to clear all the items from the collection.
        /// </summary>
        /// <remarks>The implementation removes all event handlers from the instances.</remarks>
        protected override void ClearItems()
        {
            foreach (VstMidiProgram old in this)
            {
                old.PropertyChanged -= new PropertyChangedEventHandler(VstMidiProgram_PropertyChanged);
            }

            base.ClearItems();
        }

        /// <summary>
        /// Called to insert a new instance into the collection.
        /// </summary>
        /// <param name="index">Zero-based position into the collection.</param>
        /// <param name="item">The item to insert.</param>
        /// <remarks>The implementation adds an event handler to the <see cref="ObservableObject.PropertyChanged"/> event.</remarks>
        protected override void InsertItem(int index, VstMidiProgram item)
        {
            item.PropertyChanged += new PropertyChangedEventHandler(VstMidiProgram_PropertyChanged);

            base.InsertItem(index, item);
        }

        /// <summary>
        /// Called to set a new item on an exisint position in the collection.
        /// </summary>
        /// <param name="index">Zero-based position into the collection.</param>
        /// <param name="item">The item to set.</param>
        /// <remarks>The implementation adds an event handler to the <see cref="ObservableObject.PropertyChanged"/> 
        /// event and remove the event handler from the old item that is replaced.</remarks>
        protected override void SetItem(int index, VstMidiProgram item)
        {
            VstMidiProgram old = this[index];
            old.PropertyChanged -= new PropertyChangedEventHandler(VstMidiProgram_PropertyChanged);

            item.PropertyChanged += new PropertyChangedEventHandler(VstMidiProgram_PropertyChanged);

            base.SetItem(index, item);
        }

        /// <summary>
        /// Called when to remove an item from the collection.
        /// </summary>
        /// <param name="index">Zero-based position into the collection.</param>
        /// <remarks>The implementation removes the event handler from the item that is removed.</remarks>
        protected override void RemoveItem(int index)
        {
            VstMidiProgram old = this[index];
            old.PropertyChanged -= new PropertyChangedEventHandler(VstMidiProgram_PropertyChanged);

            base.RemoveItem(index);
        }

        /// <summary>
        /// Event is raised when a <see cref="ObservableObject.PropertyChanged"/> event is raised.
        /// </summary>
        public event EventHandler<EventArgs> MidiProgramNameChanged;

        /// <summary>
        /// Raises the <see cref="MidiProgramNameChanged"/> event when a
        /// <see cref="ObservableObject.PropertyChanged"/> event is fired.
        /// </summary>
        /// <param name="sender">The original <see cref="VstMidiProgram"/> that fired the event.</param>
        protected virtual void OnMidiProgramNameChanged(object sender)
        {
            EventHandler<EventArgs> handler = MidiProgramNameChanged;

            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }

        //// event handler that receives Changed events from the VstMidiProgram instances.
        private void VstMidiProgram_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(VstMidiProgram.Name))
            {
                OnMidiProgramNameChanged(sender);
            }
        }
    }
}
