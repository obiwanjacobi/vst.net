﻿namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;

    /// <summary>
    /// Contains information about a Midi Program.
    /// </summary>
    public class VstMidiProgram : ObservableObject
    {
        private static readonly string[] KeyNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        private string _name;
        /// <summary>
        /// Gets or sets the name of the Midi program.
        /// </summary>
        /// <remarks>The Name cannot exceed 63 characters.</remarks>
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxMidiNameLength, "Name");

                SetProperty(value, ref _name, "Name");
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
                SetProperty(value, ref _programChange, "ProgramChange");
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
                SetProperty(value, ref _bankSelectMsb, "BankSelectMsb");
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
                SetProperty(value, ref _bankSelectLsb, "BankSelectLsb");
            }
        }

        public VstMidiCategory _category;
        /// <summary>
        /// Gets or sets the <see cref="VstMidiCategory"/> instance this Midi program is part of.
        /// </summary>
        /// <remarks>Can be null.</remarks>
        public VstMidiCategory Category
        {
            get { return _category; }
            set
            {
                SetProperty(value, ref _category, "Category");
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
    }
}
