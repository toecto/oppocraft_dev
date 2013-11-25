using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;

namespace OppoCraft
{
    class TaskFight: Task
    {
        Unit target;
        WorldCoords going=null;
        int cooldown;
        public TaskFight(Unit target)
        {
            this.target = target;
        }

        public override bool Tick()
        {
            if (target.currHP == 0) return false;

            if (this.unit.location.Distance(this.target.location) < this.unit.attackRange)
            {

                if (this.going != null)
                {
                    OppoMessage msg = new OppoMessage(OppoMessageType.Stop);
                    msg["x"] = this.unit.location.X;
                    msg["y"] = this.unit.location.Y;
                    this.unit.AddCommand(msg);
                    this.going = null;
                }

                if (this.cooldown <= 0)
                {
                    this.cooldown = this.unit.attackSpeed;
                    OppoMessage msg = new OppoMessage(OppoMessageType.ChangeState);
                    msg["uid"] = this.target.uid;
                    msg["addhp"] = -this.unit.damage;
                    this.unit.theGame.AddCommand(msg);

                    msg = new OppoMessage(OppoMessageType.ChangeState);
                    msg.Text["startact"] = "Attack";
                    this.unit.AddCommand(msg);
                }
                this.cooldown--;                

            }
            else
            {
                if (this.going==null || this.going!=this.target.location)
                {
                    this.unit.task.Add(new TaskGoTo(target.location));
                    this.going = target.location;
                }
            }
            
            return true;
        }

        public override void onStart()
        {
        }

        public override void onFinish()
        {
            OppoMessage msg = new OppoMessage(OppoMessageType.ChangeState);
            msg.Text["stopact"] = "Attack";
            this.unit.AddCommand(msg);
        }
    }
}
