using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace OppoCraft
{
    public class GraphContentManager
    {
        Game1 theGame;

        public GraphContentManager(Game1 game)
        {
            this.theGame = game;
        }

        public SpriteSheet Load(string name)
        {
            SpriteSheetMap map = new SpriteSheetMap();

            map.addStandardDirections(Unit.State.Halt, 0, 0, 96, 96, 9, true, 20);
            map.addStandardDirections(Unit.State.TakeDamage, 0, 96 * 4, 96, 96, 9);
            map.addStandardDirections(Unit.State.Dying, 0, 96 * 4*2, 96, 96, 9);
            map.addStandardDirections(Unit.State.Walking, 0, 96 * 4*3, 96, 96, 8);
            return new SpriteSheet(this.theGame.render, this.theGame.render.testKnight, map);
        }

    }
}
