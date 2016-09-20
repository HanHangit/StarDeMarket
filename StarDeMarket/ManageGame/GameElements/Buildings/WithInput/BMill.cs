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
    class BMill : BuildingWithInput
    {
        public BMill(Vector2 _pos, ContentManager cont)
        {
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/Mill");
            storage = new Storage();
            position = _pos;
            name = "Mill";

            productionTime = 3;

            input = new EItem[] { EItem.Getreide, EItem.Kohle };
            inputCount = new int[] { 2, 1 };

            output = new EItem[] { EItem.Mehl };
            outputCount = new int[] { 1 };
        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);
            if (taskQueue.Count == 0)
            {
                if (CheckRessourcen())
                    taskQueue.Enqueue(new ProduceTask(this), 3);

                if (storage.getCount(EItem.Getreide) < 3)
                {
                    taskQueue.Enqueue(new FromStorageTask(this, EItem.Getreide, 5),4);
                }

                if (storage.getCount(EItem.Kohle) < 3)
                {
                    taskQueue.Enqueue(new FromStorageTask(this, EItem.Kohle, 5),4);
                }

                if (storage.getCount(EItem.Mehl) > 5)
                {
                    taskQueue.Enqueue(new ToStorageTask(this, EItem.Mehl, 5),2);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width, texture2D.Height)), Color.White);
            base.Draw(spriteBatch);
        }

    }
}
