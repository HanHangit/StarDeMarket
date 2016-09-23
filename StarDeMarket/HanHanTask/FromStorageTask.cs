using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    class FromStorageTask : Task
    {
        EItem item;

        BStorage target;

        int amount;

        public FromStorageTask(Building building, EItem _item, int _amount) : base(building)
        {

            item = _item;
            status = EStatus.Preparing;
            amount = _amount;
            target = (BStorage)BuildingHandler.Instance.buildingList.Find(b => b is BStorage && (b.Storage.getCount(item) >= amount));
            if (target == null)
            {
                status = EStatus.None;
            }
            name = "ToStorage";
        }

        public override bool DoTask(GameTime gTime)
        {
            if (amount > human.carry)
                amount = human.carry;

            switch (status)
            {
                case EStatus.Preparing:
                    human.Target = build.Bounds.Location;
                    if (human.MoveToTarget(gTime))
                    {
                        // this is just here to test the Pathfinding
                        human.SetPath(BuildingHandler.Instance.map.GetPathBetweenBuildings(build, target));
                        status = EStatus.MoveOnPath;
                        if (human.path == null)
                        {
                            human.Target = target.Bounds.Location;
                            status = EStatus.MoveToTarget;
                        }
                    }
                    return false;
                case EStatus.MoveOnPath:
                    if (human.FollowPath(gTime))
                    {
                        status = EStatus.WorkOnTarget;
                    }
                    return false;
                case EStatus.MoveToTarget:
                    if(human.MoveToTarget(gTime))
                    {
                        status = EStatus.WorkOnTarget;
                    }
                    return false;
                case EStatus.WorkOnTarget:
                    human.storage.Add(item, target.Storage.Get(item, amount));
                    status = EStatus.BackToBase;
                    human.SetPath(BuildingHandler.Instance.map.GetPathBetweenBuildings(target, build));
                    if(human.path == null)
                    {
                        human.Target = build.Bounds.Location;
                    }
                    return false;
                case EStatus.BackToBase:
                    if (human.path == null)
                    {
                        if(human.MoveToTarget(gTime))
                        {
                            status = EStatus.OnBase;
                        }
                    }
                    if (human.FollowPath(gTime))
                    {
                        status = EStatus.OnBase;
                    }
                    return false;
                case EStatus.OnBase:
                    build.Storage.Add(item, human.storage.Get(item, amount));
                    status = EStatus.None;
                    return true;
                default:
                    return true;
            }
        }
    }
}
