﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace StarDeMarket
{

    class BFarm : OnlyOutput
    {
        
        Storage storage;
        EItem[] output = { EItem.Getreide };
        int[] outputCount = { 2 };

        public BFarm(Vector2 _pos, ContentManager cont)
        {
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/Farm");
            position = _pos;
            storage = new Storage();
            name = "Farm";
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
                Console.WriteLine("Farme fehlt!");
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
