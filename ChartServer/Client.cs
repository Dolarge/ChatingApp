using ChatClient.Net.IO;
using System;
using System.Net.Sockets;

namespace ChartServer
{
    class Client
    {
        public string Username { get; set; }
        public Guid UID { get; set; }
        public TcpClient ClientSocket { get; set; }
        PacketReader _packetReader;


        public Client(TcpClient client)
        {
            ClientSocket = client;            
            UID = Guid.NewGuid();

            //Read PacketReader
            _packetReader = new PacketReader(client.GetStream());
            var opcode = _packetReader.ReadByte();
            
            //get UserName
            Username = _packetReader.ReadMesassage();

            Console.WriteLine($"[{DateTime.Now}]: Client has connected with USERName(GUID) :{Username} ");


        }
    }
}
