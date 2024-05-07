namespace CryptoLab_Course3.Algo
{
    internal class XTEAImpl
    {
        // Кількість раундів шифрування, типово для XTEA - 32 раунди.
        private const int NumRounds = 32;
        private uint[] key;

        public XTEAImpl(byte[] keyBytes)
        {
            if (keyBytes.Length != 16)
                throw new ArgumentException("Ключ має бути довжиною у 128 біт.", nameof(keyBytes));

            key = new uint[4];
            Buffer.BlockCopy(keyBytes, 0, key, 0, 16); // Конвертуємо байти в uint для легшого доступу під час шифрування.
        }

        public byte[] Encrypt(byte[] data)
        {
            if (data.Length % 8 != 0)
                throw new ArgumentException("Повідомлення має бути кратним 8.", nameof(data));

            byte[] encrypted = new byte[data.Length];
            for (int p = 0; p < data.Length; p += 8) // Обробляємо кожен блок даних окремо.
            {
                uint[] block = { BitConverter.ToUInt32(data, p), BitConverter.ToUInt32(data, p + 4) };
                Encode(block); // Шифрування кожного блоку.
                Buffer.BlockCopy(block, 0, encrypted, p, 8); // Запис зашифрованого блоку назад у масив.
            }
            return encrypted;
        }

        // Внутрішній метод шифрування одного блоку даних.
        private void Encode(uint[] v)
        {
            uint v0 = v[0], v1 = v[1]; // Розбиваємо блок на дві частини.
            uint sum = 0;
            uint delta = 0x9E3779B9; // Для забезпечення псевдовипадковості в раундах.

            for (int i = 0; i < NumRounds; i++) // Проводимо задану кількість раундів.
            {
                v0 += ((v1 << 4) ^ (v1 >> 5)) + v1 ^ (sum + key[sum & 3]); // Оновлення v0.
                sum += delta; // Збільшення суми на дельту.
                v1 += ((v0 << 4) ^ (v0 >> 5)) + v0 ^ (sum + key[(sum >> 11) & 3]); // Оновлення v1.
            }

            v[0] = v0; v[1] = v1; // Запис оновлених значень назад у блок.
        }
    }
}
