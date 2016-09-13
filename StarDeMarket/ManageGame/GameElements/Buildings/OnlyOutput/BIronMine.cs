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

    class BIronMine : OnlyOutput
    {
        
        Storage storage;
        EItem[] output = { EItem.Eisen };
        int[] outputCount = { 2 };

        public BIronMine(Vector2 _pos, ContentManager _cont)
        {
            cont = _cont;
            texture2D = cont.Load<Texture2D>("Building/IronMine");
            position = _pos;
            storage = new Storage();
            name = "IronMine";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width,texture2D.Height)), Color.White);
        }

        public override void Update(GameTime gTime)
        {
        }

        public override void Workerwork()
        {
            if (HasFullWorkforce())
                Console.WriteLine("Bergmann Eisen fehlt!");
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
