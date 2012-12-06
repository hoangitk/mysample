using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncPipes
{
    public abstract class PipeBase : IDisposable
    {
        private readonly object _lockEvent = new object();

        public static readonly int BUFFER_LENGTH = 0x1000;
        
        public string PipeName { get; set; }

        public PipeBase(string pipeName = "")
        {
            this.PipeName = pipeName;
        }

        private event EventHandler<MessageEventArgs> _receivedMessage;
        public event EventHandler<MessageEventArgs> ReceivedMessage
        {
            add
            {
                lock (_lockEvent)
                {
                    _receivedMessage = (EventHandler<MessageEventArgs>)Delegate.Combine(_receivedMessage, value);
                }
            }

            remove
            {
                lock (_lockEvent)
                {
                    _receivedMessage = (EventHandler<MessageEventArgs>)Delegate.Remove(_receivedMessage, value);
                }
            }
        }

        protected virtual void OnReceivedMessage(MessageEventArgs args)
        {
            lock (_lockEvent)
            {
                if (_receivedMessage != null)
                {
                    _receivedMessage(this, args);
                } 
            }
        }

        public abstract void Start();
        public abstract void Send(byte[] message);

        public virtual void Disconnect()
        {
            lock (_lockEvent)
            {
                _receivedMessage = null;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~PipeBase()
        {
            Dispose(false);
        }
    }
}
