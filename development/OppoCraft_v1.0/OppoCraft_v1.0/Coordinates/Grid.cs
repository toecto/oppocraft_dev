using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class Grid
    {
        Game1 theGame;

        public GridCoords gridSize;       
        public int[,] gridValues;

        public Grid(Game1 g)
        {
            this.theGame = g;
            this.gridSize = new GridCoords(this.theGame.worldMapSize.X / this.theGame.cellSize.X, this.theGame.worldMapSize.Y / this.theGame.cellSize.Y);
            this.gridValues = new int[this.gridSize.X, this.gridSize.Y];
            this.initGridValues();
        }

        //returns the int value for the coordiante on the grid
        public int getGridValue(GridCoords gc)
        {
            return this.gridValues[gc.X, gc.Y];
        }        //returns the int value for the coordiante on the grid
        public int getGridValue(WorldCoords wc)
        {
            return getGridValue(this.getGridCoords(wc));
        }

        //returns new WorldCoords based on Grid coordinates
        public WorldCoords getWorldCoords(GridCoords gc)
        {
            return new WorldCoords(gc.X * this.theGame.cellSize.X + this.theGame.cellSize.X / 2, gc.Y * this.theGame.cellSize.Y + this.theGame.cellSize.Y / 2);
        }

        //returns new Grid with coordinates based on WorldCoords parameter
        public GridCoords getGridCoords(WorldCoords worldCoords)
        {
            return new GridCoords((int)(worldCoords.X / this.theGame.cellSize.X), (int)(worldCoords.Y / this.theGame.cellSize.Y));
        }

        //returns new Grid with coordinates based on WorldCoords parameter
        public void fillRectValues(WorldCoords p, WorldCoords s, int v)
        {
            this.fillRectValues(this.getGridCoords(p), this.getGridCoords(s), v);
        }
        
        public void fillRectValues(GridCoords p, GridCoords s, int v)
        {
            for (int x = p.X; x < s.X + p.X; x++)
            {
                for (int y = p.Y; y < s.Y + p.Y; y++)
                {
                    this.gridValues[x, y] = v;
                }
            }
        }

        //sets the Grid cell values all to zero
        public void initGridValues()
        {
            this.fillRectValues(new GridCoords(0, 0), this.gridSize, -1);
            this.fillRectValues(new GridCoords(1, 1), new GridCoords(this.gridSize.X - 2, this.gridSize.Y - 2), 0);
        }
        public void resetGridValues()
        {
            for (int x = 0; x < this.gridSize.X; x++)
            {
                for (int y = 0; y < this.gridSize.Y; y++)
                {
                    if (this.gridValues[x, y] > 0)
                        this.gridValues[x, y] = 0;
                }
            }
        }
        

    }
}
