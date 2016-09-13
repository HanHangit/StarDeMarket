using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    class PickUpNewJob : Task
    {
        Building building;
        MainBuilding mainBuilding;
        HNonWorker nonWorker;

        public PickUpNewJob(HNonWorker _nonWorker, Building _building, MainBuilding _mainBuilding)
        {
            hasSubTask = false;
            building = _building;
            mainBuilding = _mainBuilding;
            nonWorker = _nonWorker;
            human = nonWorker;
        }

        public override void ExecuteSpecialTask()
        {
            Console.WriteLine("ExecuteSpecialTask");
            mainBuilding.avaiableWorker.Remove(nonWorker);
            building.EmployHuman(human);
        }
    }
}
