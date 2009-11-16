namespace Jacobi.Vst.Core
{
    using System;
    using System.Text;

    public struct FourCharacterCode
    {
        public FourCharacterCode(char c1, char c2, char c3, char c4)
        {
            _value = new string(new char[] { c1, c2, c3, c4 });
        }

        public FourCharacterCode(string value)
        {
            _value = value;
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                if (value == null || value.Length != 4)
                {
                    throw new ArgumentException(Properties.Resources.FourCharacterCode_InvalidValue, "Value");
                }

                _value = value;
            }
        }

        public override string ToString()
        {
            return Value;
        }

        public int ToInt32()
        {
            byte[] buffer = Encoding.ASCII.GetBytes(Value);
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}
