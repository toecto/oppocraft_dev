using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;

namespace OppoCraft
{
    class UnitDataLoader
    {
   


        public static bool Load(Unit theUnit, string type)
        {
            DataTable unitDataTable = theUnit.theGame.db.Query("Select * from Units where UnitType like '" + type + "'");
            if (unitDataTable.Rows.Count == 0) return false;
            DataRow unitData = unitDataTable.Rows[0];

            if (unitData["AnimationFileID"] != DBNull.Value)
            {
                DataTable animationFileDataTable = theUnit.theGame.db.Query("Select Path from AnimationFile where AnimationFileID=" + unitData.Field<int>("AnimationFileID"));
                if (animationFileDataTable.Rows.Count > 0)
                    theUnit.animation = theUnit.theGame.graphContent.GetUnitAnimation(theUnit, animationFileDataTable.Rows[0].Field<string>("Path"));
            }
            theUnit.group = unitData.Field<string>("Group");
            theUnit.isObstacle = unitData.Field<bool>("Obstacle");
            theUnit.size = theUnit.theGame.theGrid.getWorldCoords(new GridCoords(unitData.Field<int>("Width"), unitData.Field<int>("Height")));

            if (theUnit.isMy || theUnit.isServed)
            {
                if (unitData["Driver"] != DBNull.Value)
                {
                    Task task = DriverTaskFactory.Create(unitData.Field<string>("Driver"));
                    if (task != null)
                        theUnit.task.Add(task);
                }
            }

            /*
            if (unitData.Field<int>("AnimationID") != DBNull.Value)
            {
                DataTable animationDataTable = theUnit.theGame.db.Query("Select Path from Animation where AnimationID=" + unitData.Field<int>("AnimationID"));
                if (animationDataTable.Rows.Count > 0)
                    theUnit.animation.startAction("");
            }
            */
            return true;
        }
    }
}
