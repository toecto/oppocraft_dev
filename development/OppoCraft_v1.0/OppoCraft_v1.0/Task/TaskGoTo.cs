using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    public class TaskGoTo : Task    
    {       
        int currStep;
        int totalSteps;
        Vector2 destination;
        WorldPath worldPath;
        WorldCoords dest;
        MessageState messageState = MessageState.Ready;

        enum MessageState { 
            Ready,
            Sent,
            InProcess,
        }

        public TaskGoTo(WorldCoords d)
        {
            this.currStep = 1;
            this.totalSteps = 0;
            this.dest = d;
            this.messageState = MessageState.Ready;
        }
        
        public void GetPath()
        {
            this.worldPath = this.unit.theGame.pathFinder.GetPath(this.unit.location, this.dest,true);
            if (this.worldPath == null)
                return;
            
            this.totalSteps = this.worldPath.Count();
            
            if (this.totalSteps == 0)
                return;
            

            this.destination = this.worldPath.First.Value.getVector2();
        }

        public override bool Tick()
        {

            if (this.messageState == MessageState.Sent && this.unit.task.isRunning(typeof(CommandMovement)))
                this.messageState = MessageState.InProcess;

            if (this.messageState == MessageState.InProcess && !this.unit.task.isRunning(typeof(CommandMovement)))
                this.messageState = MessageState.Ready;

            if (this.messageState == MessageState.Ready)
            {
                if (this.currStep >= this.totalSteps)
                {
                    return false;
                }
                this.destination = this.worldPath.ElementAt(this.currStep).getVector2();

                OppoMessage msg = new OppoMessage(OppoMessageType.Movement);
                msg["x"] = (int)this.destination.X;
                msg["y"] = (int)this.destination.Y;
                this.unit.AddCommand(msg);
                
                this.currStep++;
                this.messageState = MessageState.Sent;
            }
            return true;
        }

        public override void onStart()
        {
            base.onStart();
            this.GetPath();
        }

        public override void onFinish()
        {
            OppoMessage msg = new OppoMessage(OppoMessageType.ChangeState);
            msg.Text["stopact"] = "Walk";
            this.unit.AddCommand(msg);
        }
    }
}
