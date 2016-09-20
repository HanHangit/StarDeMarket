using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class BStorage : Building
    {

        public BStorage(Vector2 _position, ContentManager cont)
        {
            position = _position;
            name = "Lager";
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/Storage");
            storage = new Storage();

            storage.Add(EItem.Holz, 100);
            storage.Add(EItem.Bretter, 100);

            storage.Add(EItem.Fisch, 100);

            maxWorker = 0;



        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, position, Color.White);
            base.Draw(spriteBatch);
        }
    }
}
