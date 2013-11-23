using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class UnitAnimation
    {
        private Dictionary<string, ActionAnimation> actions;
        public Unit unit;
        private ActionAnimation currentAction;

        public UnitAnimation(Unit unit)
        {
            this.unit = unit;
            this.actions = new Dictionary<string, ActionAnimation>();
        }

        public void Add(string name, ActionAnimation action)
        {
            this.actions.Add(name, action);
            if (this.currentAction == null)
                this.currentAction = action;
        }

        public void Tick()
        {
            if (this.currentAction != null)
                this.currentAction.Tick();
        }

        public void Render(RenderSystem render)
        {
            if (this.currentAction == null) return;
            
            Vector2 position = render.getScreenCoords(this.unit.location);
            currentAction.Render(render, position);
        }
    }
}
