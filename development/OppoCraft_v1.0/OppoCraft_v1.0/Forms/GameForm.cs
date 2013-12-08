using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class GameForm : GameFormControl
    {

        public GameForm()
        {
            this.parentForm = this;
        }

        public override void onStart()
        {
            this.location = new WorldCoords(400, 100);
            this.size = new WorldCoords(400, 400);
            this.theGame.unitSelector.enabled = false;
        }

        public override void Tick()
        {
            WorldCoords mouse = new WorldCoords(0, 0);
            mouse.setVector2(theGame.userInput.mousePosition);
            
            if (this.theGame.userInput.mouseClicked)
            {
                this.onClickEvent(mouse);
            }
            base.Tick();
        }

        public override void Render(RenderSystem render)
        {
            render.Draw(render.primRect70, this.location.getRectangle(this.size), render.primRect70.Bounds, Color.Black);
            base.Render(render);
        }


        public override void onFinish()
        {
            this.theGame.unitSelector.enabled = true;
        }

    }
}
