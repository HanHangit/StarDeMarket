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
    class BMill : BuildingWithInput
    {
        Storage storage;

        EItem[] input = { EItem.Getreide};
        int[] inputCount = { 2};

        EItem[] output = { EItem.Mehl };
        int[] outputCount = { 1 };
        public BMill(Vector2 _pos, ContentManager cont)
        {
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/Mill");
            storage = new Storage();
            position = _pos;
            name = "Mill";
        }

        public override void Update(GameTime gTime)
        {
            Production(gTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width, texture2D.Height)), Color.White);
        }

        public override void Workerwork()
        {
            if (HasFullWorkforce())
                Console.WriteLine("Ein Mueller fehlt hier! HILFE!@Matthis");
        }

        public override void Production(GameTime gTime)
        {
            if (CheckRessourcen())
            {
                for (int i = 0; i < input.Length; ++i)
                {
                    storage.Get(input[i], inputCount[i]);
                }

                for (int i = 0; i < output.Length; ++i)
                {
                    storage.Add(output[i], outputCount[i]);
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
