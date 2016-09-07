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


        public BWoodCutter(Vector2 _pos, ContentManager cont)
        {

            Console.WriteLine("Bin hier drin i LOVE SFML!");
            texture2D = cont.Load<Texture2D>("Building/Woodcutter01");
            position = _pos;
            storage = new Storage();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width,texture2D.Height)), Color.White);
        }

        public override void Update()
        {

        }

        public override void Workerwork()
        {
            throw new NotImplementedException();
        }
    }
}
