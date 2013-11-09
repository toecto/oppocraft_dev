using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace testClient
{
    
    public class TcpMessageClient
    {
        private TcpClient ClientSocket=new TcpClient();
        private NetworkStream ServerStream;

        private LinkedList<byte[]> MessageList = new LinkedList<byte[]>();
        public delegate void onMessageHandler(TcpMessageClient x);
        public event onMessageHandler onMessage;
        private Thread ctThread=null;
        private bool Active=true;

        public int Available
        {
            get
            {
                return this.MessageList.Count;
            }
        }

        public TcpMessageClient(string IP, int Port)
        {
            this.ClientSocket.Connect(IP, Port);
            this.Start();
        }
        public TcpMessageClient(TcpClient ClientSocket)
        {
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
            this.Active = false;
            if (this.ctThread != null)
            {
                this.ctThread.Abort();
            }
            if (this.ClientSocket != null)
                this.ClientSocket.Close();
            this.ClientSocket = null;
            
        }

        private void receiveMessageLoop()
        {
            int buffSize = 0;
            int LeftToRead=0, AvailableToRead;
            byte[] buffer;
            while (this.Active)
            {
                try
                {
                    Debug.WriteLine("receiveMessageLoop");
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
                {
                    Debug.WriteLine(ex.Message);
                    return; }
                
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
