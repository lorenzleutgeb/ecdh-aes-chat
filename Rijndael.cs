using System;

namespace LeutgebAes
{
    /// <summary>
    /// Implementation of AES192.
    /// </summary>
    class Rijndael : IBlockCipher
    {
		/// <summary>
		/// The input/output length in 32bit words (4, 4, 4).
		/// </summary>
        private int NB = 4;

		/// <summary>
		/// The key length in 32bit words (4, 6, 8) depending on the block length.
		/// </summary>
	    private int NK = 4;

		/// <summary>
		/// The number of rounds (10, 12, 14) depending on the block length.
		/// </summary>
        private int NR = 10;
		
		/// <summary>
		/// The S-Box used for encryption.
		/// </summary>
	    private static readonly byte[] sbox = new byte[] { 0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76, 0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0, 0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15, 0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75, 0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84, 0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf, 0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8, 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2, 0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73, 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb, 0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79, 0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08, 0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a, 0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e, 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf, 0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 };
		
		/// <summary>
		/// The S-Box used for decryption (inverse of encryption S-Box).
		/// </summary>
        private static readonly byte[] invSbox = new byte[] { 0x52, 0x09, 0x6A, 0xD5, 0x30, 0x36, 0xA5, 0x38, 0xBF, 0x40, 0xA3, 0x9E, 0x81, 0xF3, 0xD7, 0xFB, 0x7C, 0xE3, 0x39, 0x82, 0x9B, 0x2F, 0xFF, 0x87, 0x34, 0x8E, 0x43, 0x44, 0xC4, 0xDE, 0xE9, 0xCB, 0x54, 0x7B, 0x94, 0x32, 0xA6, 0xC2, 0x23, 0x3D, 0xEE, 0x4C, 0x95, 0x0B, 0x42, 0xFA, 0xC3, 0x4E, 0x08, 0x2E, 0xA1, 0x66, 0x28, 0xD9, 0x24, 0xB2, 0x76, 0x5B, 0xA2, 0x49, 0x6D, 0x8B, 0xD1, 0x25, 0x72, 0xF8, 0xF6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xD4, 0xA4, 0x5C, 0xCC, 0x5D, 0x65, 0xB6, 0x92, 0x6C, 0x70, 0x48, 0x50, 0xFD, 0xED, 0xB9, 0xDA, 0x5E, 0x15, 0x46, 0x57, 0xA7, 0x8D, 0x9D, 0x84, 0x90, 0xD8, 0xAB, 0x00, 0x8C, 0xBC, 0xD3, 0x0A, 0xF7, 0xE4, 0x58, 0x05, 0xB8, 0xB3, 0x45, 0x06, 0xD0, 0x2C, 0x1E, 0x8F, 0xCA, 0x3F, 0x0F, 0x02, 0xC1, 0xAF, 0xBD, 0x03, 0x01, 0x13, 0x8A, 0x6B, 0x3A, 0x91, 0x11, 0x41, 0x4F, 0x67, 0xDC, 0xEA, 0x97, 0xF2, 0xCF, 0xCE, 0xF0, 0xB4, 0xE6, 0x73, 0x96, 0xAC, 0x74, 0x22, 0xE7, 0xAD, 0x35, 0x85, 0xE2, 0xF9, 0x37, 0xE8, 0x1C, 0x75, 0xDF, 0x6E, 0x47, 0xF1, 0x1A, 0x71, 0x1D, 0x29, 0xC5, 0x89, 0x6F, 0xB7, 0x62, 0x0E, 0xAA, 0x18, 0xBE, 0x1B, 0xFC, 0x56, 0x3E, 0x4B, 0xC6, 0xD2, 0x79, 0x20, 0x9A, 0xDB, 0xC0, 0xFE, 0x78, 0xCD, 0x5A, 0xF4, 0x1F, 0xDD, 0xA8, 0x33, 0x88, 0x07, 0xC7, 0x31, 0xB1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xEC, 0x5F, 0x60, 0x51, 0x7F, 0xA9, 0x19, 0xB5, 0x4A, 0x0D, 0x2D, 0xE5, 0x7A, 0x9F, 0x93, 0xC9, 0x9C, 0xEF, 0xA0, 0xE0, 0x3B, 0x4D, 0xAE, 0x2A, 0xF5, 0xB0, 0xC8, 0xEB, 0xBB, 0x3C, 0x83, 0x53, 0x99, 0x61, 0x17, 0x2B, 0x04, 0x7E, 0xBA, 0x77, 0xD6, 0x26, 0xE1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0C, 0x7D };	
		
