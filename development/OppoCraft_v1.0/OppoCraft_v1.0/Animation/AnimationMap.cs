using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace OppoCraft
{

    public class AnimationMap
    {
        public int frames;
        public int delay=5;
        public int width;
        public int height;
        public int startX;
        public int startY;
        public bool looped;

        public AnimationMap(int frames, int startX, int startY, int width, int height, bool looped,int delay=5)
        {
            this.frames = frames*delay;
            this.width = width;
            this.height = height;
            this.startX = startX;
            this.startY = startY;
            this.looped = looped;
            this.delay = delay;
        }

        public Rectangle getFrame(int number)
        {
            number /= delay;
            return new Rectangle(this.startX + number * this.width, this.startY, this.width, this.height);
        }

        internal int getNextFrame(int frame)
        {
            frame++;
            if (frame<this.frames)
                return frame;
            return 0;
        }
    }
}
