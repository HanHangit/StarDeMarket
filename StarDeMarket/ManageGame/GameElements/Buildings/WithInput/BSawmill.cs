using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace StarDeMarket
{
    class BSawmill : BuildingWithInput
    {

        public BSawmill(Vector2 _pos, ContentManager cont)
        {
            foodWatch = new Stopwatch();
            foodWatch.Start();
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/Sawmill");
            storage = new Storage();
            position = _pos;
            name = "Sawmill";
            productionTime = 2;

            input = new EItem[] { EItem.Holz};
            inputCount = new int[] { 2 };

            foodInput = new EItem[] { EItem.Fisch, EItem.Brot };
            //foodCount = new int[] { 4, 4 };

            output = new EItem[] { EItem.Bretter };
            outputCount = new int[] { 1};  
        }

        public override void Update(GameTime gTime)
        {
            //ToDo Chris, Human dürfen noch Ressourcen abbauen!
            if (foodWatch.ElapsedMilliseconds / 1000 <= 10)
            {
<<<<<<< HEAD
                Production(gTime);
                base.Update(gTime);
            }
            if (foodWatch.ElapsedMilliseconds / 1000 >= 10)
            {
                ConsumeFood();

            }
            if (taskQueue.Count == 0)
                {
                    if (storage.getCount(EItem.Fisch) < 1)
                    {
                        taskQueue.Enqueue(new FromStorageTask(this, EItem.Fisch, 3));
                    }
                    if (storage.getCount(EItem.Holz) < 3)
                    {
                        taskQueue.Enqueue(new FromStorageTask(this, EItem.Holz, 5));
                    }
                    if (storage.getCount(EItem.Bretter) > 5)
                    {
                        taskQueue.Enqueue(new ToStorageTask(this, EItem.Bretter, 5));
                    }
=======
                if(storage.getCount(EItem.Holz) < 3)
                {
                    taskQueue.Enqueue(new FromStorageTask(this, EItem.Holz, 5),2);
                }
                if(storage.getCount(EItem.Bretter) > 5)
                {
                    taskQueue.Enqueue(new ToStorageTask(this, EItem.Bretter, 5),1);
>>>>>>> c76752e869a334d170fa90b776faa38fef3ee21c
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

        public override bool HasFullWorkforce()
        {
            if (listWorker.Count == 2)
                return true;
            else
                return false;
        }
    }
}

