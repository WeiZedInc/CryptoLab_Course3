using CryptoLab_Course3.Algo;
using System.Text;

namespace CryptoLab_Course3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введiть рядок для хешування:");
            //string input = Console.ReadLine();
            string input = "cocos";

            #region Adler32Lab1
            // https://hash.online-convert.com/ru/adler32-generator -для перевірки

            uint myAdler32 = Adler32Impl.ComputeAdler32(Encoding.UTF8.GetBytes(input));

            byte[] hashBytes = BitConverter.GetBytes(myAdler32);
            if (BitConverter.IsLittleEndian) // Ставимо байти у правильний порядок
                Array.Reverse(hashBytes);

            string hex = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
              
            Console.WriteLine($"Мiй хеш по Adler32(uint): {myAdler32}");
            Console.WriteLine($"Мiй хеш по Adler32(hex): {hex}");
            Console.WriteLine($"Мiй хеш по Adler32(base64): {Convert.ToBase64String(hashBytes)}");
            #endregion

            Console.WriteLine("----------------------------");

            #region XTEALab2
            // https://asecuritysite.com/encryption/xtea -для перевірки

            byte[] key = Encoding.UTF8.GetBytes("0123456789012345"); // довжина має дорівнювати 128-и бітам.
            byte[] plaintext = Encoding.UTF8.GetBytes("ABCDEFGH"); //довжина має бути кратною 8-ми

            XTEAImpl xtea = new XTEAImpl(key);
            byte[] ciphertext = xtea.Encrypt(plaintext);
            
            Console.WriteLine($"Мiй хеш по XTEA: {BitConverter.ToString(ciphertext)}");
            #endregion
        }
    }
}
