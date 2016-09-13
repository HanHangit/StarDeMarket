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

    class BFishingHut : OnlyOutput
    {
       
        EItem[] output = { EItem.Fisch };
        int[] outputCount = { 2 };

        public BFishingHut(Vector2 _pos, ContentManager _cont)
        {
            cont = _cont;
            texture2D = cont.Load<Texture2D>("Building/Woodcutter01");
            position = _pos;
            storage = new Storage();
            name = "Fishing Hut";
            
            for(int i = 0; i < output.Length; ++i)
            {
                taskQueue.Enqueue(new CollectTask(this, output[i]));
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width,texture2D.Height)), Color.White);

        }

        public override void Update(GameTime gTime)
        {

            if(taskQueue.Count == 0)
            {

                for (int i = 0; i < output.Length; ++i)
                {

                    if (storage.Check(EItem.Holz, 5))
                        taskQueue.Enqueue(new ToStorageTask(this, EItem.Holz, 5));
                    else
                        taskQueue.Enqueue(new CollectTask(this, output[i]));
                };
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
