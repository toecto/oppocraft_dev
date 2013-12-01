using System;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class Coordinates
    {
        public int X;
        public int Y;

        public Coordinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public double Distance(Coordinates c)
        {
            return Math.Sqrt(Math.Pow(c.X - this.X, 2) + Math.Pow(c.Y - this.Y, 2));
        }

        public Vector2 getVector2()
        {
            return new Vector2(this.X, this.Y);
        }

        public void setVector2(Vector2 v)
        {
            this.X = (int)(v.X);
            this.Y = (int)(v.Y);
        }

        public bool Equals(Coordinates test)
        {
            return test!=null && (this.X == test.X) && (this.Y == test.Y);
        }

    }
}
