using System;
using System.IO;

namespace LeutgebAes
{
	public abstract class BlockCipherMode : IBlockCipherMode
	{
		private IBlockCipher cipher;

        protected IBlockCipher Cipher
        {
            get { return cipher; }
            set { cipher = value; }
        }
		
		protected BlockCipherMode(IBlockCipher cipher)
		{
			if (cipher == null)
				throw new ArgumentNullException("cipher", "Cipher cannot be null!");
			
			this.cipher = cipher;
		}
		
		public abstract void Encrypt(Stream inStream, Stream outStream);
		public abstract void Decrypt(Stream inStream, Stream outStream);
	}
}

