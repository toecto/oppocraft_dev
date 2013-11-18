using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class SpriteSheetMap
    {
        public AnimationMap[][] animations;

        public SpriteSheetMap()
        {
            this.animations = new AnimationMap[50][];
        }

        public void addStandardDirections(Unit.State state,int startX, int startY, int width, int height, int frames=1, bool looped=true,int delay=5)
        {
            this.animations[(int)state] = new AnimationMap[8];
            this.animations[(int)state][(int)Unit.Direction.North] = new AnimationMap(frames, startX, startY, width, height, looped, delay);
            this.animations[(int)state][(int)Unit.Direction.South] = new AnimationMap(frames, startX + width * frames, startY, width, height, looped, delay);

            this.animations[(int)state][(int)Unit.Direction.East] = new AnimationMap(frames, startX, startY + height, width, height, looped, delay);
            this.animations[(int)state][(int)Unit.Direction.West] = new AnimationMap(frames, startX + width * frames, startY + height, width, height, looped, delay);

            this.animations[(int)state][(int)Unit.Direction.North_East] = new AnimationMap(frames, startX, startY + height * 2, width, height, looped, delay);
            this.animations[(int)state][(int)Unit.Direction.South_West] = new AnimationMap(frames, startX + width * frames, startY + height * 2, width, height, looped, delay);

            this.animations[(int)state][(int)Unit.Direction.North_West] = new AnimationMap(frames, startX, startY + height * 3, width, height, looped, delay);
            this.animations[(int)state][(int)Unit.Direction.South_East] = new AnimationMap(frames, startX + width * frames, startY + height * 3, width, height, looped, delay);
        }

        public void addStandard(Unit.State state, int startX, int startY, int width, int height, int frames = 1, bool looped = true, int delay=5)
        {
            this.animations[(int)state] = new AnimationMap[1];
            this.animations[(int)state][(int)Unit.Direction.North] = new AnimationMap(frames, startX, startY, width, height, looped, delay);
        }

    }
}
