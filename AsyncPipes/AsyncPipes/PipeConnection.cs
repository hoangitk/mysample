using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace AsyncPipes
{
    public class PipeConnection : PipeBase
    {
        private readonly PipeStream _pipeStream;

        public PipeConnection(PipeStream pipeStream, string pipeName = "") : base(pipeName)
        {
            _pipeStream = pipeStream;
            Start();
        }

        public override void Start()
        {
            var read = Observable.FromAsyncPattern<byte[], int, int, int>(_pipeStream.BeginRead, _pipeStream.EndRead);

            byte[] buf = new byte[BUFFER_LENGTH];
            read(buf, 0, buf.Length).Subscribe(length =>
            {
                byte[] destArray = new byte[length];
                Array.Copy(buf, 0, destArray, 0, length);
                OnReceivedMessage(new MessageEventArgs(destArray));
            });
        }

        public override void Send(byte[] message)
        {
            var write = Observable.FromAsyncPattern<byte[], int, int>(_pipeStream.BeginWrite, _pipeStream.EndWrite);
            
            write(message, 0, message.Length).Subscribe(u =>
            {
            });
        }
    }
}
