using ChartServer.Net.IO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ChartServer
{
    class Program
    {
        static List<Client> _user;
        static TcpListener _listener;
        static void Main(string[] args)
        {
            _user = new List<Client>();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            _listener.Start();

            while (true)
            {
                var client = new Client(_listener.AcceptTcpClient());                
               _user.Add(client);

                //Broadcast the connection to eyeryone join user
                BroadCastConnetion();
            }

        }

        static void BroadCastConnetion()
        {
            foreach (var user in _user)
            {
                foreach (var usr in _user)
                {
                    var broadcastPacket = new PacketBuilder();
                    //0와 달리 1을 보냄
                    broadcastPacket.WriteOpCode(1);
                    broadcastPacket.WriteMessage(user.Username);
                    broadcastPacket.WriteMessage(user.UID.ToString());
                    user.ClientSocket.Client.Send(broadcastPacket.GetPackByte());



                }
            }
        }
    }
}
