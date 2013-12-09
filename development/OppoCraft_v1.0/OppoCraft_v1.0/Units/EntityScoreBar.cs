using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    class EntityScoreBar : MapEntity
    {

        Vector2 position;

        Dictionary<string, string> data;
        int cooldown=0;
        int cooldownTotal=30;


        public override void onStart()
        {
            position=new Vector2(20,5);
            data = new Dictionary<string, string>();
            Tick();
        }

        public override void Tick()
        {
            cooldown--;
            if (cooldown > 0) return;
            cooldown = cooldownTotal;

            this.data["UnitsOnMap"]=this.theGame.map.units.Count.ToString();
            this.data["EntitiesOnMap"] = this.theGame.map.entities.Count.ToString();

            int cntMy=0,cntEn=0,cntNt=0;

            foreach(Unit unit in this.theGame.map.units)
            {
                if (unit.GetType() != typeof(Unit)) continue;
                if (unit.isMy)
                    cntMy++;
                else 
                {
                    if (unit.cid == 0)
                        cntNt++;
                    else
                        cntEn++;
                }
            }
            this.data["UnitsMy"] = cntMy.ToString();
            this.data["UnitsEn"] = cntEn.ToString();
            this.data["UnitsNt"] = cntNt.ToString();
        }


        public override void Render(RenderSystem render)
        {
            int sizeX = this.theGame.render.size.X;
            //render.Draw(render.primRect50, new Rectangle(0, 0, sizeX, sizeY), new Rectangle(0, 0, 40, 24), Color.Gray);
            render.DrawText("Units on map:" 
                + " My/" + this.data["UnitsMy"]
                + " Enemy/" + this.data["UnitsEn"]
                + " Neutral/" + this.data["UnitsNt"]
                + " Total/" + this.data["UnitsOnMap"]
                , position);
        }
    }
}
