using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class Movement
    {
        Unit unit;
        double dX;
        double dY;
        double tempX;
        double tempY;
        WorldCoords destination;


        public Movement(Unit u, WorldCoords dest)
        {
            this.unit = u;
            this.destination = dest;
            this.Init();
        }

        public void Init()
        {
            double distance = this.unit.location.Distance(this.destination);
            this.dX = (this.destination.X - this.unit.location.X) / distance;
            this.dY = (this.unit.location.Y - this.destination.Y) / distance;
        }

        public bool Tick()
        {
            if (this.unit.location == this.destination)
            { return true; }
            else
            {
                this.MoveHandler();
                return false;
            }            
        }

        public void MoveHandler()
        {
            this.tempX += (this.dX * this.unit.speed);
            this.tempY += (this.dY * this.unit.speed);

            this.unit.location.X = (int)this.tempX;
            this.unit.location.Y = (int)this.tempY;
        }



    }
}
