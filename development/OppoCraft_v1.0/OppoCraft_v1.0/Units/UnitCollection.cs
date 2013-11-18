using System.Collections.Generic;
using System.Diagnostics;


namespace OppoCraft
{
    public class UnitCollection : Dictionary<int, Unit>
    {
        public Unit getById(int id)
        {
            if(this.ContainsKey(id))
                return this[id];
            return null;
        }

        new public void Remove(int uid)
        {
            Unit u = this.getById(uid);
            if (u != null)
                this.Remove(u);
        }

        public void Remove(Unit u)
        {
            this.Remove(u.uid);
        }

        public virtual void Add(Unit u)
        {
            this.Add(u.uid,u);
            u.onStart();
        }

    }
}
