using System;

namespace Jacobi.Vst3.Core.Common
{

    [Serializable]
    public class VstException : Exception
    {
        public VstException(int result)
        {
            Result = result;
        }

        public VstException(int result, string message)
            : base(message)
        {
            Result = result;
        }

        public VstException(int result, string message, Exception inner)
            : base(message, inner)
        {
            Result = result;
        }

        public int Result { get; private set; }

        protected VstException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
