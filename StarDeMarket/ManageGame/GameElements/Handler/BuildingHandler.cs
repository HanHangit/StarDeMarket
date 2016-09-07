
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;namespace StarDeMarket
{
    class BuildingHandler
    {

        public Tilemap map;
        static BuildingHandler instance;
        public List<OnlyOutput> InpBuilding;
        BWoodCutter b_WoodCutter;

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
            b_WoodCutter.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            b_WoodCutter.Draw(spriteBatch);

        }
        public void SetContentManager(ContentManager _cont)
        {
            Console.WriteLine("SetContentManager");
            cont = _cont;
            b_WoodCutter = new BWoodCutter(new Vector2(200, 200), cont);
        }
    }
}
