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
        PacketBuilder _packetBuilder;

        public Server()
        {
            _client= new TcpClient();

        }


        //if not connected Server, Connect Server
        public void ConnectToServer(string UserName)
        {
            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1", 7891);
                var connectPacket = new PacketBuilder();

                //send Packet message to server
                connectPacket.WriteOpCode(0);
                connectPacket.WriteString(UserName);
                _client.Client.Send(connectPacket.GetPackByte());
                
            }
        }
    }
}
