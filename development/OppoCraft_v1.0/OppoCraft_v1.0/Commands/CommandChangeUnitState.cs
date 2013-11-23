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
            if(this.message.ContainsKey("state"))
                this.unit.state = (Unit.State)this.message["state"];
        }
    }
}
