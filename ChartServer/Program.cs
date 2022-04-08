using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ChartServer
{
    internal class Program
    {
        static List<Client> _user;
        static TcpListener _listener;
        
        static void Main(string[] args)
        {
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            _listener.Start(); 

            var client = new Client(_listener.AcceptTcpClient());
            _user.Add(client);
            Console.WriteLine("Client Connect");

        }
    }
}
