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
            East,
            North_East,
            North,
            North_West,
            West,
            South_West,
            South,
            South_East
        }

        public Game1 theGame;
        public TaskManager task;
        public UnitAnimation animation;

        public Coordinates size = new Coordinates(1, 1);
        public WorldCoords location=new WorldCoords(1 , 1);
        public int uid;
        public int cid = 0;
        public int type;
        
        public State state;
        public Direction direction;
        public WorldPath worldPath;
        
        public int currHP;
        public int maxHP;
        public float speed=1;
        public int damage;
        public int armour;
        public int attackSpeed;

        public Unit(int cid,int uid)
        {
            this.state = State.Walking;
            this.direction = Direction.East;
            this.cid = cid;
            this.uid = uid;
            this.task = new TaskManager(this);
        }

        public void onStart()
        {
            this.animation = this.theGame.graphContent.LoadUnitAnimation(this, "BlueKnight");
        }


        public virtual void SetGridValue()
        {
            GridCoords gridlocation = this.theGame.theGrid.getGridCoords(this.location);
            this.theGame.theGrid.fillRectValues(gridlocation, size, -1);
        }

        public virtual void Tick()
        {
            this.task.Tick();
            this.animation.Tick();
        }

        public virtual void Render(RenderSystem render)
        {
            /*
            Vector2 position = this.theGame.render.getScreenCoords(this.location);
            position.X -= this.theGame.render.primRect.Width / 2;
            position.Y -= this.theGame.render.primRect.Height / 2;
                
            render.spriteBatch.Draw(this.theGame.render.primRect, position, new Rectangle(0, 0, 40, 24), new Color(255, 255, 255));
            /**/
            this.animation.Render(render);
        }

        public virtual void AddCommand(OppoMessage msg)
        {
            msg["uid"] = this.uid;
            this.theGame.AddCommand(msg);
        }



    }
}
