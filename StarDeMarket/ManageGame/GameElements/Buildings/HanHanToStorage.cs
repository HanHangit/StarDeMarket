using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    class HanHanToStorage : Task
    {

        EItem item;

        EStatus status;

        Point target;

        int amount;

        public HanHanToStorage(Building build, EItem item, int _amount) : base(build)
        {
            this.item = item;
            status = EStatus.OnBase;
            target = BuildingHandler.Instance.bMainBuilding.Bounds.Location;
            amount = _amount;
        }

        public override bool DoTask(GameTime gTime)
        {
            switch(status)
            {
                case EStatus.OnBase:
                    if (!build.Storage.Check(item, amount))
                        return true;
                    build.Storage.Get(item, amount);
                    human.storage.Add(item, amount);
                    status = EStatus.MoveToTarget;
                    return false;
                case EStatus.MoveToTarget:
                    human.Target = target;
                    if (human.MoveToTarget(gTime))
                        status = EStatus.WorkOnTarget;
                    return false;
                case EStatus.WorkOnTarget:
                    BuildingHandler.Instance.bMainBuilding.Storage.Add(item, amount);
                    human.storage.Get(item, amount);
                    status = EStatus.BackToBase;
                    human.Target = build.Bounds.Location;
                    return false;
                case EStatus.BackToBase:
                    if (human.MoveToTarget(gTime))
                        return true;
                    return false;
                default:
                    return true;

            }
        }
    }
}
