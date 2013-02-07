using System;

namespace LeutgebAes
{
	public abstract class BlockCipherModeUsingInitializationVector : BlockCipherMode
	{
		private byte[] iv;
		
		protected byte[] InitializationVector
		{
			get { return (byte[])iv.Clone(); }
		}
		
		protected BlockCipherModeUsingInitializationVector(IBlockCipher cipher, byte[] iv) : base(cipher)
		{
			if (iv == null)
				throw new ArgumentNullException("iv", "IV cannot be null!");
			
			this.iv = iv;
		}
	}
}

