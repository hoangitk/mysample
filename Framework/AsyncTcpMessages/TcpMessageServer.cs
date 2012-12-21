using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTcpMessages
{
    public class TcpMessageServer : TcpMessageBase
    {
        private TcpListener _tcpListener;

        private List<TcpMessageConnection> _connections;

        public List<TcpMessageConnection> Connections
        {
            get
            {
                return _connections;
            }
        }

        public TcpMessageServer()
        {
            _connections = new List<TcpMessageConnection>();
        }

        public TcpMessageServer(IPEndPoint localEP) : this()
        {
            _tcpListener = new TcpListener(localEP);
        }

        public TcpMessageServer(IPAddress localAddr, int port) : this()
        {
            _tcpListener = new TcpListener(localAddr, port);
        }

        public IEnumerable<TcpClient> TcpClients
        {
            get
            {
                foreach (var client in _connections)
                {
                    yield return client.TcpClient;
                }
            }
        }

        private void WaitForNewConnection()
        {
            Task.Factory.FromAsync<TcpClient>(_tcpListener.BeginAcceptTcpClient, _tcpListener.EndAcceptTcpClient, _tcpListener)
                .ContinueWith(t =>
                {
                    var tcpClient = t.Result;

                    var newConnection = new TcpMessageConnection(tcpClient, tcpClient.ReceiveBufferSize);
                    
                    OnClientConnected(newConnection);

                    newConnection.MessageReceived += RaiseMessageReceived;
                    newConnection.ClientDisconnected += RaiseClientDisconnected;

                    lock (_connections)
                    {
                        _connections.Add(newConnection);
                    }

                    newConnection.StartIncomingMessageLoop();

                    WaitForNewConnection();
                });
        }

        private void RaiseClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            OnClientDisconnected(sender, e.Exception);
        }

        private void RaiseMessageReceived(object sender, MessageEventArgs e)
        {
            OnMessageReceived(sender, e.PayLoad);
        }                

        public override void Send(object obj)
        {
            lock (_connections)
            {
                foreach (var cnn in _connections)
                {
                    cnn.Send(obj);
                }
            }
        }

        public void Start()
        {
            _tcpListener.Start();
            WaitForNewConnection();           
        }

        public void Stop()
        {
            _tcpListener.Stop();
            lock (_tcpListener)
            {
                foreach (var cnn in _connections)
                {
                    cnn.TcpClient.Client.Disconnect(false);
                }
                _connections.Clear();
            }
        }
        
    }
}
