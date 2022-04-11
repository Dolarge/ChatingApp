using ChatClient.MVVM.Core;
using ChatClient.MVVM.Model;
using ChatClient.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient.MVVM.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }

        public RelayCommand ConnectToServerCommand { get; set; }
        public string UserName { get;set; }

        private Server _server;
        public MainViewModel()
        {
            Users =  new ObservableCollection<UserModel>();
            _server = new Server();
            _server.connectedEvent += UserConnected;
            //if UserName is empty, cant Run RelayCommand
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(UserName),o=>!string.IsNullOrEmpty(UserName));
        }

        private void UserConnected()
        {
            var user = new UserModel
            {
                UserName = _server.packetReader.ReadMessage(),
                UID = _server.packetReader.ReadMessage()
            };


            if (!Users.Any(x=>x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }

        }
    }
}
