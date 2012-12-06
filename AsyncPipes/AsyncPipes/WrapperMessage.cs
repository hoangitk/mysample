using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncPipes
{
    public class WrapperMessage : IMessage
    {
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

        public byte[] PayLoad { get; set; }

        public WrapperMessage()
        {
            this.MessageId = Guid.NewGuid();
        }
    }
}
