namespace CryptoLab_Course3.Algo
{
    internal class XTEAImpl
    {
        private const int NumRounds = 32;
        private uint[] key;

        public XTEAImpl(byte[] keyBytes)
        {
            if (keyBytes.Length != 16)
                throw new ArgumentException("Ключ має бути довжиною у 128 біт.", nameof(keyBytes));

            key = new uint[4];
            Buffer.BlockCopy(keyBytes, 0, key, 0, 16);
        }

        public byte[] Encrypt(byte[] data)
        {
            if (data.Length % 8 != 0)
                throw new ArgumentException("Повідомлення має бути кратним 8.", nameof(data));

            byte[] encrypted = new byte[data.Length];
            for (int p = 0; p < data.Length; p += 8)
            {
                uint[] block = { BitConverter.ToUInt32(data, p), BitConverter.ToUInt32(data, p + 4) };
                Encode(block);
                Buffer.BlockCopy(block, 0, encrypted, p, 8);
            }
            return encrypted;
        }

        private void Encode(uint[] v)
        {
            uint v0 = v[0], v1 = v[1];
            uint sum = 0;
            uint delta = 0x9E3779B9;

            for (int i = 0; i < NumRounds; i++)
            {
                v0 += ((v1 << 4) ^ (v1 >> 5)) + v1 ^ (sum + key[sum & 3]);
                sum += delta;
                v1 += ((v0 << 4) ^ (v0 >> 5)) + v0 ^ (sum + key[(sum >> 11) & 3]);
            }

            v[0] = v0; v[1] = v1;
        }
    }
}
