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
    class BSawmill : BuildingWithInput
    {
        EItem[] input = { EItem.Holz };
        int[] inputCount = { 2 };

        EItem[] output = { EItem.Bretter };
        int[] outputCount = { 1 };

        float productionTime;
        float productionCounter;
        bool currentlyProducing;

        public BSawmill(Vector2 _pos, ContentManager cont)
        {
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/Sawmill");
            storage = new Storage();
            position = _pos;
            name = "Sawmill";
            productionTime = 2;
            currentlyProducing = false;
        }

        public override void Update(GameTime gTime)
        {
            Production(gTime);
            base.Update(gTime);
            if(taskQueue.Count == 0)
            {
                if(storage.getCount(EItem.Holz) < 3)
                {
                    taskQueue.Enqueue(new FromStorageTask(this, EItem.Holz, 5));
                }
                if(storage.getCount(EItem.Bretter) > 5)
                {
                    taskQueue.Enqueue(new ToStorageTask(this, EItem.Bretter, 5));
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width, texture2D.Height)), Color.White);
        }

        public override void Workerwork()
        {
            if (HasFullWorkforce())
                Console.WriteLine("Ein Schreiner fehlt hier! HILFE!@Matthis");
        }

        public override void Production(GameTime gTime)
        {
            if (CheckRessourcen() && !currentlyProducing)
            {
                currentlyProducing = true;
                for (int i = 0; i < input.Length; ++i)
                {
                    storage.Get(input[i], inputCount[i]);
                }
                productionCounter = productionTime;
            }
            if (currentlyProducing)
            {
                productionCounter -= (float)gTime.ElapsedGameTime.TotalSeconds;
                if (productionCounter < 0)
                {
                    currentlyProducing = false;
                    for (int i = 0; i < output.Length; i++)
                    {
                        storage.Add(output[i], outputCount[i]);
                    }
                }
            }
        }

        public override bool CheckRessourcen()
        {
            for (int i = 0; i < input.Length; ++i)
            {
                if (!storage.Check(input[i], inputCount[i]))
                    return false;
            }
            return true;
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

