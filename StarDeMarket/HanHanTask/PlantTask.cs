using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    class PlantTask : Task
    {

        EItem item;

        ETile tile;

        Tile targetTile;

        Point target;

        float timer;

        

        public PlantTask(Farm build, EItem growItem, ETile growTile) : base(build)
        {
            timer = 0;
            status = EStatus.Preparing;
            item = growItem;
            tile = growTile;
        }

        public override bool DoTask(GameTime gTime)
        {
            if (status == EStatus.Preparing)
            {
                human.Target = build.Bounds.Location;
                if (human.MoveToTarget(gTime))
                    status = EStatus.SearchTarget;
            }
            else if (status == EStatus.SearchTarget)
            {
                targetTile = BuildingHandler.Instance.map.SearchTile(human.position.ToPoint(), tile);

                if (targetTile != null)
                {
                    target = targetTile.bounds.Location;
                    targetTile.WorkAble = false;
                    status = EStatus.MoveToTarget;
                }
                else
                    status = EStatus.None;
            }
            else if(status == EStatus.MoveToTarget)
            {
                human.Target = targetTile.bounds.Location;
                if (human.MoveToTarget(gTime))
                    status = EStatus.WorkOnTarget;
            }
            else if (status == EStatus.WorkOnTarget)
            {
                timer += (float)gTime.ElapsedGameTime.TotalSeconds;
                if (timer >= targetTile.WorkTime)
                {
                    Farm b = (Farm)build;
                    b.PlantField(targetTile.bounds.Location);
                    targetTile.type = ETile.GrowField;
                    status = EStatus.BackToBase;
                }
            }
            else if (status == EStatus.BackToBase)
            {
                human.Target = build.Bounds.Location;
                if (human.MoveToTarget(gTime))
                    status = EStatus.None;
            }
            else
                return true;

            return false;

        }
    }
}
