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

    class BStonemason : BuildingWithInput
    { 
        public BStonemason(Vector2 _pos, ContentManager cont)
        {
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/Woodcutter01");
            position = _pos;
            storage = new Storage();
            name = "StoneMason";

            output = new EItem[] { EItem.Stein };
            outputCount = new int[] { 2 };
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width,texture2D.Height)), Color.White);
        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);
            if(taskQueue.Count == 0)
            {
                if(storage.getCount(EItem.Stein) > 5)
                {
                    taskQueue.Enqueue(new ToStorageTask(this, EItem.Stein, 5));
                }
                else
                {
                    taskQueue.Enqueue(new CollectTask(this, EItem.Stein));
                }
            }
        }

        public override void Workerwork()
        {
            if (HasFullWorkforce())
                Console.WriteLine("Matthis hat scheiße gebaut");
                //tom = new HWoodcutter(new Vector2(1, 2), Human.EGender.Male, cont.Load<Texture2D>("Human/Hunter"));
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
