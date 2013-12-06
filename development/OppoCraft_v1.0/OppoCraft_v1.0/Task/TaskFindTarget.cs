using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft 
{
    class TaskFindTarget : Task
    {
        string type;
        bool anySide;
        List<int> ignore;
        

        public TaskFindTarget(string type, bool anySide=false)
        {
            this.type = type;
            this.anySide = anySide;

        }

        public override void onStart()
        {
            if (!this.unit.task.checkShared("IgnoreUnits"))
                this.unit.task.setShared("IgnoreUnits", new List<int>(8));
            this.ignore = this.unit.task.getShared<List<int>>("IgnoreUnits");

        }

        public override bool Tick()
        {
            double minDistance = 0, checkDistance;
            Unit target=null, unit;
            int range = this.unit.theGame.theGrid.getWorldCoords(new GridCoords(this.unit.viewRange, 0)).X;
            WorldCoords start = new WorldCoords(this.unit.location.X - range / 2, this.unit.location.Y - range / 2);
            WorldCoords end = new WorldCoords(start.X + range, start.Y + range);

            foreach (MapEntity item in this.unit.theGame.map.Values)
            {
                if (item.GetType() != typeof(Unit)) continue;
                unit = (Unit)item;            
            
                if (unit.cid == this.unit.cid && !anySide) continue;
                if (this.ignore.Contains(unit.uid)) continue;
                if (unit.uid == this.unit.uid) continue;
                if (unit.type != this.type) continue;

                if (!unit.alive) continue;

                checkDistance = this.unit.location.Distance(unit.location);
                if (checkDistance < minDistance || minDistance == 0)
                {
                        minDistance = checkDistance;
                        target = unit;
                }
                    
            }

            if (target != null)
            {
                this.unit.task.setShared("targetUnit", target);
                return false;
            }

            return true;
        }

    }
}
