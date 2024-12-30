using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LightweightPlatform
{
    public class UserInterface
    {
        private readonly int port;

        public UserInterface(int port)
        {
            this.port = port;
        }

        public void SendCommand(PacketType type, byte[] data)
        {
            using (UdpClient udpClient = new UdpClient())
            {
                udpClient.Connect("localhost", port);

                Packet packet = new Packet(type, data);
                byte[] serializedPacket = packet.ToByteArray(true);
                udpClient.Send(serializedPacket, serializedPacket.Length);

                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] response = udpClient.Receive(ref remoteEndPoint);
                Packet responsePacket = Packet.Parse(response);

                Console.WriteLine($"User received response: {responsePacket.PacketID}");
            }
        }
    }
}
