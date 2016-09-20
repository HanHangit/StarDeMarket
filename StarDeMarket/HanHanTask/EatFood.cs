using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    class EatFood : Task
    {
        public EatFood(Building b) : base(b)
        {
            status = EStatus.Preparing;
            name = "EatFood";
        }


        public override bool DoTask(GameTime gTime)
        {
            switch(status)
            {
                case EStatus.Preparing:
                    {
                        human.Target = build.Bounds.Location;
                        if (human.MoveToTarget(gTime))
                            status = EStatus.OnBase;
                        return false;
                    }
                case EStatus.OnBase:
                    {
                        if(build.Storage.CheckFood())
                        {
                            human.currPower = human.maxPower;
                            build.Storage.GetFood(1);
                            status = EStatus.None;
                        }
                        return true;
                    }
                default:
                    return true;
            }
        }
    }
}
