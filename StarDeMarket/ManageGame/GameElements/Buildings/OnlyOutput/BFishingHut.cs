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
        //ToDo: Fischer
        HWoodcutter tom;
        List<MyHuman> human;

        public BFishingHut(Vector2 _pos, ContentManager _cont)
        {
            cont = _cont;
            listWorker = new List<HWorker>();
            texture2D = cont.Load<Texture2D>("Building/Woodcutter01");
            position = _pos;
            storage = new Storage();
            name = "Fishing Hut";
            human = new List<MyHuman>();
            human.Add(new MyHuman(position,0.01f,cont.Load<Texture2D>("Human/BasicHuman"), this));
            human.Add(new MyHuman(position + new Vector2(32,32), 0.01f, cont.Load<Texture2D>("Human/BasicHuman"), this));
            taskQueue.Enqueue(new HanHanCollect(this, EItem.Holz));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width,texture2D.Height)), Color.White);

            foreach (MyHuman h in human)
                h.Draw(spriteBatch);

            if (tom != null)
                tom.Draw(spriteBatch);
        }

        public override void Update(GameTime gTime)
        {
            foreach (MyHuman h in human)
                h.Update(gTime);

            if(tom != null)
                tom.Update(gTime);

            if (taskQueue.Count == 0)
            {
                if(storage.Check(EItem.Holz,5))
                    taskQueue.Enqueue(new HanHanToStorage(this, EItem.Holz,5));
                else
                    taskQueue.Enqueue(new HanHanCollect(this, EItem.Holz));

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

        public override void EmployHuman(Human _human)
        {
            throw new NotImplementedException();
        }
    }
}
