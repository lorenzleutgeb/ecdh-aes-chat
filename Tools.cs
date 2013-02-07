using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;

namespace LeutgebAes
{
    internal static class Tools
    {
        private static RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        private static Random unsafeRandom = new Random();

        public static string ToString(byte value)
        {
            return value.ToString("X").PadLeft(2, '0');
        }

        public static string ToString(byte[] array)
        {
            string result = "";

            foreach (byte item in array)
                result += ToString(item); // + " ";

            if (result == "")
                return result;

            return result.Substring(0, result.Length - 1);
        }

        public static string ToString(byte[][] matrix)
        {
            string result = "";

            foreach (byte[] array in matrix)
                result += ToString(array) + "\r\n";

            return result.Substring(0, result.Length - 2);
        }
		
        public static byte[] StringToBytes(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new ArgumentException("The string to read cannot have an odd number of digits.", "hex");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; i++)
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));

            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        public static BigInteger RandomBigInteger(int bytes)
        {
            BigInteger result = new BigInteger(Tools.RandomBytes(bytes));
            return result < BigInteger.One ? ~result : result;
        }

        public static byte[] RandomBytes(int bytes)
        {
            byte[] value = new byte[bytes];
            random.GetBytes(value);
            return value;
        }

        public static int GetUnsafeRandomInt(int min, int max)
        {
            return unsafeRandom.Next(min, max);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Log(object message)
        {
            MethodBase method = new StackFrame(1, true).GetMethod();
            Console.WriteLine(method.DeclaringType.Name + "." + method.Name + ": " + message);
        }

        public static bool IsSet(byte[] array, int bitIndex)
        {
            int byteIndex = bitIndex / 8;
            int bitOffset = bitIndex % 8;

            byte mask = (byte)(1 << bitOffset);

            return ((byte)(array[byteIndex] & mask)) > 0;
        }

        public static BigInteger SafeMod(BigInteger x, BigInteger m)
        {
            while (x < BigInteger.Zero)
                x = x % m + m;

            return x % m;
        }

        public static BigInteger ModularInverse(BigInteger k, BigInteger n)
        {
            if (n < BigInteger.One + BigInteger.One)
                throw new ArgumentException("Cannot perform modulo operation against a BigInteger less than 2.", "n");

            if (BigInteger.Abs(k) >= n)
                k %= n;

            if (k.Sign < 0)
                k += n;

            if (k.Sign == 0)
                throw new ArgumentException("Cannot obtain the modular inverse of 0.", "k");

            BigInteger v = BigInteger.Zero, x = n, y = k;

            if ((x.Sign < 0) || (y.Sign < 0))
                throw new ArgumentException("Cannot compute the GCD of negative BigIntegers.");

            BigInteger v1 = BigInteger.One, v2 = BigInteger.Zero, q, r;

            while (y.Sign > 0)
            {
                q = x / y;
                r = x - q * y;
                v = v2 - q * v1;

                x = y;
                y = r;
                v2 = v1;
                v1 = v;
                v = v2;
            }

            if (BigInteger.One != x)
                throw new ArgumentException("Cannot obtain the modular inverse of a number that is not coprime with the modulus.", "x");

            return v.Sign < 0 ? v + n : v;
        }
    }
}
