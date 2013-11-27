using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    class TaskMortable : Task
    {
        public override bool Tick()
        {
            if(this.unit.currHP<=0)
            {

                return false;
            }
            return true;
        }

        public override void onStart()
        {
        }

        public override void onFinish()
        {
            this.unit.task.Clear();
            OppoMessage msg = new OppoMessage(OppoMessageType.ChangeState);
            msg.Text["onlyact"] = "Die";
            this.unit.AddCommand(msg);
        }
    }
}
