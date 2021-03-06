﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class BWheatFarm : Farm
    {

        public BWheatFarm(Vector2 _pos, ContentManager _cont)
        {
            //Wie bei einem normalem Gebäude
            cont = _cont;
            texture2D = cont.Load<Texture2D>("Building/WheatFarm");
            position = _pos;
            storage = new Storage();
            name = "WheatFarm";
        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);
            if(taskQueue.Count == 0)
            {
                if (storage.getCount(EItem.Getreide) >= 5) //Task für das zuürckbringen
                    taskQueue.Enqueue(new ToStorageTask(this, EItem.Getreide, 5),1);

                //Ob neue Ackerböden gebaut werden dürfen
                bool newPlants = true;
                foreach (GrowingField f in fields)
                    if (f.finished) //Wenn ein Acker gefunden wurde, der schon geerntet werden kann.
                        newPlants = false; //Es darf kein neuer Ackerboden gebaut werden

                if(newPlants)
                    taskQueue.Enqueue(new PlantTask(this, EItem.Getreide,ETile.Grass),3); //Neuer Task für das anbauen eines neuen Ackerboden
                else
                    taskQueue.Enqueue(new CollectTask(this, EItem.Getreide),2); //Task für das Abernten
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width, texture2D.Height)), Color.White);
        }

        public override void PlantField(Point plantPos)
        {
            //Wenn neuer Acker gebaut, dann zur Liste hinzufügen.
            fields.Add(new GrowingField(plantPos, new[] { cont.Load<Texture2D>("Tile/GrowingField01"), cont.Load<Texture2D>("Tile/WheatField01") },10,EItem.Getreide));
        }
    }
}
