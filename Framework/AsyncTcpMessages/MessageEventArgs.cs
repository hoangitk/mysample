using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncTcpMessages
{
    public class MessageEventArgs : EventArgs
    {
        public byte[] PayLoad { get; set; }

        public MessageEventArgs(byte[] data)
        {
            PayLoad = data;
        }
    }

    public class ClientDisconnectedEventArgs : EventArgs
    {
        public Exception Exception { get; set; }

        public ClientDisconnectedEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }
}
