using ChatClient.MVVM.Core;
using ChatClient.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.MVVM.ViewModel
{
    class MainViewModel
    {

        public RelayCommand ConnectToServerCommand { get; set; }
        public string UserName { get;set; }

        private Server _server;
        public MainViewModel()
        {
            _server = new Server();
            _server.connectedEvent += UserConnected;
            //if UserName is empty, cant Run RelayCommand
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(UserName),o=>!string.IsNullOrEmpty(UserName));
        }

        private void UserConnected()
        {
            throw new NotImplementedException();
        }
    }
}