		/// <summary>
		/// The round constants (powers of two).
		/// </summary>
	    private static readonly byte[] roundConstants = new byte[] { 0x00, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a };
		
		/// <summary>
		/// The round keys which then can be reused for en-/decrypting multiple blocks to save ExpandKey() calls.
		/// </summary>
		private byte[][][] roundKeys = null;

        /// <summary>
        /// Initializes a new instance of this AES implementation using the specified key and precomputes all round keys.
        /// </summary>
        /// <param name="key">Main key used to derive round keys for the actual en-/decryption.</param>
		public Rijndael(byte[] key)
		{
			if (key.Length % 4 != 0)
				throw new ArgumentException("Invalid block size specified (must be a multiple of 4 bytes)!");

            switch (NK = key.Length / 4)
            {
				case 4: NR = 10; break;
				//case 6: NR = 12; break;
				//case 8: NR = 14; break;
				//default: throw new ArgumentException("Invalid block size specified! (must be one of 16, 24, 32 bytes)");
                default: throw new ArgumentException("Invalid block size specified! (must be 16 bytes)");
			}

            ExpandKey(key);
		}
		
        /// <summary>
        /// Gets the chosen size of processed blocks in bytes.
        /// </summary>
        /// <returns>The block size that can be processed by this instance in bytes.</returns>
		public int GetBlockSizeInBytes()
		{
			return NK * 4;
		}
		
		/// <summary>
		/// Shifts the rows according to the Rijndael specification
		/// </summary>
		/// <returns>
		/// Modified state after shifting rows.
		/// </returns>
		/// <param name='state'>
		/// State to modify.
		/// </param>
	    private static void ShiftRows(byte[][] state) {
            byte tmp = state[0][1];
            state[0][1] = state[1][1];
            state[1][1] = state[2][1];
            state[2][1] = state[3][1];
            state[3][1] = tmp;

            tmp = state[0][2];
            state[0][2] = state[2][2];
            state[2][2] = tmp;
            tmp = state[3][2];
            state[3][2] = state[1][2];
            state[1][2] = tmp;

            tmp = state[0][3];
            state[0][3] = state[3][3];
            state[3][3] = state[2][3];
            state[2][3] = state[1][3];
            state[1][3] = tmp;
	    }
		
		/// <summary>
		/// Reverses the process of row shifting (by shifting in the opposite direction).
		/// </summary>
		/// <returns>
		/// The modified state after being shifted.
		/// </returns>
		/// <param name='state'>
		/// The state to modify.
		/// </param>
	    private static void InvShiftRows(byte[][] state) {
            byte tmp = state[3][1];
            state[3][1] = state[2][1];
            state[2][1] = state[1][1];
            state[1][1] = state[0][1];
            state[0][1] = tmp;

            tmp = state[0][2];
            state[0][2] = state[2][2];
            state[2][2] = tmp;
            tmp = state[3][2];
            state[3][2] = state[1][2];
            state[1][2] = tmp;

            tmp = state[0][3];
            state[0][3] = state[1][3];
            state[1][3] = state[2][3];
            state[2][3] = state[3][3];
            state[3][3] = tmp;
	    }
		
		/// <summary>
		/// Mixes the columns according to Rijndael specs.
		/// </summary>
		/// <returns>
		/// The state after applying the MixColumns operation.
		/// </returns>
		/// <param name='state'>
		/// State to modify.
		/// </param>
	    private static void MixColumns(byte[][] state)
        {
		    for (int j = 0; j < 4; j++)
				state[j] = new byte[4] {
                	(byte)((XTime(state[j][1]) ^ state[j][1]) ^ XTime(state[j][0]) ^ state[j][2] ^ state[j][3]),
                	(byte)((XTime(state[j][2]) ^ state[j][2]) ^ XTime(state[j][1]) ^ state[j][0] ^ state[j][3]),
                	(byte)((XTime(state[j][3]) ^ state[j][3]) ^ XTime(state[j][2]) ^ state[j][0] ^ state[j][1]),
                	(byte)((XTime(state[j][0]) ^ state[j][0]) ^ XTime(state[j][3]) ^ state[j][1] ^ state[j][2])
				};
	    }
		
