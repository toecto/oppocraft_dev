using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using testClient;
using Microsoft.Xna.Framework;

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

        public Game1 theGame;
        public TaskCollection task;


        public Coordinates size = new Coordinates(1, 1);
        public WorldCoords location;
        public WorldCoords destination;
        public int id;
        public int type;
        
        public State state;
        public Direction direction;        

        public int currHP;
        public int maxHP;
        public float speed=2;
        public int damage;
        public int armour;
        public int attackSpeed;

        public Unit(Game1 g, int id)
        {
            this.theGame = g;
            this.id = id;
            task = new TaskCollection(this);
        }

        public virtual void SetGridValue()
        {
            GridCoords gridlocation = this.theGame.theGrid.getGridCoords(this.location);
            this.theGame.theGrid.fillRectValues(gridlocation, size, -1);
        }

        public virtual void Tick()
        {
            this.task.Tick();
        }

        public virtual void Render(RenderSystem render)
        {
            Vector2 position = this.theGame.render.getScreenCoords(this.location);
            position.X -= this.theGame.render.primRect.Width / 2;
            position.Y -= this.theGame.render.primRect.Height / 2;
                
            render.spriteBatch.Draw(this.theGame.render.primRect, position, new Rectangle(0, 0, 40, 24), new Color(255, 255, 255));

        }

        public virtual void AddCommand(OppoMessage msg)
        {
            msg["unitid"] = this.id;
            this.theGame.AddCommand(msg);
        }

    }
}
