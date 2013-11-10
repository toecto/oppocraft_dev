using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class GoTo : Task
    {       
        int currStep;
        int totalSteps;
        Vector2 destination;

        public GoTo(WorldCoords dest)
        {
            this.currStep = 1;
            this.GetPath(dest);
            this.totalSteps = this.unit.worldPath.Count();
            this.destination = this.unit.worldPath.ElementAt(this.currStep).getVector2();
        }
        
        public void GetPath(WorldCoords dest)
        {
            this.unit.worldPath = this.unit.theGame.theGrid.thePathFinder.GetPath(this.unit.location, dest);
        }

        public override bool Tick()
        {
            if (this.currStep < this.totalSteps)
            {
                if (Vector2.Distance(this.unit.location.getVector2(), this.destination) < this.unit.speed)
                {
                    this.currStep++;
                    this.destination = this.unit.worldPath.ElementAt(this.currStep).getVector2();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
