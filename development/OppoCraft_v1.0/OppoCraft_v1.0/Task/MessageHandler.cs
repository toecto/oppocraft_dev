using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    class MessageHandler
    {
        Game1 theGame;

        public MessageHandler(Game1 g)
        {
            this.theGame = g;
        }

        public void Tick()
        {
            OppoMessage msg;
            while((msg=this.theGame.network.getMessage())!=null)   
            {
                this.handle(msg);
            }
        }

        void handle(OppoMessage msg)
        {
            Debug.WriteLine("handle "+msg.ToString());
        }

    }
}
