using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class TaskKnightDriver : Task
    {

        public override bool Tick()
        {
            if (this.unit.state == Unit.State.Main)
            {
                this.unit.task.Add(new TaskFindTarget(Unit.Type.Knight));
                this.unit.state = Unit.State.Patrol;
            }

            if (this.unit.state != Unit.State.Fight && this.unit.task.checkShared("targetUnit"))
            {
                this.unit.state = Unit.State.Fight;
                this.unit.task.Add(new TaskFight(this.unit.task.getShared<Unit>("targetUnit")));
            }
            return true;
        }

        public override void onStart()
        {
            this.unit.task.Add(new TaskMortable());
        }

        public override void onFinish()
        {
        }
    }
}
