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

    class BCoalMine : OnlyOutput
    {
        
        Storage storage;
        EItem[] output = { EItem.Kohle };
        int[] outputCount = { 2 };
        //ToDo: Fischer
        HWoodcutter tom;

        public BCoalMine(Vector2 _pos, ContentManager _cont)
        {
            cont = _cont;
            listWorker = new List<HWorker>();
            texture2D = cont.Load<Texture2D>("Building/CoalMine");
            position = _pos;
            storage = new Storage();
            name = "CoalMine";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width,texture2D.Height)), Color.White);
            if (tom != null)
                tom.Draw(spriteBatch);
        }

        public override void Update(GameTime gTime)
        {
            if(tom != null)
                tom.Update(gTime);
        }

        public override void Workerwork()
        {
            if (HasFullWorkforce())
                Console.WriteLine("Bergmann Kohle fehlt!");
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
