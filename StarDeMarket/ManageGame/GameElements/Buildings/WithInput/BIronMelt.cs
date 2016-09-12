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
    class BIronMelt : BuildingWithInput
    {
        Storage storage;

        EItem[] input = { EItem.Eisen, EItem.Kohle };
        int[] inputCount = { 2 };

        EItem[] output = { EItem.Eisenbarren };
        int[] outputCount = { 1 };
        public BIronMelt(Vector2 _pos, ContentManager _cont)
        {
            storage = new Storage();
            cont = _cont;
            listWorker = new List<HWorker>();
            texture2D = cont.Load<Texture2D>("Building/IronMelt");
            position = _pos;
            name = "IronMelt";
        }

        public override void Update(GameTime gTime)
        {
            Production();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width, texture2D.Height)), Color.White);
        }

        public override void Workerwork()
        {
            if (HasFullWorkforce())
                Console.WriteLine("Ein Schmelzer fehlt hier! HILFE!@Matthis");
        }

        public override void Production()
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

        public override void EmployHuman(Human _human)
        {
            throw new NotImplementedException();
        }
    }
}

