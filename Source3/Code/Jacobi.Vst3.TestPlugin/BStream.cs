using System;
using System.IO;
using Jacobi.Vst3.Interop;

namespace Jacobi.Vst3.TestPlugin
{
    public class BStream : Stream, IBStream
    {
        // TODO: cannot implement IBStream and wrap in one class!
        public BStream(IBStream streamToWrap)
        {
            InternalStream = streamToWrap;
        }

        protected IBStream InternalStream { get; private set; }

        public IBStream ToIBStream()
        {
            return (IBStream)this;
        }

        public override bool CanRead
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanSeek
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanWrite
        {
            get { throw new NotImplementedException(); }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        #region IBStream Members

        int IBStream.Read(byte[] buffer, int numBytes, ref int numBytesRead)
        {
            throw new NotImplementedException();
        }

        int IBStream.Write(byte[] buffer, int numBytes, ref int numBytesWritten)
        {
            throw new NotImplementedException();
        }

        int IBStream.Seek(long pos, IStreamSeekMode mode, ref long result)
        {
            throw new NotImplementedException();
        }

        int IBStream.Tell(ref long pos)
        {
            throw new NotImplementedException();
        }

        #endregion

        
    }
}
