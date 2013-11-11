using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    public class GoTo : Task    
    {       
        int currStep;
        int totalSteps;
        Vector2 destination;
        WorldPath worldPath;
        WorldCoords dest;

        public GoTo(WorldCoords d)
        {
            this.currStep = 0;
            this.dest = d;
        }
        
        public void GetPath()
        {
            this.worldPath = this.unit.theGame.theGrid.thePathFinder.GetPath(this.unit.location, this.dest);
            this.totalSteps = this.worldPath.Count();

            this.destination = this.worldPath.First.Value.getVector2();
            Debug.WriteLine("Path length "+this.totalSteps);
        }
        public override bool Tick()
        {
                if (this.currStep < this.totalSteps)
                {
                    Debug.WriteLine("currStep " + this.currStep + " " + this.totalSteps);
                    if (Vector2.Distance(this.unit.location.getVector2(), this.destination) < this.unit.speed)
                    {

                        this.destination = this.worldPath.ElementAt(this.currStep).getVector2();

                        OppoMessage msg = new OppoMessage(OppoMessageType.MoveUnit);
                        msg["x"] = (int)this.destination.X;
                        msg["y"] = (int)this.destination.Y;
                        this.unit.AddCommand(msg);

                        this.currStep++;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public override void onStart()
        {
            base.onStart();
            this.GetPath();
        }
    }
}
