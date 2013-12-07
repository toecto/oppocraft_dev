﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class TaskLamberjackDriver: Task
    {
        public override bool Tick()
        {
            if (!this.unit.task.isRunning(typeof(TaskFindTarget)) && !this.unit.task.isRunning(typeof(TaskFight)))
            {
                this.unit.task.Add(new TaskFindTarget(new List<string>(4) { "Knight", "Archer" }));
            }

            if (!this.unit.task.isRunning(typeof(TaskFight)) && this.unit.task.checkShared("targetUnit"))
            {
                this.unit.task.Add(new TaskFight(this.unit.task.removeShared<Unit>("targetUnit")));
            }
            return true;
        }

        public override void onStart()
        {
            this.unit.task.Add(new TaskMortality());
        }

        public override void onFinish()
        {
        }
    }
}