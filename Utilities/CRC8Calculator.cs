namespace LightweightPlatform
{
    public static class CRC8Calculator
    {
        public static byte CalculateCRC(byte[] data)
        {
            byte crc = 0x00;
            foreach (var b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                {
                    crc = (crc & 0x80) != 0 ? (byte)((crc << 1) ^ 0x07) : (byte)(crc << 1);
                }
            }
            return crc;
        }
    }
}
