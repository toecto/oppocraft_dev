using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
   public class PathFinder
    {
        Grid theGrid;

        //set of coordinates with the value to apply during path finding algorithm
        public int[,] pathValues = new int[,] 
        {
            {0, -1, 2}, {1, 0, 2}, {0, 1, 2}, {-1, 0, 2},
            {-1, -1, 3}, {1,- 1, 3}, {1, 1, 3}, {-1, 1, 3},

            
        };


        public PathFinder(Grid g)
        {
            this.theGrid = g;
        }

        // orig == Origin coordiantes, dest == Destination coordinates
        public WorldPath GetPath(WorldCoords orig, WorldCoords dest, bool soft=true)
        {
            this.theGrid.resetGridValues();
            GridCoords origGrid = this.theGrid.getGridCoords(orig);
            GridCoords destGrid = this.theGrid.getGridCoords(dest);

            //if (this.theGrid.gridValues[origGrid.X, origGrid.Y]>=0)
            this.theGrid.gridValues[origGrid.X, origGrid.Y]=1; //set orginal grid cell value to 1
               // return null;

            int targetValue = this.theGrid.getGridValue(destGrid);
            GridPath toRestore = null;
            if (soft == true)
            {
                toRestore = this.ajustDestination(origGrid, destGrid);
            }
            WorldPath result = null;

            if (this.SetValues(origGrid, destGrid))
                result = this.SetPath(origGrid, destGrid);

            if (toRestore != null)
            {
                foreach (GridCoords item in toRestore)
                {
                    this.theGrid.gridValues[item.X, item.Y] = targetValue;
                }
            }

            return result;
        }

        private GridPath ajustDestination(GridCoords orig, GridCoords dest)
        {
            int targetValue = this.theGrid.getGridValue(dest);
            if (targetValue >-2) return null;
            

            GridPath cellQue = new GridPath();
            cellQue.AddFirst(dest);
            LinkedListNode<GridCoords> currentNode = cellQue.First;
            this.theGrid.gridValues[dest.X, dest.Y] = 0;
            GridCoords current;
            int pathValLength = this.pathValues.GetLength(0);
            int cnt = 0;
            while (currentNode != null)
            {
                cnt++;
                if (cnt > 100)
                {
                    break;
                }
                current = currentNode.Value;
                for (int i = 0; i < pathValLength; i++)
                {
                    int x = current.X + this.pathValues[i, 0];
                    int y = current.Y + this.pathValues[i, 1];

                    if (this.theGrid.gridValues[x, y] == targetValue)
                    {
                        cellQue.AddLast(new GridCoords(x, y));
                        this.theGrid.gridValues[x, y] = 0;
                    }
                }
                currentNode = currentNode.Next;
            }
            return cellQue;
        }


        //takes a Path (collection of Grid Coords) and applies the values from pathValues to the surrounding cells 
        //for each Grid Coord, using the pathValues array
        public bool SetValues(GridCoords orig, GridCoords dest)
        {
            GridPath cellQue = new GridPath(), newCellQue;
            cellQue.AddFirst(orig);
            int pathValLength = this.pathValues.GetLength(0);
            int cnt = 0;
            while (cellQue.Count > 0)
            {
                newCellQue=new GridPath();
                foreach (GridCoords gridCoords in cellQue)
                {
                    cnt++;
                    if (cnt > 115*115)
                    {
                        break;
                    }
                    int currentValue = this.theGrid.gridValues[gridCoords.X, gridCoords.Y];
                    for (int i = 0; i < pathValLength; i++)
                    {
                        int x = gridCoords.X + this.pathValues[i, 0];
                        int y = gridCoords.Y + this.pathValues[i, 1];
                        int newValue = currentValue + this.pathValues[i, 2];

                        if (this.theGrid.gridValues[x, y] == 0 || this.theGrid.gridValues[x, y] > newValue)
                        {
                            this.theGrid.gridValues[x, y] = newValue;
                            newCellQue.AddLast(new GridCoords(x, y));

                            if (x == dest.X && y == dest.Y)
                            {
                                return true;
                            }
                        }
                    }
                }
                cellQue = newCellQue;
            }
            return false;
        }

        // orig == Origin coordiantes, dest == Destination coordinates
        //Using the Grid values populated in SetValues method, determine the lowest value of each surrounding cell, and choose that
        //cell (add to Path collection), use that chosen cell to follow the same routine until getting back to the origin coord.
        public WorldPath SetPath(GridCoords orig, GridCoords dest)
        {
            WorldPath path = new WorldPath();
            GridCoords current = dest;
            int cnt=0;
            //Debug.WriteLine("Start!");
            while (current != null)
            {
                cnt++;
                if (cnt > 1000)
                {
                    Debug.WriteLine("Achtung!");
                }
                path.AddFirst(this.theGrid.getWorldCoords(current));
                current = this.makeNextStep(current, orig);
                
            }
            path.RemoveFirst();
            return path;
        }

        private GridCoords makeNextStep(GridCoords current, GridCoords orig)
        {

            GridCoords minCoord = null;
            int minValue = this.theGrid.getGridValue(current);
            bool isCornerAround = false;
            int pathValLength = this.pathValues.GetLength(0);

            for (int i = 0; i < pathValLength; i++)
            {
                if (isCornerAround && this.pathValues[i, 2] == 3)
                    break;
                GridCoords nextCoord = new GridCoords(current.X + this.pathValues[i, 0], current.Y + this.pathValues[i, 1]);

                int nextVal = this.theGrid.getGridValue(nextCoord);
                if (nextVal < 0 && this.pathValues[i, 2] == 2)
                {
                    isCornerAround = true;
                }
                else
                {
                    if (nextVal < minValue && nextVal > 0)
                    {
                        minCoord = nextCoord;
                        minValue = nextVal;
                    }
                }
            }
            //if (minCoord!=null)
            //Debug.WriteLine("minCoord! " + minCoord.X + " " + minCoord.Y + " " + this.theGrid.getGridValue(minCoord));

            return minCoord;
        }

    }
}
