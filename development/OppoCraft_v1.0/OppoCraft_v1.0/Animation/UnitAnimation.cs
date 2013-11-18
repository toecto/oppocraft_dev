using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    public class UnitAnimation
    {
        public SpriteSheet sprites;
        int frameCounter=0;
        public AnimationMap currentAnimation;//{ get { return currentAnimation; } set { frameCounter = 0; currentAnimation = value; } }
        
        Unit unit;

        public UnitAnimation(Unit unit, string name)
        {
            this.unit = unit;
            this.sprites = unit.theGame.graphContent.Load(name);
        }

        public void Render(RenderSystem render)
        {
            this.sprites.Render(render,unit.location, this.currentAnimation, frameCounter);
            this.frameCounter = this.currentAnimation.getNextFrame(this.frameCounter);
            
        }



    }
}
