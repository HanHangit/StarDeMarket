
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

        public Tilemap map;
        static BuildingHandler instance;
        public List<OnlyOutput> InpBuilding;
        B_WoodCutter b_WoodCutter;

        BuildingHandler()
        {
            InpBuilding = new List<OnlyOutput>();
            b_WoodCutter = new B_WoodCutter(new Vector2(200,200));
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

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }


    }
}
