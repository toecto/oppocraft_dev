using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace testClient
{
    
    public class TcpMessageClient
    {
        private TcpClient ClientSocket;
        private NetworkStream ServerStream;
        private IPAddress IP;
        private int Port;

        private LinkedList<byte[]> MessageList = new LinkedList<byte[]>();
        public delegate void onMessageHandler(TcpMessageClient x);
        public event onMessageHandler onMessage;
        private Thread ctThread=null;

        public int Available
        {
            get
            {
                return this.MessageList.Count;
            }
        }

        public TcpMessageClient(string IP, int Port)
        {
            this.IP = IPAddress.Parse(IP);
            this.Port = Port;
        }
        public TcpMessageClient(TcpClient ClientSocket)
        {
            this.Connect(ClientSocket);
        }


        public void Connect()
        {
            this.Stop();
            this.ClientSocket.Connect(this.IP, this.Port);
            this.Start();
        }

        public void Connect(TcpClient ClientSocket)
        {
            this.Stop();
            this.ClientSocket = ClientSocket;
            this.Start();
        }

        private void Start()
        {
            this.ServerStream = this.ClientSocket.GetStream();
            this.ctThread = new Thread(this.receiveMessageLoop);
            ctThread.Start();
        }

        public void Stop()
        {
            if (this.ctThread!=null)
                this.ctThread.Join();

            if (this.ClientSocket != null)
                this.ClientSocket.Close();
            ClientSocket = new TcpClient();
        }

        private void receiveMessageLoop()
        {
            int buffSize = 0;
            int LeftToRead=0, AvailableToRead;
            byte[] buffer;
            while (true)
            {
                try
                {
                    buffer = new Byte[sizeof(Int32)];
                    ServerStream.Read(buffer, 0, sizeof(Int32));
                    buffSize = BitConverter.ToInt32(buffer, 0);
                    buffer = new Byte[buffSize];
                    LeftToRead = buffSize;

                    while (LeftToRead > 0)
                    {
                        AvailableToRead = ClientSocket.Available;
                        if (AvailableToRead > LeftToRead) AvailableToRead = LeftToRead;

                        ServerStream.Read(buffer, buffSize - LeftToRead, AvailableToRead);
                        LeftToRead -= AvailableToRead;
                    }
                    /**/
                    lock (this.MessageList)
                    {
                        this.MessageList.AddLast(buffer);
                    }

                    if (this.onMessage != null)
                        this.onMessage(this);
                }
                catch (Exception ex)
                { return; }
                
            }
        }

        public byte[] getMessage()
        {
            if (this.MessageList.Count == 0) return null;
            byte[] Message;
            lock (this.MessageList)
            {
                Message = this.MessageList.First.Value;
                this.MessageList.RemoveFirst();
            }
            return Message;
        }

        public void sendMessage(byte[] msg)
        {
            byte[] rez = new byte[msg.Length + sizeof(Int32)];
            BitConverter.GetBytes((Int32)msg.Length).CopyTo(rez,0);
            msg.CopyTo(rez, sizeof(Int32));
            ServerStream.Write(rez, 0, rez.Length);
            ServerStream.Flush();
        }


    }
}
