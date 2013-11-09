using System.Collections.Generic;
using System.Diagnostics;


namespace OppoCraft
{
    public class UnitCollection : LinkedList<Unit>
    {
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

        public Unit getByID(int id)
        {
            foreach(Unit u in this)
            {
                if (u.id == id)
                    return u;
            }
            return null;
        }

        public void Add(Unit u)
        {
            this.AddLast(u);
        }

    }
}
