using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;

namespace OppoCraft
{
    class CommandChangeUnitState: Task
    {
        OppoMessage message;
        public CommandChangeUnitState(OppoMessage message)
        {
            this.message = message;
        }

        public override void onStart()
        {
            if (this.message.Text.ContainsKey("startact"))
            {
                //this.unit.state = (Unit.State)this.message["state"];
                this.unit.animation.startAction(this.message.Text["startact"]);
            }
            if (this.message.Text.ContainsKey("onlyact"))
            {
                //this.unit.state = (Unit.State)this.message["state"];
                this.unit.animation.Clear();
                this.unit.animation.startAction(this.message.Text["onlyact"]);
            }
            if (this.message.Text.ContainsKey("stopact"))
            {
                //this.unit.state = (Unit.State)this.message["state"];
                this.unit.animation.stopAction(this.message.Text["stopact"]);
            }
            if (this.message.ContainsKey("addhp"))
            {
                this.unit.currHP += this.message["addhp"];
            }

        }
    }
}
