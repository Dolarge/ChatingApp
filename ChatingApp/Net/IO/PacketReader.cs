﻿using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ChatClient.Net.IO
{
    public class PacketReader: BinaryReader
    {
        private NetworkStream _ns;
        


        public  PacketReader(NetworkStream ns) : base(ns)
        {
            _ns = ns;
        }


        public string ReadMessage()
        {
            byte[] msgBuffer;
            var length = ReadInt32();
            msgBuffer = new byte[length];

            var msg = Encoding.ASCII.GetString(msgBuffer);
            return msg;
        }
    }
}
