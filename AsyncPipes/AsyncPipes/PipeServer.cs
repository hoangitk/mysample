using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace AsyncPipes
{
    public class PipeServer : PipeBase
    {  
        private List<PipeConnection> _connections;        
        
        public PipeServer(string pipeName) : base(pipeName)
        {
            _connections = new List<PipeConnection>();       
        }

        public override void Start()
        {
            var pipeStream = new NamedPipeServerStream(
                this.PipeName, PipeDirection.InOut, -1,
                PipeTransmissionMode.Message, PipeOptions.Asynchronous);

            WaitNewConnection(pipeStream);
        }

        private void WaitNewConnection(NamedPipeServerStream pipeStream)
        {
            var connect = Observable.FromAsyncPattern(pipeStream.BeginWaitForConnection, pipeStream.EndWaitForConnection);

            connect().Subscribe(u =>
            {
                if (pipeStream.IsConnected)
                {
                    var newPipeConnection = new PipeConnection(pipeStream);
                    newPipeConnection.ReceivedMessage += PipeConnection_ReceivedMessage;

                    lock (_connections) _connections.Add(newPipeConnection);
                }

                Debug.Print(_connections.Count.ToString());

                var newPipeStream = new NamedPipeServerStream(
                    this.PipeName, PipeDirection.InOut, -1,
                    PipeTransmissionMode.Message, PipeOptions.Asynchronous);
                
                WaitNewConnection(newPipeStream);
            });
        }

        void PipeConnection_ReceivedMessage(object sender, MessageEventArgs e)
        {
            OnReceivedMessage(e);
        }

        public override void Send(byte[] message)
        {
            List<PipeConnection> failedConnections = new List<PipeConnection>();

            lock (_connections)
            {
                foreach (var cnn in _connections)
                {
                    try
                    {   
                        cnn.Send(message);
                    }
                    catch
                    {                        
                        failedConnections.Add(cnn);
                    }
                }

                foreach (var failedCnn in failedConnections)
                {                    
                    _connections.Remove(failedCnn);
                }
            }
        }
    }
}
