using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class Movement
    {
        Unit unit;


        public Movement(Unit u)
        {
            this.unit = u;
        }

        public bool Move()
        {
            if (this.unit.location == this.unit.destination)
            { return true; }
            else
            {
                double distance = this.unit.location.Distance(this.unit.destination);
                this.unit.dX = (this.unit.destination.X - this.unit.location.X) / distance;
                this.unit.dY = (this.unit.location.Y - this.unit.destination.Y) / distance;
                return false;
            }
        }

        public void Tick()
        {
            this.MoveHandler();
        }

        public void MoveHandler()
        {
            this.unit.tempX += (this.unit.dX * this.unit.speed);
            this.unit.tempY += (this.unit.dY * this.unit.speed);

            this.unit.location.X = (int)this.unit.tempX;
            this.unit.location.Y = (int)this.unit.tempY;
        }



    }
}
