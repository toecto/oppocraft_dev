using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class UnitCastle: Unit
    {
        

        public OppoMessage factorySettings;
        public int trainingCooldown = 0;
        public int trainingSpeedReal { get { return 3600 / this.factorySettings["trainingspeed"]; } }
        
        public UnitCastle(Game1 theGame, OppoMessage settings)
            : base(theGame,settings)
        {
            this.factorySettings = new OppoMessage(OppoMessageType.ChangeState);
            this.factorySettings["attack"] = 5;
            this.factorySettings["attackspeed"] = 30;
            this.factorySettings["attackrange"] = 1;
            this.factorySettings["viewrange"] = 10;
            this.factorySettings["speed"] = 10;
            this.factorySettings["armour"] = 0;
            this.factorySettings["trainingspeed"] = 50;
            this.factorySettings["training"] = 1;
            this.factorySettings.Text["zone"] = "";
            this.factorySettings.Text["unittype"] = "Knight";
            this.factorySettings.Text["targets"] = "Knight";
        }


        public override void Render(RenderSystem render)
        {
            base.Render(render);

            Vector2 position = render.getScreenCoords(this.location);
            position.X -= render.primRect50.Bounds.Width / 2;
            if (this.animation.current.First != null)
                position.Y -= this.animation.current.First.Value.currentAnimation.file.size.Y / 2 + 10;

            if (trainingCooldown > 0)
            {
                Rectangle bar = new Rectangle(0, 0, render.primRect50.Bounds.Width, 2);
                render.Draw(render.primDot, position, bar, Microsoft.Xna.Framework.Color.Blue);
                bar = new Rectangle(0, 0, render.primRect50.Bounds.Width * trainingCooldown / this.trainingSpeedReal, 2);
                render.Draw(render.primDot, position, bar, Microsoft.Xna.Framework.Color.LightSkyBlue);
            }
        }



        public virtual void applySettings(OppoMessage msg, OppoMessage settings)
        {
            msg["attack"] = settings["attack"];
            msg["attackspeed"] = settings["attackspeed"];
            msg["attackrange"] = settings["attackrange"];
            msg["viewrange"] = settings["viewrange"];
            msg["speed"] = settings["speed"];
            msg["armour"] = settings["armour"];
            msg["viewrange"] = settings["viewrange"];
            msg.Text["type"] = settings.Text["unittype"];
            msg.Text["zone"] = settings.Text["zone"];
            msg.Text["targets"] = settings.Text["targets"];
        }


        public virtual void tryToSpawn()
        {
            if (this.factorySettings["training"] == 1)
            {
                OppoMessage msg;
                msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.theGame.CreateUID();
                msg["ownercid"] = this.cid;
                msg["x"] = this.location.X;
                msg["y"] = this.location.Y + 40 * 2;

                this.applySettings(msg, this.factorySettings);
                this.theGame.AddCommand(msg);
            }
        }

        

    }
}
