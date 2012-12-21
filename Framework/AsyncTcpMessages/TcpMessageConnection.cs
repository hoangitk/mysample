using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Cadena.Library.Serialization;

namespace AsyncTcpMessages
{
    public class TcpMessageConnection : TcpMessageBase
    {
        private byte[] Header = System.Text.ASCIIEncoding.ASCII.GetBytes("<--");
        private byte[] Footer = System.Text.ASCIIEncoding.ASCII.GetBytes("-->");

        public TcpClient TcpClient { get; private set; }

        public int BufferLength { get; private set; }

        public NetworkStream Stream
        {
            get
            {
                try
                {
                    return TcpClient.GetStream();
                }
                catch
                {
                    return null;
                }
            }
        }

        public TcpMessageConnection(TcpClient tcpClient, int bufferLength)
        {
            if (tcpClient == null)
                throw new ArgumentNullException("TcpClient");

            if (bufferLength <= 0)
                throw new ArgumentException("BufferLength must be greate than 1");

            this.TcpClient = tcpClient;
            this.BufferLength = bufferLength;
        }

        public TcpMessageConnection(int bufferLength)
            : this(new TcpClient(), bufferLength)
        {
        }

        public TcpMessageConnection()
            : this(new TcpClient(), 0x1000)
        {
        }

        public void StartIncomingMessageLoop()
        {
            var read = Task.Factory.StartNew(ReadMessage);

            read.ContinueWith(t =>
            {
                OnClientDisconnected(this, t.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.AttachedToParent);

            read.ContinueWith(t =>
            {
                if (this.TcpClient != null)
                {
                    if (this.Stream != null)
                        this.Stream.Close();

                    this.TcpClient.Close();
                }
            });
        }

        private void ReadMessage()
        {
            NetworkStream stream = this.Stream;
            byte[] buf = new byte[this.BufferLength];
            List<byte> msgBuf = new List<byte>();
            int readLength = -1;

            do
            {
                readLength = stream.Read(buf, 0, buf.Length);
                msgBuf.AddRange(buf.Take(readLength));

                if (msgBuf.Count != 0)
                {
                    int msgStart = -1;

                    while (msgBuf.Count != 0 && (msgStart = msgBuf.IndexOf(Footer[0], msgStart + 1)) != -1)
                    {
                        bool footerFound = true;

                        if (msgStart + Footer.Length <= msgBuf.Count)
                        {
                            for (int i = 0; i < Footer.Length; i++)
                            {
                                if (msgBuf[msgStart + i] != Footer[i])
                                {
                                    footerFound = false;
                                    break;
                                }
                            }

                            if (footerFound)
                            {
                                byte[] message = msgBuf.Skip(Header.Length).Take(msgStart - Header.Length).ToArray();

                                if (message.Length > 0)
                                {
                                    OnMessageReceived(this, message);

                                    msgBuf.RemoveRange(0, msgStart + Footer.Length);
                                }
                                msgStart = 0;
                            }
                        }
                    }
                }
            } while (readLength != 0);
        }

        public override void Send(object obj)
        {
            byte[] data = ObjectSerializer.ToBinary(obj);

            if (data == null)
                data = new byte[0];

            NetworkStream stream = this.Stream;

            //Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, Header, 0, Header.Length, null)
            //    .ContinueWith(t1 =>
            //    {
            //        Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, data, 0, data.Length, null)
            //            .ContinueWith(t2 =>
            //            {
            //                Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, Footer, 0, Footer.Length, null);
            //            });
            //    }, TaskContinuationOptions.NotOnFaulted);

            stream.Write(Header, 0, Header.Length);
            stream.Write(data, 0, data.Length);
            stream.Write(Footer, 0, Footer.Length);
        }

        #region Object override

        public static bool operator ==(TcpMessageConnection c1, TcpMessageConnection c2)
        {
            if (c1.TcpClient != c2.TcpClient || c1.Stream != c2.Stream)
                return false;
            else
                return true;
        }

        public static bool operator !=(TcpMessageConnection c1, TcpMessageConnection c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object obj)
        {
            return this == (TcpMessageConnection)obj;
        }

        public override int GetHashCode()
        {
            return this.TcpClient.GetHashCode();
        }

        #endregion Object override
    }
}