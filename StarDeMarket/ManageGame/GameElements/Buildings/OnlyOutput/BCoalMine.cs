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

    class BCoalMine : BuildingWithInput
    {
        public BCoalMine(Vector2 _pos, ContentManager cont)
        {
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/CoalMine");

            position = _pos;
            storage = new Storage();
            name = "CoalMine";

            output = new EItem[] { EItem.Kohle };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), new Point(texture2D.Width, texture2D.Height)), Color.White);
        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);

            if (taskQueue.Count == 0)
            {

                for (int i = 0; i < output.Length; ++i)
                {

                    if (storage.Check(output[i], 5))
                        taskQueue.Enqueue(new ToStorageTask(this, output[i], 5), 2);
                    else
                        taskQueue.Enqueue(new CollectTask(this, output[i]), 3);
                };
            }
        }

    }
}
