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

    }
}
