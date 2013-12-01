using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Data;
using System.Diagnostics;

namespace OppoCraft
{
    public class GraphContentManager
    {
        Game1 theGame;
        public Dictionary<string, AnimationFile> files;

        public GraphContentManager(Game1 game)
        {
            this.files = new Dictionary<string, AnimationFile>();
            this.theGame = game;
        }

        public void LoadContent()
        {
            DataTable filesData = this.theGame.db.Query("SELECT * FROM AnimationFile");
            foreach (DataRow AnimationFileData in filesData.Rows)
            {
                Debug.WriteLine("AnimationFileData" + (string)AnimationFileData["Path"]);
                Texture2D texture;
                AnimationFile file;
                string name;
                if ((bool)AnimationFileData["Coloured"])
                {
                    this.files.Add((string)AnimationFileData["Path"], null);//mark that the animation exists and coloured
                    name = "Blue" + (string)AnimationFileData["Path"];
                    texture = this.LoadTexture(name);
                    file = new AnimationFile(texture, (int)AnimationFileData["FrameWidth"], (int)AnimationFileData["FrameHeight"], (int)AnimationFileData["AnimationFileID"]);
                    this.files.Add(name, file);

                    name = "Red" + (string)AnimationFileData["Path"];
                    texture = this.LoadTexture(name);
                    file = new AnimationFile(texture, (int)AnimationFileData["FrameWidth"], (int)AnimationFileData["FrameHeight"], (int)AnimationFileData["AnimationFileID"]);
                    this.files.Add(name, file);
                }
                else
                {
                    name =  (string)AnimationFileData["Path"];
                    texture = this.LoadTexture(name);
                    file = new AnimationFile(texture, (int)AnimationFileData["FrameWidth"], (int)AnimationFileData["FrameHeight"], (int)AnimationFileData["AnimationFileID"]);
                    this.files.Add(name, file);

                }
            }
        }

        public Texture2D LoadTexture(string name)
        {
            return this.theGame.Content.Load<Texture2D>("Animations\\" + name);
        }

        public UnitAnimation GetUnitAnimation(Unit unit, string name)
        {
            AnimationFile file = this.files[name];

            if (file == null)
            {
                if (unit.cid == this.theGame.cid)
                    name = "Blue" + name;
                else
                    name = "Red" + name;
                file = this.files[name];
            }
            

            DataTable actions = this.theGame.db.Query("SELECT * FROM Animation where AnimationFileID=" + file.id);
            UnitAnimation unitAnimation = new UnitAnimation(unit);
            List<SimpleAnimation> actionAnimations = null;
            foreach (DataRow action in actions.Rows)
            {
                if ((string)action["AnimationMap"] == "Directions")
                {
                    actionAnimations = file.getAnimations((int)action["StartX"], (int)action["StartY"], (int)action["Frames"], (int)action["Delay"], (bool)action["Looped"], 2, 4);
                    unitAnimation.Add((string)action["AnimationNameID"], new ActionAnimationByDirection((string)action["AnimationNameID"],actionAnimations, unit, (int)action["Priority"]));
                }
                else
                {
                    actionAnimations = file.getAnimations((int)action["StartX"], (int)action["StartY"], (int)action["Frames"], (int)action["Delay"], (bool)action["Looped"]);
                    unitAnimation.Add((string)action["AnimationNameID"], new ActionAnimation((string)action["AnimationNameID"],actionAnimations, unit, (int)action["Priority"]));
                }
            }

            return unitAnimation;
        }
        
        public SimpleAnimation GetDecaleAnimation(string name)
        {
            AnimationFile file = this.files[name];
            DataTable actions = this.theGame.db.Query("SELECT Animation.*, Units. FROM Units, Animation where Decales.AnimationID=Animation.AnimationID and Decales.Name='" + name + "'");
            DataRow action=actions.Rows[0];
            SimpleAnimation rez= file.getAnimations((int)action["StartX"], (int)action["StartY"], (int)action["Frames"], (int)action["Delay"], (bool)action["Looped"])[0];
            return rez;
        }
    }
}