        /// <summary>
        /// Inverse of MixColumns.
        /// </summary>
        /// <param name="state">The state to modify.</param>
        /// <returns>Modified state.</returns>
		private static void InvMixColumns(byte[][] state)
		{
            for (int j = 0; j < 4; j++)
				state[j] = new byte[4] {
	                (byte)(mul(state[j][0], 0x0e) ^ mul(state[j][1], 0x0b) ^ mul(state[j][2], 0x0d) ^ mul(state[j][3], 0x09)),
					(byte)(mul(state[j][0], 0x09) ^ mul(state[j][1], 0x0e) ^ mul(state[j][2], 0x0b) ^ mul(state[j][3], 0x0d)),
					(byte)(mul(state[j][0], 0x0d) ^ mul(state[j][1], 0x09) ^ mul(state[j][2], 0x0e) ^ mul(state[j][3], 0x0b)),
					(byte)(mul(state[j][0], 0x0b) ^ mul(state[j][1], 0x0d) ^ mul(state[j][2], 0x09) ^ mul(state[j][3], 0x0e))
				};
		}
		
        /// <summary>
        /// Calculates <code>a * b</code> in the Galois Field 256.
        /// </summary>
        /// <param name="a">Operand of the multiplication.</param>
        /// <param name="b">Operand of the multiplication.</param>
        /// <returns>Result of the multiplcation in respect to a Galois Field of size 256.</returns>
		private static byte mul(byte a, byte b)
		{
			byte result = 0;

			for (int i = 0; i < 8; i++) {
                if ((b & 1) == 1)
                    result ^= a;

				a = XTime(a);
                b >>= 1;
			}
			
			return result;
		}
	
        /// <summary>
        /// Multiplication in 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
	    private static byte XTime(byte value) {
		    return (byte)((byte)(value << 1) ^ ((value & 128) != 0 ? 27 : 0));
	    }
	
        /// <summary>
        /// Substitutes all bytes of a state with the corresponding value in the S-Box.
        /// </summary>
        /// <param name="state">The state to substitute</param>
	    private static void SubBytes(byte[][] state) {
		    for (int i = 0; i < state.Length; i++)
                for (int j = 0; j < state[i].Length; j++)
                    state[i][j] = sbox[state[i][j]];
	    }
		
        /// <summary>
        /// Inverse byte substitution to undo byte substitiontion of specified state via S-Box.
        /// </summary>
        /// <param name="state">The state to substitute with the inverse S-Box</param>
	    private static void InvSubBytes(byte[][] state) {
		    for (int i = 0; i < state.Length; i++)
                for (int j = 0; j < state[i].Length; j++)
                    state[i][j] = invSbox[state[i][j]];
	    }
	
        /// <summary>
        /// Substitutes all bytes in the specified array.
        /// </summary>
        /// <param name="bytes">Array to substitute with S-Box values.</param>
	    private static void SubBytes(byte[] bytes) {
		    for (int i = 0; i < bytes.Length; i++)
			    bytes[i] = sbox[bytes[i]];
	    }
	
        /// <summary>
        /// Adds (XOR) the a specified roundKey to a state.
        /// </summary>
        /// <param name="bytes">state to alter</param>
        /// <param name="roundKey">round key to use for XOR</param>
	    private static void AddRoundKey(byte[][] bytes, byte[][] roundKey) {
            for (int i = 0; i < bytes.Length; i++)
                for (int j = 0; j < bytes[i].Length; j++)
                    bytes[i][j] ^= roundKey[i][j];
	    }

        /// <summary>
        /// Flattens a 2D state into a one-dimensional array.
        /// </summary>
        /// <seealso cref="Rijndael.Explode(byte[])"/>
        /// <param name="bytes">state to implode</param>
        /// <returns>One-dimensional representation of the input.</returns>
        private static byte[] Implode(byte[][] bytes)
        {
		    byte[] message = new byte[bytes.Length * bytes[0].Length];
		
		    for (int i = 0; i < bytes.Length; i++)
                Array.Copy(bytes[i], 0, message, i * bytes[i].Length, bytes[i].Length);
		
		    return message;
	    }
	
