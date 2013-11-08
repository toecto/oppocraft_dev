using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class NetworkModule
    {
        Game1 theGame;
        TcpMessageClient net;
        OppoMessageBuffer buffer;
        public NetworkModule(Game1 game)
        {
            this.buffer = new OppoMessageBuffer();
            this.theGame = game;
            this.net=new TcpMessageClient("127.0.0.1",8888);
            this.net.onMessage += this.gotMessage;
            this.net.Connect();
        }

        private void gotMessage(TcpMessageClient client)
        {
            if (client.Available > 0)
            {
                Byte[] RawMessage;
                while ((RawMessage = client.getMessage()) != null)
                {
                    OppoMessage message = OppoMessage.fromBin(RawMessage);

                }
            }
        }



    }
}
