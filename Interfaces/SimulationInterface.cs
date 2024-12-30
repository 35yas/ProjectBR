using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LightweightPlatform
{
    public class SimulationInterface
    {
        private readonly int port;

        public SimulationInterface(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            using (UdpClient udpServer = new UdpClient(port))
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

                while (true)
                {
                    try
                    {
                        byte[] receivedData = udpServer.Receive(ref remoteEndPoint);
                        Packet packet = Packet.Parse(receivedData);
                        Console.WriteLine($"Simulation received valid packet: {packet.PacketID}");

                        // Simulate response
                        byte[] responseData = new Packet(PacketType.FeedbackPacket, Encoding.UTF8.GetBytes("Response")).ToByteArray(true);
                        udpServer.Send(responseData, responseData.Length, remoteEndPoint);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }
    }
}
