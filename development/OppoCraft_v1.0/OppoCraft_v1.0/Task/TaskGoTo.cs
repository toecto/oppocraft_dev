using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    public class TaskGoTo : Task    
    {       
        int currStep;
        int totalSteps;
        Vector2 destination;
        WorldPath worldPath;
        WorldCoords dest;

        public TaskGoTo(WorldCoords d)
        {
            this.currStep = 1;
            this.totalSteps = 0;
            this.dest = d;
        }
        
        public void GetPath()
        {
            this.worldPath = this.unit.theGame.theGrid.thePathFinder.GetPath(this.unit.location, this.dest);
            if (this.worldPath == null)
                return;
            this.totalSteps = this.worldPath.Count();

            this.destination = this.worldPath.First.Value.getVector2();
        }

        public override bool Tick()
        {
            if (Vector2.Distance(this.unit.location.getVector2(), this.destination) < this.unit.speed || this.currStep==1)
            {
                if (this.currStep >= this.totalSteps)
                    return false;


                this.destination = this.worldPath.ElementAt(this.currStep).getVector2();

                OppoMessage msg = new OppoMessage(OppoMessageType.Movement);
                msg["x"] = (int)this.destination.X;
                msg["y"] = (int)this.destination.Y;
                this.unit.AddCommand(msg);

                this.currStep++;
            }
            return true;
        }

        public override void onStart()
        {
            base.onStart();
            this.GetPath();
        }

        public override void onFinish()
        {
            OppoMessage msg = new OppoMessage(OppoMessageType.ChangeState);
            msg["state"]=(int)Unit.State.Halt;
            this.unit.AddCommand(msg);
        }
    }
}
