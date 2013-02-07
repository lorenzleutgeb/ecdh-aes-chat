using System;
using System.IO;

namespace LeutgebAes
{
	public class CipherFeedback : BlockCipherModeUsingInitializationVector
	{
		public CipherFeedback(IBlockCipher cipher, byte[] iv) : base(cipher, iv)
		{
		}
		
        public override void Encrypt(Stream inStream, Stream outStream)
        {
			byte[] iv = this.InitializationVector, block = new byte[Cipher.GetBlockSizeInBytes()];

            while (inStream.Position < inStream.Length)
            {
				inStream.Read(block, 0, block.Length);
                iv = Cipher.Encrypt(iv);
				
				for (int j = 0; j < block.Length; j++)
					iv[j] ^= block[j];

				outStream.Write(iv, 0, block.Length);
			}
        }
		
        public override void Decrypt(Stream inStream, Stream outStream)
        {
			byte[] iv = this.InitializationVector, block = new byte[Cipher.GetBlockSizeInBytes()];

            while (inStream.Position < inStream.Length)
            {
				inStream.Read(block, 0, block.Length);
                iv = Cipher.Encrypt(iv);

				for (int j = 0; j < block.Length; j++)
					iv[j] ^= block[j];
				
				outStream.Write(iv, 0, block.Length);				
				iv = block;
			}
        }
	}
}

