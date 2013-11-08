using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using System.Net.Sockets;

namespace TestServer
{
    class OppoServerClientHandler
    {
        public TcpMessageClient Net;
        public int ID;
        private OppoServer server;

        public OppoServerClientHandler(OppoServer server, TcpClient inClientSocket, int ID)
        {
            this.Net = new TcpMessageClient(inClientSocket);
            this.Net.onMessage += this.gotMessage;
            this.ID = ID;
            this.server = server;
        }

        public void Stop()
        {
            this.Net.Stop();
        }

        private void gotMessage(TcpMessageClient client)
        {
            if (client.Available > 0)
            {
                byte[] buffer = client.getMessage();
                OppoMessage message = OppoMessage.fromBin(buffer);
                Console.WriteLine("From client " + ID + ": " + buffer.Length + " Bytes");
                Console.WriteLine(message.ToString());
                this.server.broadcast(buffer, this.ID);
            }
        }

    } //end class handleClinet
}
