using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OppoCraft
{
    public class SmoothScroller
    {
        public Coordinates scroll;
        Coordinates limit;
        public Vector2 myScroll;
        Vector2 delta;
        UserInputSystem input;

        public SmoothScroller(UserInputSystem input, Coordinates scroll, Coordinates totalSize, Coordinates frameSize)
        {
            this.input = input;
            this.scroll = scroll;
            this.limit = totalSize;
            this.limit.X -= frameSize.X;
            this.limit.Y -= frameSize.Y;
            this.myScroll = scroll.getVector2();
            delta = new Vector2(0, 0);
        }

        public void HandleInput()
        {
            this.delta.X += this.input.arrowX;
            if (this.delta.X > 10) this.delta.X = 10;

            this.delta.Y += this.input.arrowY;
            if (this.delta.Y > 10) this.delta.Y = 10;

            if (this.input.mouse.RightButton == ButtonState.Pressed)
                this.delta = this.input.mouseCoordinatesDelta;
        }

        public void Tick()
        {
            this.HandleInput();
            this.myScroll = Vector2.Add(this.myScroll, this.delta);
            if (this.myScroll.X < 0) this.myScroll.X = 0;
            if (this.myScroll.Y < 0) this.myScroll.Y = 0;
            if (this.myScroll.X > this.limit.X) this.myScroll.X = this.limit.X;
            if (this.myScroll.Y > this.limit.Y) this.myScroll.Y = this.limit.Y;
            this.scroll.setVector2(myScroll);
            this.Slow();
        }

        public void Slow()
        {
            this.delta.X += 0.5f * -Math.Sign(this.delta.X);
            this.delta.Y += 0.5f * -Math.Sign(this.delta.Y);
        }


    }
}
