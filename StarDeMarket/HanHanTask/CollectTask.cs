using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{

    enum EStatus
    {
        SearchTarget,
        MoveToTarget,
        WorkOnTarget,
        BackToBase,
        OnBase,
        Preparing,
        None
    }

    class CollectTask : Task
    {
        EItem toCollect;

        EStatus status;

        Point target;

        float startTimer;

        Tile targetTile;


        public CollectTask(Building building, EItem collect) : base(building)
        {
            toCollect = collect;
            status = EStatus.Preparing;
            target = Point.Zero;
            startTimer = 0;
            targetTile = null;
            name = "CollectTask";
        }

        public override bool DoTask(GameTime gTime)
        {
            if(status == EStatus.Preparing)
            {
                human.Target = build.Bounds.Location;
                if (human.MoveToTarget(gTime))
                    status = EStatus.SearchTarget;
            }
            else if (status == EStatus.SearchTarget)
            {
                targetTile = BuildingHandler.Instance.map.SearchTile(human.position.ToPoint(), toCollect);

                if (targetTile != null)
                {
                    target = targetTile.bounds.Location;
                    targetTile.WorkAble = false;
                    status = EStatus.MoveToTarget;
                }
                else
                    status = EStatus.None;

            }
            else if (status == EStatus.MoveToTarget)
            {
                human.Target = target;

                if (human.MoveToTarget(gTime))
                {
                    startTimer = 0;
                    status = EStatus.WorkOnTarget;
                    human.Target = new Point(0, 0);
                }
            }
            else if (status == EStatus.WorkOnTarget)
            {
                startTimer += (float)gTime.ElapsedGameTime.TotalSeconds * human.workSpeed;
                if (startTimer >= targetTile.WorkTime)
                {
                    targetTile.WorkAble = true;
                    startTimer = 0;
                    if (targetTile.storage.Check(toCollect))
                    {
                        status = EStatus.BackToBase;
                        human.Target = build.Bounds.Location;
                        human.storage.Add(toCollect);
                        targetTile.storage.Get(toCollect, 1);
                        targetTile.Update(gTime);
                    }
                    else
                    {
                        status = EStatus.SearchTarget;
                        targetTile = null;
                    }
                }
            }
            else if (status == EStatus.BackToBase)
            {

                if (human.MoveToTarget(gTime))
                {
                    build.Storage.Add(toCollect);
                    human.storage.Get(toCollect);
                    status = EStatus.None;
                    return true;
                }
            }
            else
                return true;

            return false;


        }


        bool CheckTile(Tile t)
        {
            if (t.storage.Check(toCollect, 1) && t.WorkAble)
                return true;
            else
                return false;
        }
    }
}
