using System;
using System.IO;
using Jacobi.Vst3.Interop;

namespace Jacobi.Vst3.Host
{
    public class BStream : IBStream
    {
        public BStream(Stream streamToWrap)
        {
            BaseStream = streamToWrap;
        }

        protected Stream BaseStream { get; private set; }

        #region IBStream Members

        public int Read(byte[] buffer, int numBytes, ref int numBytesRead)
        {
            numBytesRead = BaseStream.Read(buffer, 0, numBytes);
            
            return TResult.S_OK;
        }

        public int Write(byte[] buffer, int numBytes, ref int numBytesWritten)
        {
            BaseStream.Write(buffer, 0, numBytes);
            numBytesWritten = numBytes;

            return TResult.S_OK;
        }

        public int Seek(long pos, IStreamSeekMode mode, ref long result)
        {
            throw new NotImplementedException();
        }

        public int Tell(ref long pos)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
