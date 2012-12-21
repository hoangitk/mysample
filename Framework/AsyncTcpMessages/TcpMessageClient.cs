using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Cadena.Library.Serialization;

namespace AsyncTcpMessages
{
    public class TcpMessageClient
    {
        private TcpMessageConnection _client;

        public TcpMessageClient()
        {
            _client = new TcpMessageConnection();
        }

        public void Connect(string host, int port)
        {
            _client.TcpClient.Connect(host, port);
            _client.StartIncomingMessageLoop();

            _client.MessageReceived += (s, e) =>
            {
                OnMessageReceived(e.PayLoad);
            };

            _client.ClientDisconnected += (s, e) =>
            {
                OnClientDisconnected(e.Exception);
            };            
        }

        public void Send(object obj)
        {           
            _client.Send(obj);
        }

        public EventHandler<MessageEventArgs> MessageReceived;
        protected virtual void OnMessageReceived(byte[] data)
        {
            EventHandler<MessageEventArgs> handler = MessageReceived;
            if (handler != null)
            {
                MessageReceived(this, new MessageEventArgs(data));
            }
        }

        public EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        protected virtual void OnClientDisconnected(Exception e)
        {
            EventHandler<ClientDisconnectedEventArgs> handler = ClientDisconnected;
            if (handler != null)
            {
                ClientDisconnected(this, new ClientDisconnectedEventArgs(e));
            }
        }
    }
}
