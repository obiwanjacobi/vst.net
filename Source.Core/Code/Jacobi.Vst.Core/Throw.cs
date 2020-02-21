namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// A helper class for method parameter checking.
    /// </summary>
    public static class Throw
    {
        /// <summary>
        /// Tests if the <paramref name="argument"/> is null.
        /// </summary>
        /// <typeparam name="T">Inferred, no need to specify explicitly (in most cases).</typeparam>
        /// <param name="argument">The value of the argument to test.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is not set to an instance of an object.</exception>
        public static void IfArgumentIsNull<T>(T argument, string argumentName) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Tests if the number of characters in <paramref name="argument"/> exceed the <paramref name="maxLength"/>.
        /// </summary>
        /// <param name="argument">The string argument to test.</param>
        /// <param name="maxLength">The maximum number of characters allowed for the <paramref name="argument"/>.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <exception cref="ArgumentException">Thrown when the number of characters of the <paramref name="argument"/>
        /// exceed the specified <paramref name="maxLength"/>.</exception>
        public static void IfArgumentTooLong(string argument, int maxLength, string argumentName)
        {
            if (argument != null && argument.Length > maxLength)
            {
                throw new ArgumentException(
                    String.Format(Properties.Resources.Throw_ArgumentTooLong, argument, maxLength), argumentName);
            }
        }

        /// <summary>
        /// Tests if the string <paramref name="argument"/> is an empty string or null.
        /// </summary>
        /// <param name="argument">The string argument to test.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"/> is not set to an instance of an object.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"/> is an empty string.</exception>
        public static void IfArgumentIsNullOrEmpty(string argument, string argumentName)
        {
            if (String.IsNullOrEmpty(argument))
            {
                IfArgumentIsNull(argument, argumentName);

                throw new ArgumentException(Properties.Resources.Throw_ArgumentIsEmpty, argumentName);
            }
        }

        /// <summary>
        /// Tests if the <paramref name="argument"/> lies in a range between <paramref name="minValue"/> and <paramref name="maxValue"/>.
        /// </summary>
        /// <typeparam name="T">Inferred, no need to specify explicitly (in most cases).</typeparam>
        /// <param name="argument">The value to test the range for.</param>
        /// <param name="minValue">The minimal value the <paramref name="argument"/> can have (inclusive).</param>
        /// <param name="maxValue">The maximum value the <paramref name="argument"/> can have (inclusive).</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value of <paramref name="argument"/>
        /// does not lie in the range between <paramref name="minValue"/> and <paramref name="maxValue"/>.</exception>
        public static void IfArgumentNotInRange<T>(IComparable<T> argument, T minValue, T maxValue, string argumentName)
        {
            if (argument.CompareTo(minValue) < 0 || argument.CompareTo(maxValue) > 0)
            {
                throw new ArgumentOutOfRangeException(argumentName, argument,
                    String.Format(Properties.Resources.Throw_ArgumentNotInRange, minValue, maxValue));
            }
        }
    }
}
