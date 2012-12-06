using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadena.Library.Serialization;

namespace AsyncPipes
{
    [Serializable]
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

        private byte[] _payLoad;

        public byte[] PayLoad
        {
            get { return _payLoad; }
            set { _payLoad = value; }
        }

        public WrapperMessage(object obj)
        {
            this.MessageId = Guid.NewGuid();
            _payLoad = ObjectSerializer.ToBinary(obj);
            _messageType = obj.GetType();
        }
    }
}
