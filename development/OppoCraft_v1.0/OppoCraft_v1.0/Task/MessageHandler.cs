using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    public class MessageHandler
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
            Debug.WriteLine(msg.ToString());
            if (msg.Type == OppoMessageType.MoveUnit)
            { 
                Unit u=this.theGame.units.getByID(msg["unitid"]);
                u.movement=new Movement(u, new WorldCoords(msg["x"],msg["y"]));
            
            }
        }

    }
}
