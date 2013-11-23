using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class ActionAnimation
    {
        public List<SimpleAnimation> animations;
        public Unit unit;
        public int priority;
        protected SimpleAnimation currentAnimation;

        public ActionAnimation(List<SimpleAnimation> animations, Unit unit, int priority)
        {
            this.unit = unit;
            this.animations = animations;
            this.priority = priority;
            this.currentAnimation = animations[0];
        }

        virtual public void Tick()
        {
            this.currentAnimation.Tick();
        }

        virtual public void Render(RenderSystem render, Vector2 position)
        {
            this.currentAnimation.Render(render, position);
        }
    }
}
