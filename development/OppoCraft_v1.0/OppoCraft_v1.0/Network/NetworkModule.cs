using System;
using testClient;

namespace OppoCraft{
    public class NetworkModule
    {

        TcpMessageClient net;
        public OppoMessageCollection buffer= new OppoMessageCollection();

        public NetworkModule(string IP, int port = 8888)
        {
            this.net = new TcpMessageClient(IP, 8888);
            this.net.onMessage+=readMessage;
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
                    if (msg!=null)
                        this.buffer.AddLast(msg);
                }
                
            }
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
