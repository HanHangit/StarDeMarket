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
            status = EStatus.SearchTarget;
            target = Point.Zero;
            startTimer = 0;
            targetTile = null;
            name = "CollectTask";
        }

        public override bool DoTask(GameTime gTime)
        {

            if (status == EStatus.SearchTarget)
            {

                int i = (int)human.position.X;
                int j = (int)human.position.Y;
                int k = 8;

                Queue<Tile> queue = new Queue<Tile>();
                queue.Enqueue(BuildingHandler.Instance.map.GetTile(new Point(i, j)));


                while (targetTile == null)
                {



                    //TODO: Range des Humans
                    k += BuildingHandler.Instance.map.tilesize;
                    {
                        for (int s = i - k; s <= i + k; s += BuildingHandler.Instance.map.tilesize)
                            queue.Enqueue(BuildingHandler.Instance.map.GetTile(new Point(s, j - k)));
                        for (int s = i - k; s <= i + k; s += BuildingHandler.Instance.map.tilesize)
                            queue.Enqueue(BuildingHandler.Instance.map.GetTile(new Point(s, j + k)));
                        for (int s = j - k; s <= j + k; s += BuildingHandler.Instance.map.tilesize)
                            queue.Enqueue(BuildingHandler.Instance.map.GetTile(new Point(i - k, s)));
                        for (int s = j - k; s <= j + k; s += BuildingHandler.Instance.map.tilesize)
                            queue.Enqueue(BuildingHandler.Instance.map.GetTile(new Point(i + k, s)));
                    }

                    while (queue.Count > 0)
                    {
                        Tile help = queue.Dequeue();

                        if (CheckTile(help))
                        {
                            targetTile = help;
                            status = EStatus.MoveToTarget;
                            break;
                        }

                    }

                }

                target = targetTile.bounds.Location;
                targetTile.WorkAble = false;

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
                if (startTimer >= 1f)
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
