using System;
using testClient;

namespace OppoCraft{
    class NetworkModule
    {

        TcpMessageClient net;
        OppoMessageCollection buffer= new OppoMessageCollection();

        public NetworkModule(string IP, int port = 8888)
        {
            this.net = new TcpMessageClient(IP, 8888);
        }

        void gotMessage(TcpMessageClient client)
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
    }
}
