using System;

namespace LeutgebAes
{
	public interface IBlockCipher
	{
		byte[] Encrypt(byte[] message);
		byte[] Decrypt(byte[] cipher);
		int GetBlockSizeInBytes();
	}
}

