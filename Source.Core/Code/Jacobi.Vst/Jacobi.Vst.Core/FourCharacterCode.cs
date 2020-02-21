namespace Jacobi.Vst.Core
{
    using System;
    using System.Text;

    /// <summary>
    /// This structure encapsulates a Four Character Code as a string.
    /// </summary>
    /// <remarks>
    /// Note that this type is defined as a structure. It is therefor a value-type.
    /// </remarks>
    public struct FourCharacterCode
    {
        /// <summary>
        /// Constructs a new instance based on four seperate characters.
        /// </summary>
        /// <param name="c1">Most significant character.</param>
        /// <param name="c2">Character 2.</param>
        /// <param name="c3">Character 3.</param>
        /// <param name="c4">Least significant character</param>
        public FourCharacterCode(char c1, char c2, char c3, char c4)
        {
            _value = new string(new char[] { c1, c2, c3, c4 });
        }

        /// <summary>
        /// Constructs an instance based on a string <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A string of exactly four characters.</param>
        public FourCharacterCode(string value)
        {
            ThrowIfInvalidString(value, "value");

            _value = value;
        }

        private string _value;
        /// <summary>
        /// Gets or sets the Four Character Code value as a string.
        /// </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                ThrowIfInvalidString(value, "value");

                _value = value;
            }
        }

        /// <summary>
        /// Retrieves the FCC as a string.
        /// </summary>
        /// <returns>Returns the <see cref="P:Value"/> property.</returns>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Retrieves the FCC as an integer.
        /// </summary>
        /// <returns>Returns an 32-bit integer value representing the the FCC.</returns>
        /// <remarks>ASCII encoding on the <see cref="Value"/> property is used to convert the FCC.</remarks>
        public int ToInt32()
        {
            byte[] buffer = Encoding.ASCII.GetBytes(Value);

            // custom code to always force big endian
            return (((int)buffer[0] << 24) | ((int)buffer[1] << 16) | ((int)buffer[2] << 8) | ((int)buffer[3]));
        }

        private static void ThrowIfInvalidString(string value, string parameterName)
        {
            if (value == null || value.Length != 4)
            {
                throw new ArgumentException(Properties.Resources.FourCharacterCode_InvalidValue, parameterName);
            }
        }
    }
}
