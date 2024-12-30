using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LightweightPlatform
{
    public class Packet
    {
        public byte SyncByte1 { get; } = 0xA9;
        public byte SyncByte2 { get; } = 0xE9;
        public byte PacketID { get; }
        public byte Length { get; }
        public byte[] Data { get; }
        public byte CRC { get; }

        public Packet(PacketType type, byte[] data)
        {
            PacketID = (byte)type;
            Data = data;
            Length = (byte)(5 + data.Length); // SyncBytes + ID + Length + Data + CRC
            CRC = CRC8Calculator.CalculateCRC(ToByteArray(false));
        }

        public byte[] ToByteArray(bool includeCRC)
        {
            var bytes = new List<byte> { SyncByte1, SyncByte2, PacketID, Length };
            bytes.AddRange(Data);
            if (includeCRC) bytes.Add(CRC);
            return bytes.ToArray();
        }

        public static Packet Parse(byte[] receivedData)
        {
            if (receivedData.Length < 5)
                throw new InvalidDataException("Invalid packet: too short");

            if (receivedData[0] != 0xA9 || receivedData[1] != 0xE9)
                throw new InvalidDataException("Invalid synchronization bytes");

            byte packetID = receivedData[2];
            byte length = receivedData[3];
            byte[] data = receivedData.Skip(4).Take(length - 5).ToArray();
            byte crc = receivedData[^1];

            // Validate CRC
            byte calculatedCRC = CRC8Calculator.CalculateCRC(receivedData.Take(receivedData.Length - 1).ToArray());
            if (crc != calculatedCRC)
                throw new InvalidDataException("CRC validation failed");

            return new Packet((PacketType)packetID, data);
        }
    }
}
