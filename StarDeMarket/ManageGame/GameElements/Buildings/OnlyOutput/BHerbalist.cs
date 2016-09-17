using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class BHerbalist : Building
    {

        public BHerbalist(Vector2 _pos, ContentManager cont)
        {
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/Herbalist");

            position = _pos;
            storage = new Storage();
            name = "CoalMine";

            output = new EItem[] { EItem.Pilze };
            outputCount = new int[] { 2 };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width, texture2D.Height)), Color.White);
        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);
            if (taskQueue.Count == 0)
            {
                if (storage.getCount(EItem.Kohle) > 5)
                {
                    taskQueue.Enqueue(new ToStorageTask(this, EItem.Pilze, 5), 1);
                }
                else
                {
                    taskQueue.Enqueue(new CollectTask(this, EItem.Pilze), 2);
                }
            }
        }


        public override bool HasFullWorkforce()
        {
            throw new NotImplementedException();
        }

        public override void Workerwork()
        {
            throw new NotImplementedException();
        }
    }
}
