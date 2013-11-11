using System.Collections.Generic;
using System.Diagnostics;


namespace OppoCraft
{
    public class UnitCollection : LinkedList<Unit>
    {
        Game1 theGame;
        Dictionary<int, Unit> ById = new Dictionary<int, Unit>();

        public UnitCollection(Game1 g)
        {
            this.theGame = g;
        }

        public void Tick()
        {
            foreach(Unit unit in this)
            {
                unit.Tick();
            }
        }

        public void Render(RenderSystem render)
        {
            foreach (Unit unit in this)
            {
                unit.Render(render);
            }
        }

        public Unit getById(int id)
        {
            if(this.ById.ContainsKey(id))
            return this.ById[id];
            return null;
        }

        public void Remove(int id)
        {
            Unit u = this.getById(id);
            if (u != null)
                this.Remove(u);
        }

        new public void Remove(Unit u)
        {
            this.ById.Remove(u.id);
            base.Remove(u);
        }

        public void Add(Unit u)
        {
            u.theGame = this.theGame;
            this.ById.Add(u.id,u);
            this.AddLast(u);
        }

    }
}
