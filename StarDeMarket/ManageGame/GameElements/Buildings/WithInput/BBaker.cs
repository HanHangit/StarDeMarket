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
    class BBaker : BuildingWithInput
    {        
        public BBaker(Vector2 _pos, ContentManager cont)
        {
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/Baker");
            storage = new Storage();
            position = _pos;
            name = "Baker";

            productionTime = 5;

            input = new EItem[] { EItem.Mehl, EItem.Kohle };
            inputCount = new int[] { 2, 1 };

            output = new EItem[] { EItem.Brot };
            outputCount = new int[] { 1 };
        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);
            if (taskQueue.Count == 0)
            {
                if (CheckRessourcen())
                    taskQueue.Enqueue(new ProduceTask(this), 3);

                if (storage.getCount(EItem.Mehl) < 3)
                {
                    taskQueue.Enqueue(new FromStorageTask(this, EItem.Mehl, 5),5);
                }
                if (storage.getCount(EItem.Kohle) < 3)
                {
                    taskQueue.Enqueue(new FromStorageTask(this, EItem.Kohle, 5),5);
                }
                if (storage.getCount(EItem.Brot) > 5)
                {
                    taskQueue.Enqueue(new ToStorageTask(this, EItem.Brot, 5),2);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width, texture2D.Height)), Color.White);
        }

    }
}

