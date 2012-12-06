namespace AsyncPipes
{
    using System;

    [Serializable]
    public class MessageEventArgs : EventArgs
    {
        private readonly byte[] _Message;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public MessageEventArgs(byte[] message)
        {
            this._Message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public byte[] Message
        {
            get
            {
                return this._Message;
            }
        }
    }

    //[Serializable]
    //public class ClientConnectedEventArgs
    //{
    //    public string ClientName { get; private set; }

    //    public ClientConnectedEventArgs(string clientName)
    //    {
    //        ClientName = clientName;
    //    }
    //}

    //[Serializable]
    //public class ClientDisconnectedEventArgs
    //{
    //    public string ClientName { get; private set; }

    //    public ClientDisconnectedEventArgs(string clientName)
    //    {
    //        ClientName = clientName;
    //    }
    //}
}
