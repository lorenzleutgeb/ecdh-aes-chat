using System;
using System.IO;

namespace LeutgebAes
{
	public interface IBlockCipherMode
	{
		void Encrypt(Stream inStream, Stream outStream);
		void Decrypt(Stream inStream, Stream outStream);
	}
}

