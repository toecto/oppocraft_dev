using System;
using testClient;

namespace OppoCraft{
    public class NetworkModule
    {

        TcpMessageClient net;
        public OppoMessageCollection buffer= new OppoMessageCollection();

        public delegate void onMessageHandler(NetworkModule x);
        public event onMessageHandler onMessage=null;

        public NetworkModule(string IP, int port = 8898)
        {
            this.net = new TcpMessageClient(IP, port);
            this.net.onMessage+=readMessage;
            this.Send(new OppoMessage(OppoMessageType.GetClientID));
            this.Flush();
        }

        void readMessage(TcpMessageClient client)
        {
            if (client.Available > 0)
            {
                lock (this.buffer)
                {
                    OppoMessage msg=null;
                    try{
                        msg=OppoMessage.fromBin(client.getMessage());
                    }
                    catch(Exception ex)
                    {}
                    if (msg != null)
                    {
                        this.buffer.AddLast(msg);

                        if (this.onMessage != null)
                            this.onMessage(this);
                    }
                }
                
            }
        }

        public void Flush()
        {
            this.net.Flush();
        }

        public void Stop()
        {
            this.net.Stop();
        }

        public void Send(OppoMessage msg)
        {
            this.net.sendMessage(msg.toBin());
        }

        public OppoMessage getMessage()
        {
            OppoMessage msg = null;
            lock (this.buffer)
            {
                if (this.buffer.First != null)
                {
                    msg = this.buffer.First.Value;
                    buffer.RemoveFirst();
                }
            }
            return msg;

        }


    }
}
