using CryptoLab_Course3.Algo;
using System.Text;

namespace CryptoLab_Course3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введiть рядок для хешування:");
            string input = Console.ReadLine();

            #region Adler32Lab1
            uint myAdler32 = Adler32Impl.ComputeAdler32(Encoding.UTF8.GetBytes(input));

            byte[] hashBytes = BitConverter.GetBytes(myAdler32);
            if (BitConverter.IsLittleEndian) // Ставимо байти у правильний порядок
                Array.Reverse(hashBytes);

            string hex = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
              
            Console.WriteLine($"Мiй хеш по Adler32(uint): {myAdler32}");
            Console.WriteLine($"Мiй хеш по Adler32(hex): {hex}");
            Console.WriteLine($"Мiй хеш по Adler32(base64): {Convert.ToBase64String(hashBytes)}");
            #endregion
        }
    }
}
