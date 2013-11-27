using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft 
{
    class TaskFindTarget : Task
    {
        Unit.Type type;

        public TaskFindTarget(Unit.Type type)
        {
            this.type = type;    
        }

        public override bool Tick()
        {
            double minDistance = 0, checkDistance;
            Unit target=null;
            foreach (KeyValuePair<int, Unit> item in this.unit.theGame.map)
            {
                if (item.Value.cid == this.unit.cid) continue;
                if (item.Value.uid == this.unit.uid) continue;
                if (item.Value.type != this.type) continue;

                if (item.Value.currHP <= 0) continue;

                checkDistance=this.unit.location.Distance(item.Value.location);
                if (checkDistance < minDistance || minDistance == 0)
                {
                    minDistance = checkDistance;
                    target = item.Value;
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
