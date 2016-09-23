using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    class ToStorageTask : Task
    {

        EItem item;

        BStorage target;

        int amount;

        public ToStorageTask(Building build, EItem item, int _amount) : base(build)
        {
            this.item = item;
            status = EStatus.Preparing;
            target = (BStorage)BuildingHandler.Instance.buildingList.Find(b => b is BStorage);
            amount = _amount;
        }

        public override bool DoTask(GameTime gTime)
        {
            
            switch(status)
            {
                case EStatus.Preparing:
                    human.Target = build.Bounds.Location;
                    if (human.MoveToTarget(gTime))
                        status = EStatus.OnBase;
                    return false;
                case EStatus.OnBase:
                    if (!build.Storage.Check(item, amount))
                        return true;
                    build.Storage.Get(item, amount);
                    human.storage.Add(item, amount);
                    
                    // this is just here to test the Pathfinding
                    human.SetPath(BuildingHandler.Instance.map.GetPathBetweenBuildings(build, target));
                    status = EStatus.MoveOnPath;
                    if (human.path == null)
                    {
                        human.Target = target.Bounds.Location;
                        status = EStatus.MoveToTarget;
                    }
                    return false;
                case EStatus.MoveToTarget:
                    if (human.MoveToTarget(gTime))
                        status = EStatus.WorkOnTarget;
                    return false;
                case EStatus.MoveOnPath:
                    if (human.FollowPath(gTime))
                    {
                        status = EStatus.WorkOnTarget;
                    }
                    return false;
                case EStatus.WorkOnTarget:
                    target.Storage.Add(item, amount);
                    human.storage.Get(item, amount);
                    status = EStatus.BackToBase;
                    human.SetPath(BuildingHandler.Instance.map.GetPathBetweenBuildings(target, build));
                    if (human.path == null)
                    {
                        human.Target = build.Bounds.Location;
                    }
                    return false;
                case EStatus.BackToBase:
                    if (human.path == null)
                    {
                        if (human.MoveToTarget(gTime))
                        {
                            status = EStatus.OnBase;
                        }
                    }
                    if (human.FollowPath(gTime))
                    {
                        status = EStatus.OnBase;
                    }
                    return false;
                default:
                    return true;
            }
        }
    }
}
