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

        public Color[] color { get; private set; }

        public Rectangle bounds { get; private set; }

        ContentManager cont;

        Vector2 pos;

        public Tile(ETile type, Vector2 position, ContentManager cont)
        {
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            lager = new SSystem();
            pos = position;
            color = new Color[32 * 32];
            bounds = new Rectangle((int)position.X, (int)position.Y, 32, 32);

            switch (type)
            {
                case ETile.Water:
                    lager.Add(EItem.Holz, 1000);
                    cont.Load<Texture2D>("Tile/Water01").GetData(color);
                    break;
                case ETile.Rock:
                    lager.Add(EItem.Holz, 1000);
                    cont.Load<Texture2D>("Tile/Rock01").GetData(color);
                    break;
                case ETile.Grass:
                    lager.Add(EItem.Holz, 1000);
                    cont.Load<Texture2D>("Tile/Grass01").GetData(color);
                    break;
                default:
                    break;
            }
        }

        public void Update(GameTime gTime)
        {

        }


    }
}
