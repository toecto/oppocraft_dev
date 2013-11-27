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
            switch(msg.Type)
            {
                case OppoMessageType.StartGame:
                    {
                        this.theGame.running=true;
                        break;
                    }
                case OppoMessageType.GetClientID:
                    {
                        this.theGame.cid = msg["cid"];
                        break;
                    }
                case OppoMessageType.CreateUnit:
                    {
                        Unit unit = new Unit(msg["ownercid"], msg["uid"]);
                        unit.location = new WorldCoords(msg["x"], msg["y"]);
                        this.theGame.map.Add(unit);
                        break;
                    }
                case OppoMessageType.Movement:
                    {
                        Unit u = this.theGame.map.getById(msg["uid"]);
                        if (u == null)
                            Debug.WriteLine("Message for unexisting unit");
                        else
                            u.task.Add(new CommandMovement(msg));
                        break;
                    }
                case OppoMessageType.ChangeState:
                    {
                        //Debug.WriteLine("ChangeState "+msg.ToString());
                        Unit u = this.theGame.map.getById(msg["uid"]);
                        if (u != null)
                            u.task.Add(new CommandChangeUnitState(msg));
                        break;
                    }

                case OppoMessageType.Stop:
                    {
                        Unit u = this.theGame.map.getById(msg["uid"]);
                        if (u != null)
                        {
                            u.task.Remove(typeof(TaskGoTo));
                            u.task.Remove(typeof(CommandMovement));
                            u.location = new WorldCoords(msg["x"], msg["y"]);
                        }
                        break;
                    }

            }
        }

    }
}
