using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    class ProduceTask : Task
    {

        BuildingWithInput inputBuild;

        public ProduceTask(Building building) : base(building)
        {
            inputBuild = (BuildingWithInput)building;
            status = EStatus.Preparing;
            name = "ProduceTask";
        }

        public override bool DoTask(GameTime gTime)
        {
            switch(status)
            {
                case EStatus.Preparing:
                    human.Target = build.Bounds.Location;
                    status = EStatus.MoveToTarget;
                    return false;
                case EStatus.MoveToTarget:
                    if (human.MoveToTarget(gTime))
                    {
                        status = EStatus.WorkOnTarget;
                        if (!inputBuild.CheckRessourcen())
                        {
                            return true;
                        }

                    }
                    return false;
                case EStatus.WorkOnTarget:
                    return inputBuild.Production(gTime);
                default:
                    return true;



            }
        }
    }
}
