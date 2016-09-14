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
        Road,
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
            new Color()

        };

        public Building refBuilding { get; set; }

        public static Texture2D[] tileText;

        public static Color[][] tileColorData;

        public Storage storage;

        public Color[] color { get; private set; }

        public Rectangle bounds { get; private set; }

        public string name { get; set; }

        bool buildable;

        bool walkable;

        bool workable;



        Vector2 pos;

        public Tile(ETile type, Vector2 position, int tilesize)
        {
            walkable = false;
            refBuilding = null;
            storage = new Storage();
            pos = position;
            color = new Color[tilesize * tilesize];
            color = tileColorData[(int)type];
            bounds = new Rectangle((int)position.X, (int)position.Y, tilesize, tilesize);

            switch (type)
            {
                case ETile.Water:
                    storage.Add(EItem.Fisch, 1000);
                    name = "Water";
                    buildable = false;
                    workable = true;
                    break;
                case ETile.Rock:
                    storage.Add(EItem.Eisen, 100);
                    storage.Add(EItem.Kohle, 100);
                    storage.Add(EItem.Stein, 300);
                    name = "Rock";
                    buildable = true;
                    workable = true;
                    break;
                case ETile.Grass:
                    name = "Grass";
                    buildable = true;
                    workable = true;
                    break;
                case ETile.Tree:
                    name = "Tree";
                    storage.Add(EItem.Holz, 10);
                    buildable = false;
                    workable = true;
                    break;
                default:
                    name = "NA";
                    buildable = false;
                    workable = false;
                    break;
            }
        }

        public void Update(GameTime gTime)
        {
            if(storage.IsEmpty())
            {
                color = tileColorData[(int)ETile.Grass];
                BuildingHandler.Instance.map.BuildMap(bounds.Location, color);
            }
        }

        public void BuildRoad()
        {
            Walkable = true;
            Buildable = false;
            workable = false;
            color = tileColorData[(int)ETile.Road];
        }

        public bool Walkable
        {
            get
            {
                return walkable;
            }
            private set
            {
                walkable = value;
            }
        }

        public bool Buildable
        {
            get
            {
                return buildable;
            }
            set
            {
                buildable = value;
            }
        }

        public bool WorkAble
        {
            get
            {
                return workable;
            }
            set
            {
                workable = value;
            }
        }

        public override string ToString()
        {
            if (refBuilding != null)
                return refBuilding.ToString();
            else
                return name;
        }


    }
}
