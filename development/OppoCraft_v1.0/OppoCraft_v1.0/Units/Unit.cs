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
            Attacking,
            Fight,
            Patrol,
            Main
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

        public enum Type
        {
            Knight,
            System
        }


        public Game1 theGame;
        public TaskManager task;
        public UnitAnimation animation;

        public Coordinates size = new Coordinates(1, 1);
        public WorldCoords location = new WorldCoords(1 , 1);
        public int uid;
        public int cid = 0;
        public Unit.Type type;
        
        public State state;
        public Direction direction;
        
        public int currHP = 100;
        public int maxHP = 100;
        public float speed = 1f;
        public int damage = 5;
        public int armour = 1;
        public int attackSpeed = 30;
        public int attackRange = 50;

        public Unit(int cid,int uid)
        {
            this.state = State.Main;
            this.direction = Direction.East;
            this.cid = cid;
            this.uid = uid;
            this.task = new TaskManager(this);
        }

        public void onStart()
        {
            this.animation = this.theGame.graphContent.LoadUnitAnimation(this, "BlueKnight");
            if (this.cid == this.theGame.cid)
                this.task.addDriver();
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
            Vector2 position =  this.theGame.render.getScreenCoords(this.location);
            position.X -=       this.theGame.render.primRect.Width / 2;
            position.Y -=       this.theGame.render.primRect.Height / 2;
                
            render.spriteBatch.Draw(this.theGame.render.primRect, position, new Rectangle(0, 0, 40, 24), new Color(255, 255, 255));
            /**/
            this.animation.Render(render);
            if(this.type != Unit.Type.System)
            {
                UnitAnimationAdditions.Render(this,render);
            }
        }

        public virtual void AddCommand(OppoMessage msg)
        {
            msg["uid"] = this.uid;
            this.theGame.AddCommand(msg);
        }




        
    }
}
