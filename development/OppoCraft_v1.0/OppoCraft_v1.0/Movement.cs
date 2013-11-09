using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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
            this.tempX = this.unit.location.X;
            this.tempY = this.unit.location.Y;
            double distance = this.unit.location.Distance(this.destination);
            this.dX = (this.destination.X - this.unit.location.X) / distance;
            this.dY = (this.destination.Y - this.unit.location.Y) / distance;
        }

        public bool Tick()
        {
            if (this.unit.location.Distance(this.destination) <= this.unit.speed)
            {
                this.unit.location = this.destination;
                return false; 
            }
            else
            {
                this.MoveHandler();
                return true;
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
