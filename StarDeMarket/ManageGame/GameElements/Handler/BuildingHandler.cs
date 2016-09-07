﻿
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
    class BuildingHandler
    {
        HNonWorker tom;
        public Tilemap map;
        static BuildingHandler instance;
        public List<OnlyOutput> InpBuilding;
        BFishingHut b_WoodCutter;
        BMill b_Mill;

        ContentManager cont;

        BuildingHandler()
        {
            InpBuilding = new List<OnlyOutput>();

        }

        public static BuildingHandler Instance
        {
            get
            {
                if (instance == null)
                    instance = new BuildingHandler();

                return instance;
            }
        }


        public void Update(GameTime gTime)
        {
            map.Update(gTime);
            b_WoodCutter.Update(gTime);
            b_Mill.Update(gTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            b_WoodCutter.Draw(spriteBatch);
            b_Mill.Draw(spriteBatch);

        }
        public void SetContentManager(ContentManager _cont)
        {
            cont = _cont;
            b_WoodCutter = new BFishingHut(new Vector2(200, 200), cont);
            b_Mill = new BMill(new Vector2(400, 400), cont);
        }
    }
}