        /// <summary>
        /// Transforms a one-dimensional byte-array into a two-dimensional state.
        /// </summary>
        /// <param name="bytes">Array to transform</param>
        /// <returns>Two-dimensional state</returns>
	    private static byte[][] Explode(byte[] bytes) {
		    byte[][] state = new byte[4][];

            for (int i = 0; i < state.Length; i++)
                state[i] = new byte[state.Length];
		
		    for (int i = 0; i < 4; i++)
                Array.Copy(bytes, i * 4, state[i], 0, 4);
			
		    return state;
	    }
	
        /// <summary>
        /// Rotates an array by "shifting" to the left bytewise.
        /// </summary>
        /// <param name="bytes">Array to shift/rotate</param>
	    private static void Rotate(byte[] bytes) {
            byte tmp = bytes[0];
            bytes[0] = bytes[1];
            bytes[1] = bytes[2];
            bytes[2] = bytes[3];
            bytes[3] = tmp;
	    }

        /// <summary>
        /// Expands the input key to separate round keys using the Rijndael key scheduling algorithm.
        /// </summary>
        /// <param name="key">Base key</param>
        private void ExpandKey(byte[] key)
        {
            roundKeys = new byte[NR + 1][][];

            int i, j, k;

            for (i = 0; i < roundKeys.Length; i++) {
                roundKeys[i] = new byte[NB][];
                for (j = 0; j < roundKeys[i].Length; j++)
                    roundKeys[i][j] = new byte[4];
            }
        
            for (i = 0; i < NK; i++)
                Array.Copy(key, i * 4, roundKeys[0][i], 0, NK);

            byte[] temp;

            for (i = 1; i < roundKeys.Length; i++)
            {
                for (j = 0; j < roundKeys[i].Length; j++)
                {
                    temp = new byte[4];
                    Array.Copy(roundKeys[j == 0 ? i - 1 : i][(unchecked((uint)j - 1)) % 4], temp, temp.Length);

                    if ((i * 4 + j) % NK == 0)
                    {
                        Rotate(temp);
                        SubBytes(temp);
                        temp[0] ^= roundConstants[(i * 4 + j) / NK];
                    }
                    else if (NK > 6 && (i * 4 + j) % NK == 4)
                    {
                        SubBytes(temp);
                    }

                    for (k = 0; k < roundKeys[i][j].Length; k++)
                        roundKeys[i][j][k] = (byte)(roundKeys[i - 1][j][k] ^ temp[k]);
                }
            }
        }
		
        /// <summary>
        /// Encrypts a block of 16 bytes.
        /// </summary>
        /// <param name="message">Block to encrypt</param>
        /// <returns>Encrypted block</returns>
		public byte[] Encrypt(byte[] message)
		{
            if (message.Length != 16) throw new ArgumentException("A block must be exactly 16 bytes of length.");

			if (roundKeys == null) throw new InvalidOperationException("No key specified.");

            byte[][] state = Explode(message);
            AddRoundKey(state, roundKeys[0]);

            for (int i = 0; i < NR - 1; i++)
            {
                SubBytes(state);
                ShiftRows(state);
                MixColumns(state);
                AddRoundKey(state, roundKeys[i + 1]);
            }

            SubBytes(state);
            ShiftRows(state);
            AddRoundKey(state, roundKeys[NR]);

            return Implode(state);
		}

        /// <summary>
        /// Decrypts a block of 16 bytes.
        /// </summary>
        /// <param name="cipher">Block to decrypt</param>
        /// <returns>Decrypted block</returns>
		public byte[] Decrypt(byte[] cipher)
		{
            if (cipher.Length != 16) throw new ArgumentException("A block must be exactly 16 bytes of length.");

			if (roundKeys == null) throw new InvalidOperationException("No key specified.");

            byte[][] state = Explode(cipher);
            AddRoundKey(state, roundKeys[NR]);

            for (int i = NR - 1; i > 0; i--)
            {
                InvShiftRows(state);
                InvSubBytes(state);
                AddRoundKey(state, roundKeys[i]);
                InvMixColumns(state);
            }

            InvShiftRows(state);
            InvSubBytes(state);
            AddRoundKey(state, roundKeys[0]);

            return Implode(state);
		}
    }
}
