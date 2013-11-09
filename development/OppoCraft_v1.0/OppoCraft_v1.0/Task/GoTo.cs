using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class GoTo : Task
    {
        Game1 theGame;
        Unit unit;

        public GoTo(Game1 g, Unit u, WorldCoords dest)
        {
            this.theGame = g;
            this.unit = u;             
        }

        
        
        
    }
}
