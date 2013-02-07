using System;
using System.IO;

namespace LeutgebAes
{
	public class PKCSPaddedStream : Stream
	{
		private Stream stream;
		private byte padding;
		private byte[] temp;
        private bool written = false;
        private int blockSize = 16;

        public PKCSPaddedStream(int blockSize, Stream stream)
        {
			this.stream = stream;
            this.temp = new byte[blockSize];

            try
            {
                padding = Convert.ToByte(blockSize - stream.Length % blockSize);
            }
            catch (NotSupportedException)
            {
                padding = 0xff;
            }
        }

		public PKCSPaddedStream(IBlockCipher cipher, Stream stream) : this(cipher.GetBlockSizeInBytes(), stream)
		{
		}
		
		public override bool CanRead {
			get { return /*padding == 0xff ? false :*/ stream.CanRead; }
		}
		
		public override bool CanSeek {
			get { return stream.CanSeek; }
		}
		
		public override bool CanWrite {
			get { return stream.CanWrite; }
		}
		
		public override long Length {
			get {
				return padding == 0xff ? 0 : stream.Length + padding;
			}
		}
		
		public override long Position {
			get {
				try {
                    return /*padding == 0xff ? long.MaxValue :*/ stream.Position;
				}
				catch (ObjectDisposedException) {
					return long.MaxValue;
				}
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public override int Read(byte[] buffer, int offset, int count)
		{
            if (stream.CanSeek)
            {
                if (padding == 0xff)
                    return -1;

                int read = stream.Read(buffer, offset, count);

                if (read < 0 || stream.Position < stream.Length)
                    return read;

                for (int i = read; i < count; i++)
                    buffer[offset + i] = padding;

                padding = 0xff;

                return count;
            }
            if (count == 16)
                return stream.Read(buffer, offset, count);
            else
                throw new NotSupportedException();
		}
		
		public override void Write(byte[] buffer, int offset, int count)
		{
            if (stream.CanSeek)
            {
                if (count != blockSize)
                    throw new NotImplementedException("This type of stream can only buffer full blocks!");

                if (written) stream.Write(this.temp, 0, this.temp.Length);

                written = true;

                Array.Copy(buffer, offset, this.temp, 0, count);
            }
            else
            {
                /*for (int i = 0; i < count - count % 16; i += 16)
                {
                    stream.Write(buffer, offset + i, 16);
                }

                padding = (byte)(16 - count % 16);
                byte[] padded = new byte[16];
                Array.Copy(buffer, offset, padded, 0, padding);
                for (int i = padding; i < 16; i++)
                    padded[i] = padding;

                stream.Write(padded, 0, 16); */
                stream.Write(buffer, offset, count);
            }
		}

        public override void Close()
        {
			if (written) {
				padding = temp[temp.Length - 1];

                if (padding > blockSize)
                    throw new Exception("Padding is out of range!"); 

				for (byte i = 0; i < padding; i++)
	                if (temp[temp.Length - i - 1] != padding)
						throw new Exception("Padding is incorrect!");
				
				stream.Write(temp, 0, temp.Length - padding);
			}
            stream.Close();
            base.Close();
        }
		
		public override void Flush()
		{
			stream.Flush();
		}
		
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}
		
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}
	}
}

