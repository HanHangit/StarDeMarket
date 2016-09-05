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

    enum ETile
    {
        Grass,
        Water,
        Rock
    }

    class Tile
    {

        public SSystem lager;

        Texture2D text;

        ContentManager cont;

        Vector2 pos;

        public Tile(ETile type, Vector2 position, ContentManager cont)
        {
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            lager = new SSystem();
            pos = position;

            switch (type)
            {
                case ETile.Water:
                    lager.Add(EItem.Holz, 1000);
                    text = cont.Load<Texture2D>("Tile/Water01");
                    break;
                case ETile.Rock:
                    lager.Add(EItem.Holz, 1000);
                    text = cont.Load<Texture2D>("Tile/Rock01");
                    break;
                case ETile.Grass:
                    lager.Add(EItem.Holz, 1000);
                    text = cont.Load<Texture2D>("Tile/Grass01");
                    break;
                default:
                    break;
            }
        }

        public void Update(GameTime gTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(text, pos, Color.White);
        }


    }
}
