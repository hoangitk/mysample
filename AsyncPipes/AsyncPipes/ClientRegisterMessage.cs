using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncPipes
{
    [Serializable]
    public class ClientRegisterMessage : IMessage
    {
        private Guid _messageId;
        public Guid MessageId
        {
            get { return _messageId; }
            set { _messageId = value; }
        }

        private Type _messageType;
        public Type MessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }

        public string ClientName { get; private set; }

        public ClientRegisterMessage(string clientName)
        {
            _messageId = Guid.NewGuid();
            _messageType = typeof(ClientRegisterMessage);
            ClientName = clientName;
        }
    }
}
