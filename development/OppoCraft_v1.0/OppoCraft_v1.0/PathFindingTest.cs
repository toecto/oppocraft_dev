using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace OppoCraft
{
    class PathFinderTest: Unit
    {
        WorldPath aPath=null;

        public PathFinderTest(Game1 g,int id)
            : base(g, id)
        { 
        
        }

        public override void Tick()
        {
            MouseState mouseState = Mouse.GetState();
            WorldCoords origCoord = new WorldCoords(60, 60);
            WorldCoords destCoord = this.theGame.render.getWorldCoords(new Vector2(mouseState.X, mouseState.Y));
            //Debug.WriteLine(mouseState.X + ", " + mouseState.Y);
            this.aPath = this.theGame.theGrid.thePathFinder.GetPath(origCoord, destCoord);
  
        }

        public override void Render(RenderSystem render)
        {
            int maxValue = 0;
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

            int color;

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
