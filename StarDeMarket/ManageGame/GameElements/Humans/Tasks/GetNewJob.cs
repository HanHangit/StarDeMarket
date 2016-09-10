using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    class GetNewJob : Task
    {
        public GetNewJob(Building _targetBuilding, MainBuilding _mainBuilding, HNonWorker _nonWorker)
        {
            hasSubTask = true;
            subTaskList = new List<Task>();
            subTaskList.Add(new PickUpNewJob(_nonWorker, _targetBuilding, _mainBuilding));
            subTaskList.Add(new MoveToTarget(_nonWorker, _targetBuilding.Bounds.Location.ToVector2()));
        }
    }
}
