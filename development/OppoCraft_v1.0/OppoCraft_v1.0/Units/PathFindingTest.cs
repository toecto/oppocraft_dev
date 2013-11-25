﻿using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using testClient;
using System.Collections.Generic;

namespace OppoCraft
{
    class PathFinderTest: Unit
    {
        WorldPath aPath=null;
        GridCoords lastSpot = new GridCoords(0,0);

        public PathFinderTest(int pid,int id)
            : base(pid, id)
        {
            this.location = new WorldCoords(100, 100);
            this.type = Unit.Type.System;
        }

        public override void Tick()
        {
            MouseState mouseState = Mouse.GetState();

            WorldCoords x=this.theGame.render.getWorldCoords(new Vector2(mouseState.X, mouseState.Y));
            GridCoords test = this.theGame.theGrid.getGridCoords(x);
            if (!test.Equals(this.lastSpot))
            {
                this.lastSpot = test;

                foreach(KeyValuePair<int,Unit> item in this.theGame.map)
                {
                    
                    Unit u = item.Value;
                    if (u.cid != this.theGame.cid) continue;
                    //Unit u = this.theGame.map.getById(this.theGame.myFirstUnit);
                    WorldCoords origCoord = u.location;
                    WorldCoords destination = this.theGame.theGrid.getWorldCoords(test);

                    this.aPath = this.theGame.theGrid.thePathFinder.GetPath(origCoord, destination);
                    //u.task.Add(new TaskGoTo(destination));
                }
            }
            //Debug.WriteLine(mouseState.X + ", " + mouseState.Y);
        }

        public override void Render(RenderSystem render)
        {
            int maxValue = 1;
            for (int x = 0; x < this.theGame.theGrid.gridValues.GetLength(0); x++)
            {
                for (int y = 0; y < this.theGame.theGrid.gridValues.GetLength(1); y++)
                {
                    if (maxValue < this.theGame.theGrid.getGridValue(new GridCoords(x, y)))
                    {
                        maxValue = this.theGame.theGrid.getGridValue(new GridCoords(x, y));
                    }
                }
            }

            int color = 1;

            for (int x = 0; x < this.theGame.theGrid.gridValues.GetLength(0); x++)
            {
                for (int y = 0; y < this.theGame.theGrid.gridValues.GetLength(1); y++)
                {
                    Vector2 position = this.theGame.render.getScreenCoords(this.theGame.theGrid.getWorldCoords(new GridCoords(x, y)));
                    position.X-=this.theGame.render.primRect.Width/2;
                    position.Y -= this.theGame.render.primRect.Height/ 2; 
                    color = this.theGame.theGrid.getGridValue(new GridCoords(x, y)) * 255 / maxValue;
                    if (color < 0)
                        render.spriteBatch.Draw(this.theGame.render.primRect, position, new Rectangle(0, 0, 40, 24), new Color(255, 0, 0));
                    else
                        render.spriteBatch.Draw(this.theGame.render.primRect, position, new Rectangle(0, 0, 40, 24), new Color(0, 0, color));
                }
            }
            if (this.aPath != null)
            {
                foreach (WorldCoords coords in this.aPath)
                {
                    Vector2 position = this.theGame.render.getScreenCoords(coords);
                    position.X -= this.theGame.render.primRect.Width / 2;
                    position.Y -= this.theGame.render.primRect.Height / 2; 
                    render.spriteBatch.Draw(this.theGame.render.primRect, position, new Rectangle(0, 0, 40, 24), new Color(0, 255, 0));
                }
            }
            else
            {
                //Debug.WriteLine("No path");
            }
        }
    }
}
