using System;
using System.IO;

namespace LeutgebAes
{
    class ElectronicCodeBook : BlockCipherMode
    {
        public ElectronicCodeBook(IBlockCipher cipher) : base(cipher)
        {
        }

        public override void Encrypt(Stream inStream, Stream outStream)
        {
            byte[] block = new byte[16];

            while (inStream.Position < inStream.Length)
            {
                inStream.Read(block, 0, 16);
                block = Cipher.Encrypt(block);
                outStream.Write(block, 0, 16);
            }
        }

        public override void Decrypt(Stream inStream, Stream outStream)
        {
            byte[] block = new byte[16];

            while (inStream.Position < inStream.Length)
            {
                inStream.Read(block, 0, 16);
                block = Cipher.Decrypt(block);
                outStream.Write(block, 0, 16);
            }
        }
    }
}
