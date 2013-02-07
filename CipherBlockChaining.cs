using System;
using System.IO;
using System.Threading;

namespace LeutgebAes
{
    public class CipherBlockChaining : IBlockCipherMode
    {
        private byte[] iv;
        private IBlockCipher cipher;

        public CipherBlockChaining(IBlockCipher cipher, byte[] iv)
        {
            this.iv = iv;
            this.cipher = cipher;
        }

        public void Encrypt(Stream inStream, Stream outStream)
        {
            byte[] block = new byte[16];

            while (inStream.Position < inStream.Length)
            {
                inStream.Read(block, 0, 16);

                for (int j = 0; j < block.Length; j++)
                    iv[j] ^= block[j];

                iv = cipher.Encrypt(iv);

                outStream.Write(iv, 0, 16);
            }
        }

        /// <summary>
        /// Decrypts a Stream
        /// </summary>
        /// <param name="inStream">Stream to read ciphered bytes</param>
        /// <param name="outStream">Stream to write deciphered bytes to</param>
        public void Decrypt(Stream inStream, Stream outStream)
        {
            byte[] block = new byte[16];

            while (inStream.Position < inStream.Length)
            {
                inStream.Read(block, 0, 16);

                block = cipher.Encrypt(block);

                for (int j = 0; j < block.Length; j++)
                    iv[j] ^= block[j];

                outStream.Write(iv, 0, 16);

                Array.Copy(block, iv, 16);
            }
        }
    }
}

