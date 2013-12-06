using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class GameMap: EntityCollection
    {
        public Game1 theGame;
        public GameMap(Game1 g)
        {
            this.theGame = g;
        }

        public void Tick()
        {
            foreach (KeyValuePair<int, MapEntity> item in this)
            {
                item.Value.Tick();
            }
        }

        public void Render(RenderSystem render)
        {
            foreach (KeyValuePair<int, MapEntity> item in this.OrderBy(item => item.Value.location.Y))
            {
                item.Value.Render(render);
            }
        }

        public override void Add(MapEntity u)
        {
            if (u.uid == 0) u.uid = this.theGame.CreateUID();
            u.theGame = this.theGame;
            base.Add(u);
            u.onStart();
        }

        public List<MapEntity> EntitiesIn(WorldCoords start, WorldCoords stop)
        {
            List<MapEntity> result = new List<MapEntity>(8);
            foreach (KeyValuePair<int, MapEntity> item in this)
            {
                if (item.Value.location.isIn(start, stop))
                    result.Add(item.Value);
            }
            return result;
        }
    }


}
