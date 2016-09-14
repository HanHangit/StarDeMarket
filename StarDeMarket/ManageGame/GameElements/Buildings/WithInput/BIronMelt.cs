using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace StarDeMarket
{
    class BIronMelt : BuildingWithInput
    {
        public BIronMelt(Vector2 _pos, ContentManager cont)
        {
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/IronMelt");
            storage = new Storage();
            position = _pos;
            name = "IronMelt";

            productionTime = 10;

            input = new EItem[] { EItem.Eisen, EItem.Kohle };
            inputCount = new int[] { 2 , 1};

            output = new EItem[] { EItem.Eisenbarren };
            outputCount = new int[] { 1 };
        }

        public override void Update(GameTime gTime)
        {
            Production(gTime);
            base.Update(gTime);
            if (taskQueue.Count == 0)
            {
                if (storage.getCount(EItem.Eisen) < 3)
                {
                    taskQueue.Enqueue(new FromStorageTask(this, EItem.Eisen, 5));
                }
                if (storage.getCount(EItem.Kohle) < 3)
                {
                    taskQueue.Enqueue(new FromStorageTask(this, EItem.Kohle, 5));
                }
                if (storage.getCount(EItem.Eisenbarren) > 5)
                {
                    taskQueue.Enqueue(new ToStorageTask(this, EItem.Eisenbarren, 5));
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width, texture2D.Height)), Color.White);
            base.Draw(spriteBatch);
        }

        public override void Workerwork()
        {
            if (HasFullWorkforce())
                Console.WriteLine("Ein Schmelzer fehlt hier! HILFE!@Matthis");
        }

        public override bool HasFullWorkforce()
        {
            if (listWorker.Count == 2)
                return true;
            else
                return false;
        }
    }
}

