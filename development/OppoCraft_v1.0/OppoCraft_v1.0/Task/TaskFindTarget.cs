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
            foreach (KeyValuePair<int, Unit> item in this.unit.theGame.map)
            {
                if (item.Value.uid == this.unit.uid) continue;
                if (item.Value.type == this.type)
                {
                    this.unit.task.setShared("targetUnit",item.Value);
                    return false;
                }
            }

            return true;
        }

    }
}
