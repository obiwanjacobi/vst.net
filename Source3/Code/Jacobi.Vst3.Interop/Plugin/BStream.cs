using System;
using System.IO;
using Jacobi.Vst3.Interop;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Plugin
{
    public sealed class BStream : Stream
    {
        private int _unmanagedBufferSize;
        private IntPtr _unmanagedBuffer;

        public BStream(IBStream streamToWrap)
        {
            BaseStream = streamToWrap;
        }

        public BStream(IBStream streamToWrap, int unmanagedBufferSize)
        {
            BaseStream = streamToWrap;

            if (unmanagedBufferSize > 0)
            {
                _unmanagedBufferSize = unmanagedBufferSize;
                _unmanagedBuffer = Marshal.AllocHGlobal(unmanagedBufferSize);
                GC.AddMemoryPressure(unmanagedBufferSize);
            }
        }

        protected IBStream BaseStream { get; private set; }


        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            // no-op
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get
            {
                long pos = 0;

                if (TResult.Succeeded(BaseStream.Tell(ref pos)))
                {
                    return pos;
                }

                return -1;
            }
            set
            {
                Seek(value, SeekOrigin.Begin);
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long pos = 0;

            if (TResult.Succeeded(BaseStream.Seek(offset, SeekOriginSeekMode(origin), ref pos)))
            {
                return pos;
            }

            return -1;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            IntPtr unmanaged = GetUnmanagedBuffer(ref count);

            try
            {
                int readBytes = 0;

                if (TResult.Succeeded(BaseStream.Read(unmanaged, count, ref readBytes)))
                {
                    for (int i = 0; i < readBytes; i++)
			        {
                        buffer[offset + i] = Marshal.ReadByte(unmanaged, i);
			        }

                    return readBytes;
                }
            }
            finally 
            {
                ReleaseUnmanagedBuffer(unmanaged);
            }

            return 0;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            IntPtr unmanaged = GetUnmanagedBuffer(ref count);

            try
            {
                for (int i = 0; i < count; i++)
                {
                    Marshal.WriteByte(unmanaged, i, buffer[offset + i]);
                }

                int writtenBytes = 0;
                BaseStream.Write(unmanaged, count, ref writtenBytes);
            }
            finally
            {
                ReleaseUnmanagedBuffer(unmanaged);
            }
        }

        private IntPtr GetUnmanagedBuffer(ref int size)
        {
            if (_unmanagedBuffer != IntPtr.Zero)
            {
                if (size > _unmanagedBufferSize)
                {
                    size = _unmanagedBufferSize;
                }

                return _unmanagedBuffer;
            }
            
            return Marshal.AllocHGlobal(size);
        }

        private void ReleaseUnmanagedBuffer(IntPtr mem)
        {
            if (_unmanagedBuffer == IntPtr.Zero)
            {
                Marshal.FreeHGlobal(mem);
            }
        }

        private StreamSeekMode SeekOriginSeekMode(SeekOrigin seekOrigin)
        {
            switch (seekOrigin)
            {
                case SeekOrigin.Begin:
                    return StreamSeekMode.SeekSet;
                case SeekOrigin.Current:
                    return StreamSeekMode.SeekCur;
                case SeekOrigin.End:
                    return StreamSeekMode.SeekEnd;
            }

            return StreamSeekMode.SeekSet;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (_unmanagedBuffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(_unmanagedBuffer);
                    GC.RemoveMemoryPressure(_unmanagedBufferSize);
                    _unmanagedBuffer = IntPtr.Zero;
                    _unmanagedBufferSize = 0;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
    }
}
