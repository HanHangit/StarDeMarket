﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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
        Coal,
        Gold,
        Iron,
        Count,
        GrowField


    }



    class Tile
    {
        public static Color[] tileColor =
        {
            new Color(),
            new Color(34,177,76), //Grass
            new Color(63,72,204), //Water
            new Color(127,127,127), //Rock
            new Color (181,230,29 ), //Tree
            new Color(),  //Road
            new Color(0,0,0), //Coal
            new Color(255,242,0), //Gold
            new Color(128,0,0), //Iron
            new Color()

        };

        public Building refBuilding { get; set; }

        public static Texture2D[][] tileText;

        public static Color[][][] tileColorData;

        public Storage storage;

        public Color[] color { get; private set; }

        public Rectangle bounds { get; private set; }

        public string name { get; set; }

        bool buildable;

        bool walkable;

        bool workable;

        float workTime;

        public int collectAmount;

        public ETile type;



        Vector2 pos;

        public Tile(ETile type, Vector2 position, int tilesize)
        {
            Random rnd = UsefullStuff.Instance.Random;
            this.type = type;
            walkable = false;
            refBuilding = null;
            storage = new Storage();
            pos = position;
            color = new Color[tilesize * tilesize];

            if (rnd.Next(101) > 90)
            {
                int numTile = rnd.Next(0, tileColorData[(int)type].Length);
                color = tileColorData[(int)type][numTile];
            }
            else
                color = tileColorData[(int)type][0];

            

            bounds = new Rectangle((int)position.X, (int)position.Y, tilesize, tilesize);

            switch (type)
            {
                case ETile.Water:
                    storage.Add(EItem.Fisch, 1000);
                    name = "Water";
                    buildable = false;
                    workable = true;
                    WorkTime = 1f;
                    collectAmount = 3;
                    break;
                case ETile.Rock:
                    name = "Rock";
                    storage.Add(EItem.Stein, 100);
                    buildable = true;
                    workable = true;
                    WorkTime = 10f;
                    collectAmount = 2;
                    break;
                case ETile.Grass:
                    name = "Grass";
                    buildable = true;
                    workable = true;
                    WorkTime = 5f;
                    collectAmount = 3;
                    break;
                case ETile.Tree:
                    name = "Tree";
                    storage.Add(EItem.Holz, 10);
                    if (rnd.Next(100) > 70)
                        storage.Add(EItem.Pilze, 5);
                    buildable = false;
                    workable = true;
                    WorkTime = 4f;
                    collectAmount = 2;
                    break;
                case ETile.Coal:
                    name = "Coal";
                    storage.Add(EItem.Kohle, 100);
                    buildable = false;
                    workable = true;
                    WorkTime = 15f;
                    collectAmount = 1;
                    break;
                case ETile.Iron:
                    name = "Iron";
                    storage.Add(EItem.Eisen, 100);
                    buildable = false;
                    workable = true;
                    WorkTime = 15f;
                    collectAmount = 1;
                    break;
                case ETile.Gold:
                    name = "Gold";
                    storage.Add(EItem.Gold, 100);
                    buildable = false;
                    workable = true;
                    WorkTime = 25f;
                    collectAmount = 1;
                    break;
                default:
                    name = "NA";
                    buildable = false;
                    workable = false;
                    break;
            }
        }

        public void Reset(ETile tile)
        {
            switch (tile)
            {
                case ETile.Water:
                    storage.Add(EItem.Fisch, 1000);
                    name = "Water";
                    buildable = false;
                    workable = true;
                    WorkTime = 6f;
                    break;
                case ETile.Rock:
                    name = "Rock";
                    storage.Add(EItem.Stein, 100);
                    buildable = true;
                    workable = true;
                    WorkTime = 10f;
                    break;
                case ETile.Grass:
                    name = "Grass";
                    buildable = true;
                    workable = true;
                    WorkTime = 5f;
                    break;
                case ETile.Tree:
                    name = "Tree";
                    storage.Add(EItem.Holz, 10);
                    buildable = false;
                    workable = true;
                    WorkTime = 4f;
                    break;
                case ETile.Coal:
                    name = "Coal";
                    storage.Add(EItem.Kohle, 100);
                    buildable = false;
                    workable = true;
                    WorkTime = 15f;
                    break;
                case ETile.Iron:
                    name = "Iron";
                    storage.Add(EItem.Eisen, 100);
                    buildable = false;
                    workable = true;
                    WorkTime = 15f;
                    break;
                case ETile.Gold:
                    name = "Gold";
                    storage.Add(EItem.Gold, 100);
                    buildable = false;
                    workable = true;
                    WorkTime = 25f;
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
            if (storage.IsEmpty())
            {
                color = tileColorData[(int)ETile.Grass][0];
                buildable = true;
                BuildingHandler.Instance.map.BuildMap(bounds.Location, color);
                type = ETile.Grass;
            }
        }

        public void BuildRoad()
        {
            Walkable = true;
            Buildable = false;
            workable = false;
            color = tileColorData[(int)ETile.Road][0];
            type = ETile.Road;
        }

        public float WorkTime
        {
            get
            {
                return workTime;
            }
            set
            {
                workTime = value;
            }
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
