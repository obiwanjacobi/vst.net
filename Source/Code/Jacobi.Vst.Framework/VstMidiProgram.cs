namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;

    /// <summary>
    /// Contains information about a Midi Program.
    /// </summary>
    public class VstMidiProgram : ObservableObject, IActivatable
    {
        private static readonly string[] KeyNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        /// <summary>Name</summary>
        public const string NamePropertyName = "Name";
        /// <summary>ProgramChange</summary>
        public const string ProgramChangePropertyName = "ProgramChange";
        /// <summary>BankSelectMsb</summary>
        public const string BankSelectMsbPropertyName = "BankSelectMsb";
        /// <summary>BankSelectLsb</summary>
        public const string BankSelectLsbPropertyName = "BankSelectLsb";
        /// <summary>Category</summary>
        public const string CategoryPropertyName = "Category";
        /// <summary>IsActive</summary>
        public const string IsActivePropertyName = "IsActive";

        private string _name;
        /// <summary>
        /// Gets or sets the name of the Midi program.
        /// </summary>
        /// <remarks>The Name cannot exceed 64 characters.</remarks>
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxMidiNameLength, NamePropertyName);

                SetProperty(value, ref _name, NamePropertyName);
            }
        }

        private byte _programChange;
        /// <summary>
        /// Gets or sets the Midi Program Change message value.
        /// </summary>
        public byte ProgramChange
        {
            get { return _programChange; }
            set
            {
                SetProperty(value, ref _programChange, ProgramChangePropertyName);
            }
        }

        private byte _bankSelectMsb;
        /// <summary>
        /// Gets or sets the Most Significant Byte (Hi) value of the Midi Bank Select message.
        /// </summary>
        public byte BankSelectMsb
        {
            get { return _bankSelectMsb; }
            set
            {
                SetProperty(value, ref _bankSelectMsb, BankSelectMsbPropertyName);
            }
        }
        
        private byte _bankSelectLsb;
        /// <summary>
        /// Gets or sets the Least Significant Byte (Lo) value of the Midi Bank Select message.
        /// </summary>
        public byte BankSelectLsb
        {
            get { return _bankSelectLsb; }
            set
            {
                SetProperty(value, ref _bankSelectLsb, BankSelectLsbPropertyName);
            }
        }

        private VstMidiCategory _category;
        /// <summary>
        /// Gets or sets the <see cref="VstMidiCategory"/> instance this Midi program is part of.
        /// </summary>
        /// <remarks>Can be null.</remarks>
        public VstMidiCategory Category
        {
            get { return _category; }
            set
            {
                SetProperty(value, ref _category, CategoryPropertyName);
            }
        }

        /// <summary>
        /// Retrieves the name of the specified <paramref name="keyNumber"/>.
        /// </summary>
        /// <param name="keyNumber">The Midi key number (note number).</param>
        /// <returns>Returns a string containing the note (C,C#,D -- A,A#,B) and an ocatve number starting at -2.</returns>
        public virtual string GetKeyName(int keyNumber)
        {
            int note = keyNumber % 12;
            int octave = keyNumber / 12;

            return KeyNames[note] + (octave - 2);
        }

        #region IActivatable Members

        private bool _isActive;

        /// <summary>
        /// Gets or sets an indication if this instance is currently the active MidiProgram.
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                SetProperty(value, ref _isActive, IsActivePropertyName);
            }
        }

        #endregion
    }
}
