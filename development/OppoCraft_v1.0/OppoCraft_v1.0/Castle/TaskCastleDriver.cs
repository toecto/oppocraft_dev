using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    class TaskCastleDriver: Task
    {

        CastleForm form=null;
        UnitCastle castle;

        public override bool Tick()
        {
            if (form == null)
            {
                if (this.unit.theGame.unitSelector.selected == this.unit)
                {
                    this.unit.theGame.forms.Add(this.form = new CastleForm());
                    this.form.onClick += handleForm;
                    this.updateForm(this.form);
                }
            }
            else
            {
                if (!this.form.onScreen)
                {
                    this.form = null;
                    this.unit.theGame.unitSelector.selected = null;
                }
            }

            return true;
        }

        public override void onStart()
        {
            this.castle = (UnitCastle)this.unit;
            //this.unit.task.Add(new TaskTowerDriver());
        }

        public void handleForm(GameFormControl obj, WorldCoords mouse)
        {

            foreach (GameFormControl item in obj.parentForm.controls)
            {
                if (item.tag != "")
                {
                    switch(item.GetType().Name)
                    {
                        case "GameFormUpDown":
                            this.castle.factorySettings[item.tag] = ((GameFormUpDown)item).value;
                            break;

                        case "GameFormCheckGroup":
                            string[] strs = ((GameFormCheckGroup)item).getSelectedValues<string>();
                            this.castle.factorySettings.Text[item.tag] = String.Join(",", strs);
                            break;
                        case "GameFormRadioGroup":
                            if (((GameFormRadioGroup)item).selected != null)
                                this.castle.factorySettings.Text[item.tag] = (string)((GameFormRadioGroup)item).selected.value;
                            break;

                    }
                }
            }

            Debug.WriteLine(this.castle.factorySettings.ToString());
        }

        public void updateForm(GameForm form)
        {
            
            GameFormControl control;
            foreach (string tag in this.castle.factorySettings.Keys)
            {
                control = form.findByTag(tag);
                if (control == null) continue;
                switch (control.GetType().Name)
                {
                    case "GameFormUpDown":
                        ((GameFormUpDown)control).value = this.castle.factorySettings[tag];
                        break;
                }
            }

            foreach (string tag in this.castle.factorySettings.Text.Keys)
            {
                control = form.findByTag(tag);
                if (control == null) continue;
                switch (control.GetType().Name)
                {
                    case "GameFormCheckGroup":
                        ((GameFormCheckGroup)control).setSelectedValues(this.castle.factorySettings.Text[tag].Split(new string[] { "," }, StringSplitOptions.None));
                        break;

                    case "GameFormRadioGroup":
                        ((GameFormRadioGroup)control).setSelectedValue(this.castle.factorySettings.Text[tag]);
                        break;
                }
            }

        }

    }
}
