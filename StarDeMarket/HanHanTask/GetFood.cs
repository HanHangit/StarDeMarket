using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    class GetFood : Task
    {

        Building target;

        public GetFood(Building build) : base(build)
        {
            status = EStatus.Preparing;
            name = "GetFood";
        }

        public override bool DoTask(GameTime gTime)
        {
            switch (status)
            {
                case EStatus.Preparing:
                    target = BuildingHandler.Instance.buildingList.Find(b => b is BStorage && b.Storage.CheckFood());
                    if (target == null)
                        return true;
                    status = EStatus.MoveToTarget;
                    human.Target = target.Bounds.Location;
                    return false;
                case EStatus.MoveToTarget:
                    if (human.MoveToTarget(gTime))
                        status = EStatus.WorkOnTarget;
                    return false;
                case EStatus.WorkOnTarget:
                    human.storage.Add(target.Storage.GetFood(human.carry));
                    human.Target = build.Bounds.Location;
                    status = EStatus.BackToBase;
                    return false;
                case EStatus.BackToBase:
                    if (human.MoveToTarget(gTime))
                        status = EStatus.OnBase;
                    return false;
                case EStatus.OnBase:
                    build.Storage.Add(human.storage.GetFood(human.carry));
                    return true;
                default:
                    return true;
            }
        }
    }
}
