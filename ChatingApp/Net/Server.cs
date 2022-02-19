using ChatClient.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Net
{
    public class Server
    {
        TcpClient _client;
        public PacketReader packetReader;

        public event Action connectedEvent;


        public Server()
        {
            _client = new TcpClient();

        }


        //if not connected Server, Connect Server
        public void ConnectToServer(string UserName)
        {
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1", 7891);
                packetReader = new PacketReader(_client.GetStream());

                if (!string.IsNullOrEmpty(UserName))
                {
                    var connectPacket = new PacketBuilder();

                    //send Packet message to server
                    connectPacket.WriteOpCode(0);
                    connectPacket.WriteMessage(UserName);
                    _client.Client.Send(connectPacket.GetPackByte());

                }
                ReadPackets();

            }
        }

        //dead lock
        private void ReadPackets()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var opcode = packetReader.ReadByte();
                    switch (opcode)
                    {
                        case 1:
                            connectedEvent?.Invoke();
                            break;
                        
                        default:
                            Console.WriteLine("ReadPackets error");
                            break;
                    }
                }
            });
        }
    }
}
