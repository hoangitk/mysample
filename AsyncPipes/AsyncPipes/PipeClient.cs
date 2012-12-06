using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace AsyncPipes
{
    public class PipeClient : PipeBase
    {
        public string PipeServerName { get; set; }

        protected NamedPipeClientStream clientPipe;

        public PipeClient(string pipeName, string pipeServerName) : base(pipeName)
        {
            this.PipeServerName = pipeServerName;

            clientPipe = new NamedPipeClientStream(
                ".", this.PipeServerName,
                PipeDirection.InOut, PipeOptions.Asynchronous);            
        }

        public override void Start()
        {
            clientPipe.Connect();
            clientPipe.ReadMode = PipeTransmissionMode.Message;

            var read = Observable.FromAsyncPattern<byte[], int, int, int>(clientPipe.BeginRead, clientPipe.EndRead);

            byte[] buf = new byte[BUFFER_LENGTH];

            Observable.While(() => clientPipe.IsConnected, Observable.Defer(() => read(buf, 0, buf.Length)))
                .ObserveOn(Scheduler.Default)
                .Subscribe(length =>
                {
                    byte[] destArray = new byte[length];
                    Array.Copy(buf, 0, destArray, 0, length);
                    OnReceivedMessage(new MessageEventArgs(destArray));
                },
                ex => { Debug.Print(ex.ToString()); });
        }

        public override void Send(byte[] message)
        {
            var write = Observable.FromAsyncPattern<byte[], int, int>(clientPipe.BeginWrite, clientPipe.EndWrite);

            write(message, 0, message.Length).Subscribe(u =>
                {
                    Debug.Print(message.Length.ToString());
                },
                ex => { Debug.Print(ex.ToString()); });
        }
    }
}
