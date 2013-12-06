using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    class DriverTaskFactory
    {
        public static Task Create(string type)
        {
            switch (type)
            {
                case "Knight":
                    return new TaskKnightDriver();

                case "Archer":
                    return new TaskKnightDriver();

                case "Tree":
                    return new TaskTreeDriver();

                case "Obstacle":
                    return new TaskObstacleDriver();

            }

            return null;
        }
    }
}
