using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using testClient;

namespace OppoCraft
{
    public class Unit
    {
        public enum State
        {
            Halt,
            TakeDamage,
            Dying,
            Walking,
            Running,
            Attacking
        }
        public enum Direction
        {
            North,
            South,
            East,
            West,
            North_East,
            North_West,
            South_East,
            South_West
        }

        protected Game1 theGame;
        Coordinates size;
        public WorldCoords location;
        public WorldCoords destination;
        int id;
        int type;
        
        public State state;
        public Direction direction;
        public WorldPath worldPath;
        public double dX, dY;
        public double tempX, tempY;
        Movement movement;
        

        int currHP;
        int maxHP;
        public double speed;
        int damage;
        int armour;
        int attackSpeed;

        public Unit(Game1 g, int id)
        {
            this.theGame = g;
            this.id = id;
            this.size = new Coordinates(1, 1);
        }

        public void SetPath(WorldCoords orig, WorldCoords dest)
        {
           this.worldPath = this.theGame.theGrid.thePathFinder.GetPath(orig, dest);
        }

        public WorldCoords GetNextStep()
        {
            WorldCoords nextStep = this.worldPath.ElementAt(1);  //The second World Coord in the collection

            return nextStep;
        }
        
        public virtual void SetGridValue()
        {
            GridCoords gridlocation = this.theGame.theGrid.getGridCoords(this.location);
            this.theGame.theGrid.fillRectValues(gridlocation, size, -1);
        }

        public virtual void Tick()
        {
            if (this.movement != null)
            {
                if (this.movement.Move())
                {
                    this.movement = null;
                }
                else
                {
                    this.movement.Tick();
                }
            }

        }

        public virtual void Render(RenderSystem spriteBatch)
        {
            Debug.WriteLine("Unit Render");

        }

        public virtual void AddCommand(OppoMessage msg)
        {
            msg["UnitID"] = this.id;
            this.theGame.AddCommand(msg);
        }

    }
}
