using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncPipes
{
    [Serializable]
    public class PingMessage : AsyncPipes.IMessage
    {
        #region IMessage Members

        private Guid _messageId;                
        private Type _messageType;

        public Guid MessageId
        {
            get
            {
                return _messageId;
            }
            set
            {
                _messageId = value;
            }
        }

        public Type MessageType
        {
            get
            {
                return _messageType;
            }
            set
            {
                _messageType = value;
            }
        }

        public string Sender { get; private set; }

        #endregion IMessage Members

        public PingMessage(string sender = "")
        {
            _messageId = Guid.NewGuid();
            _messageType = typeof(PingMessage);
            this.Sender = sender;
        }
    }
}
