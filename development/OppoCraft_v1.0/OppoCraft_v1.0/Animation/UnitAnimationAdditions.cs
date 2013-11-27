using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    class UnitAnimationAdditions
    {
        public static void Render(Unit unit, RenderSystem render)
        {
            Vector2 position = render.getScreenCoords(unit.location);
            //position = Vector2.Subtract(position, Vector2.Divide(new Vector2(render.primRect.Bounds.Width,render.primRect.Bounds.Height), 2f));

            position.X -= render.primRect.Bounds.Width / 2;

            position.Y -= unit.animation.current.First.Value.currentAnimation.file.height/2 + 5;
            Rectangle bar = new Rectangle(0, 0, render.primRect.Bounds.Width, 2);

            render.Draw(render.primDot, position, bar, Microsoft.Xna.Framework.Color.Red);

            bar = new Rectangle(0, 0, render.primRect.Bounds.Width * unit.currHP/unit.maxHP, 2);
            render.Draw(render.primDot, position, bar, Microsoft.Xna.Framework.Color.GreenYellow);


            string text = unit.currHP + "/" + unit.maxHP + "\n";
            //text += unit.direction.ToString()+"\n";
            foreach (KeyValuePair<Type,Task> task in unit.task.getTasks())
            {
                //text += task.Value.GetType().ToString() + "\n";
                
            }
            
            render.DrawText(text, position);
        }



    }
}
