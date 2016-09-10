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
        None,
        Grass,
        Water,
        Rock,
        Tree,
        Count
    }



    class Tile
    {
        public static Color[] tileColor =
        {
            new Color(),
            new Color(34,177,76),
            new Color(63,72,204),
            new Color(127,127,127),
            new Color (181,230,29 ),

        };

        public static Texture2D[] tileText;

        public static Color[][] tileColorData;

        public Storage lager;

        public Color[] color { get; private set; }

        public Rectangle bounds { get; private set; }

        public string name { get; private set; }

        bool walkable;

        Vector2 pos;

        public Tile(ETile type, Vector2 position, int tilesize)
        {
            lager = new Storage();
            pos = position;
            color = new Color[tilesize * tilesize];
            color = tileColorData[(int)type];
            bounds = new Rectangle((int)position.X, (int)position.Y, tilesize, tilesize);

            switch (type)
            {
                case ETile.Water:
                    lager.Add(EItem.Fisch, 1000);
                    name = "Water";
                    walkable = false;
                    break;
                case ETile.Rock:
                    lager.Add(EItem.Eisen, 100);
                    lager.Add(EItem.Kohle, 100);
                    lager.Add(EItem.Stein, 300);
                    name = "Rock";
                    walkable = true;
                    break;
                case ETile.Grass:
                    name = "Grass";
                    walkable = true;
                    break;
                case ETile.Tree:
                    name = "Tree";
                    lager.Add(EItem.Holz, 1000);
                    walkable = false;
                    break;
                default:
                    name = "NA";
                    walkable = false;
                    break;
            }
        }

        public void Update(GameTime gTime)
        {

        }

        public bool Walkable()
        {
            return walkable;
        }

        public override string ToString()
        {
            return name;
        }


    }
}
