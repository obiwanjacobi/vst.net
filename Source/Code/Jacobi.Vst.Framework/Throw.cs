namespace Jacobi.Vst.Framework
{
    using System;

    internal static class Throw
    {
        public static void IfArgumentIsNull<T>(T argument, string argumentName) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void IfArgumentTooLong(string argument, int maxLength, string argumentName)
        {
            if (argument != null && argument.Length > maxLength)
            {
                throw new ArgumentException(
                    String.Format("'{0}' is too long. Maximum length is {1} characters.", argument, maxLength), argumentName);
            }
        }

        public static void IfArgumentNotInRange<T>(IComparable<T> argument, T minValue, T maxValue, string argumentName)
        {
            if (argument.CompareTo(minValue) < 0 || argument.CompareTo(maxValue) > 0)
            {
                throw new ArgumentOutOfRangeException(argumentName, argument,
                    String.Format("The value should lie between '{0}' and '{1}'.", minValue, maxValue));
            }
        }
    }
}
