using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncTcpMessages
{
    public abstract class TcpMessageBase : IDisposable
    {
        #region Received Event
        private readonly object _lockReceivedEvent = new object();

        private EventHandler<MessageEventArgs> _messageReceived;
        public event EventHandler<MessageEventArgs> MessageReceived
        {
            add
            {
                lock (_lockReceivedEvent)
                {
                    _messageReceived = (EventHandler<MessageEventArgs>)Delegate.Combine(_messageReceived, value);
                }
            }

            remove
            {
                lock (_lockReceivedEvent)
                {
                    _messageReceived = (EventHandler<MessageEventArgs>)Delegate.Remove(_messageReceived, value);
                }
            }
        }

        protected virtual void OnMessageReceived(object connection, byte[] data)
        {
            lock (_lockReceivedEvent)
            {
                if (_messageReceived != null)
                {
                    _messageReceived(connection, new MessageEventArgs(data));
                }
            }
        } 
        #endregion

        #region ClientConnect Event
        private readonly object _lockClientConnectedEvent = new object();

        private EventHandler _clientConnected;
        public event EventHandler ClientConnected
        {
            add
            {
                lock (_lockClientConnectedEvent)
                {
                    _clientConnected = (EventHandler)Delegate.Combine(_clientConnected, value);
                }
            }

            remove
            {
                lock (_lockClientConnectedEvent)
                {
                    _clientConnected = (EventHandler)Delegate.Remove(_clientConnected, value);
                }
            }
        }

        protected virtual void OnClientConnected(object connection)
        {
            lock (_lockClientConnectedEvent)
            {
                if (_clientConnected != null)
                {
                    _clientConnected(connection, EventArgs.Empty);
                }
            }
        }
        #endregion

        #region ClientDisconnected Event
        private readonly object _lockClientDisconnectedEvent = new object();

        private EventHandler<ClientDisconnectedEventArgs> _clientDisconnected;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected
        {
            add
            {
                lock (_lockClientDisconnectedEvent)
                {
                    _clientDisconnected = (EventHandler<ClientDisconnectedEventArgs>)Delegate.Combine(_clientDisconnected, value);
                }
            }

            remove
            {
                lock (_lockClientDisconnectedEvent)
                {
                    _clientDisconnected = (EventHandler<ClientDisconnectedEventArgs>)Delegate.Remove(_clientDisconnected, value);
                }
            }
        }

        protected virtual void OnClientDisconnected(object connection, Exception args)
        {
            lock (_lockClientDisconnectedEvent)
            {
                if (_clientDisconnected != null)
                {
                    _clientDisconnected(connection, new ClientDisconnectedEventArgs(args));
                }
            }
        }
        #endregion

        public abstract void Send(object obj);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        ~TcpMessageBase()
        {
            this.Dispose(false);
        }
    }
}
