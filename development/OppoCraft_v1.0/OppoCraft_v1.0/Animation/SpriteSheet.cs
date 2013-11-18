using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using OppoCraft;
using System.Diagnostics;

namespace OppoCraft
{
    public class SpriteSheet
    {
        Texture2D texture;
        public SpriteSheetMap map;
        RenderSystem renders;
        Vector2 origin = new Vector2(0, 0);

        public SpriteSheet(RenderSystem render, Texture2D texture, SpriteSheetMap map)
        {
            this.renders = render;
            this.texture = texture;
            this.map = map; 
        }

        public void Render(RenderSystem render,WorldCoords location,AnimationMap map, int frame)
        {

            Vector2 position = render.getScreenCoords(location);
            position.X -= map.width / 2;
            position.Y -= map.width-26;

            render.spriteBatch.Draw(this.texture, position, map.getFrame(frame), Microsoft.Xna.Framework.Color.White);
        }

    }
}
