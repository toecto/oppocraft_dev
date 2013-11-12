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
        NetworkModule network;

        public MessageHandler(Game1 g, NetworkModule network)
        {
            this.theGame = g;
            this.network = network;
        }

        public void Tick()
        {
            OppoMessage msg;
            while((msg=this.network.getMessage())!=null)   
            {
                this.handle(msg);
            }
        }

        void handle(OppoMessage msg)
        {
            //Debug.WriteLine(msg.ToString());
            switch(msg.Type)
            {
                case OppoMessageType.GetClientID:
                    {
                        this.theGame.cid=msg["cid"];
                        break;
                    }
                case OppoMessageType.CreateUnit:
                    {
                        Unit unit = new Unit(msg["cid"], msg["uid"]);
                        unit.location = new WorldCoords(100, 100);
                        this.theGame.units.Add(unit);
                        break;
                    }
                case OppoMessageType.MoveUnit:
                    {
                        Unit u=this.theGame.units.getById(msg["uid"]);
                        if (u == null)
                        {
                            Debug.WriteLine("Message for unexisting unit");
                            break;
                        }
                        u.task.AddUnique(new CommandMovement(new WorldCoords(msg["x"],msg["y"])));
                        break;
                    }
            }
        }

    }
}
