using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace LeutgebAes
{
	public class SegmentedIntegerCounter : BlockCipherModeUsingInitializationVector
	{
		public SegmentedIntegerCounter (IBlockCipher cipher, byte[] iv) : base(cipher, iv)
		{
			if (cipher.GetBlockSizeInBytes() != 16)
				throw new NotImplementedException();
		}
		
		ManualResetEvent[] doneEvents;
		byte[] input, output;
		
		private void EncryptParallel(Stream inStream, Stream outStream)
		{
			input = new byte[inStream.Length];
			inStream.Read(input, 0, input.Length);
            int blocks = input.Length % Cipher.GetBlockSizeInBytes() == 0 ? input.Length / Cipher.GetBlockSizeInBytes() : input.Length / Cipher.GetBlockSizeInBytes() + 1, i = 0;
            output = new byte[blocks * Cipher.GetBlockSizeInBytes()];

            while (i < blocks)
            {
                doneEvents = new ManualResetEvent[Math.Min(64, blocks - i)];

                for (int j = 0; j < doneEvents.Length; j++)
                {
                    doneEvents[j] = new ManualResetEvent(false);
                    ThreadPool.QueueUserWorkItem(EncryptParallel, i++);
                }

                WaitHandle.WaitAll(doneEvents);

                for (int j = 0; j < doneEvents.Length; j++)
                    outStream.Write(output, (i - doneEvents.Length + j) * Cipher.GetBlockSizeInBytes(), Cipher.GetBlockSizeInBytes());
            }
		}
		
		private void EncryptParallel(object state)
		{
			int counter = (int)state;
			byte[] temp = BitConverter.GetBytes(counter), iv = this.InitializationVector;
			
			for (int j = 0; j < 4; j++)
				iv[j * (iv.Length / 4)] ^= temp[j];

            iv = Cipher.Encrypt(iv);

            for (int j = 0; j < iv.Length && counter * 16 + j < input.Length; j++)
				iv[j] ^= input[counter * 16 + j];
			
			Array.Copy(iv, 0, output, counter * 16, 16);
			
			doneEvents[counter % 64].Set();
		}
		
		private void EncryptSerial(Stream inStream, Stream outStream)
		{
			byte[] iv = null, block = new byte[16], temp = new byte[4];
			uint counter = 0;
			
			while (inStream.Position < inStream.Length) {
				inStream.Read(block, 0, 16); 
				iv = this.InitializationVector;
				
				temp = BitConverter.GetBytes(counter++);
				
				for (int j = 0; j < 4; j++)
					iv[j * (iv.Length / 4)] ^= temp[j];

                iv = Cipher.Encrypt(iv);

				for (int j = 0; j < block.Length; j++)
					iv[j] ^= block[j];

				outStream.Write(iv, 0, 16);
			}
		}
		
		public override void Encrypt(Stream inStream, Stream outStream)
		{
			if (Convert.ToInt64(new PerformanceCounter("Memory", "Available Bytes").NextValue()) < inStream.Length * 2)
				EncryptSerial(inStream, outStream);
			else
				EncryptParallel(inStream, outStream);
		}
		
		public override void Decrypt(Stream inStream, Stream outStream)
		{
			Encrypt(inStream, outStream);
		}
	}
}

