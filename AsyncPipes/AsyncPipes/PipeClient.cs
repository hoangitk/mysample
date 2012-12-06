using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace AsyncPipes
{
    public class PipeClient : PipeBase
    {
        private ManualResetEvent _connectGate;
        
        private readonly object _lockStream = new object();

        public string PipeServerName { get; set; }

        protected NamedPipeClientStream clientPipe;

        public PipeClient(string pipeName, string pipeServerName) : base(pipeName)
        {
            _connectGate = new ManualResetEvent(false);

            this.PipeServerName = pipeServerName;

            clientPipe = new NamedPipeClientStream(
                ".", this.PipeServerName,
                PipeDirection.InOut, PipeOptions.Asynchronous);            
        }

        public override void Start()
        {
            _connectGate.Reset();
            Thread pipeThread = new Thread(new ThreadStart(TryConnect));
            pipeThread.IsBackground = true;
            pipeThread.Start();
        }

        private void TryConnect()
        {
            _connectGate.Reset();

            lock (_lockStream)
            {
                var read = Observable.FromAsyncPattern<byte[], int, int, int>(clientPipe.BeginRead, clientPipe.EndRead);

                clientPipe.Connect();
                clientPipe.ReadMode = PipeTransmissionMode.Message;

                byte[] buf = new byte[BUFFER_LENGTH];

                read(buf, 0, buf.Length).Subscribe(length =>
                {
                    byte[] destArray = new byte[length];
                    Array.Copy(buf, 0, destArray, 0, length);

                    OnReceivedMessage(new MessageEventArgs(destArray));
                });

                _connectGate.Set();
            }
        }

        public override void Send(byte[] message)
        {
            var write = Observable.FromAsyncPattern<byte[], int, int>(clientPipe.BeginWrite, clientPipe.EndWrite);

            write(message, 0, message.Length).Subscribe(u =>
                {
                    Debug.Print(message.Length.ToString());
                });
        }
    }
}
