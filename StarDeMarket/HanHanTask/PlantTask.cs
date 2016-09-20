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

        
        /// <summary>
        /// Mit diesem Task wird ein Item angebaut.
        /// </summary>
        /// <param name="build">Das zugehörige Gebäude</param>
        /// <param name="growItem">Den Gegenstand der angebaut wird.</param>
        /// <param name="growTile">Auf welchen Feld angebaut werden darf.</param>
        public PlantTask(Farm build, EItem growItem, ETile growTile) : base(build)
        {
            timer = 0;
            status = EStatus.Preparing;
            item = growItem;
            tile = growTile;
        }

        public override bool DoTask(GameTime gTime)
        {
            //Zuerst zum Gebäude zurück gehen.
            if (status == EStatus.Preparing)
            {
                human.Target = build.Bounds.Location;
                if (human.MoveToTarget(gTime))
                    status = EStatus.SearchTarget;
            }
            else if (status == EStatus.SearchTarget) //Ein freies Feld suchen.
            {
                //SuchAlgorithmus
                targetTile = BuildingHandler.Instance.map.SearchTile(human.position.ToPoint(), tile);

                //Wenn ein Feld gefunden wude, dann wird es als Ziel gesetzt.
                if (targetTile != null)
                {
                    target = targetTile.bounds.Location;
                    targetTile.WorkAble = false;
                    status = EStatus.MoveToTarget;
                }
                else
                    status = EStatus.None; //Wenn nicht, wird die Aufgabe abgebrochen.
            }
            else if(status == EStatus.MoveToTarget) //Zum Zielfeld, gehen.
            {
                human.Target = targetTile.bounds.Location;
                if (human.MoveToTarget(gTime))
                    status = EStatus.WorkOnTarget;
            }
            else if (status == EStatus.WorkOnTarget) //Auf dem Zielfeld arbeiten.
            {
                timer += (float)gTime.ElapsedGameTime.TotalSeconds;
                if (timer >= targetTile.WorkTime)
                {
                    //Cast zu Farm, damit ich auf die Funktionen von Farm zugreifen kann.
                    Farm b = (Farm)build;

                    //mithilfe von PlantField, wird der Gegenstand gepflanzt.
                    b.PlantField(targetTile.bounds.Location);

                    //Kennzeichnet, dass auf dem Feld ein "Acker" ist.
                    targetTile.type = ETile.GrowField;

                    //Als nächstes zurück zur Basis gehen.
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
