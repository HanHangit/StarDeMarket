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

    class BWoodCutter : OnlyOutput
    {
        
        Storage storage;
        EItem[] output = { EItem.Holz };
        int[] outputCount = { 2 };

        public BWoodCutter(Vector2 _pos, ContentManager _cont)
        {
            cont = _cont;
            texture2D = cont.Load<Texture2D>("Building/Woodcutter01");
            position = _pos;
            storage = new Storage();
            name = "WoodCutter";
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width,texture2D.Height)), Color.White);
        }


        public override void Update(GameTime gTime)
        {
            if (taskQueue.Count == 0)
            {
                if(storage.getCount(EItem.Holz) > 5)
                {
                    taskQueue.Enqueue(new ToStorageTask(this, EItem.Holz, 5));
                }
                else
                {
                    taskQueue.Enqueue(new CollectTask(this, EItem.Holz));
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
