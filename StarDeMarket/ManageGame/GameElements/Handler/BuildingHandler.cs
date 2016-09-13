
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
        public List<Building> buildingList;
        MainBuilding bMainBuilding;

        ContentManager cont;

        BuildingHandler()
        {
            buildingList = new List<Building>();

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
            foreach(Building b in buildingList)
            {
                b.Update(gTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            foreach(Building b in buildingList)
            {
                b.Draw(spriteBatch);
            }
        }
        public void SetContentManager(ContentManager _cont)
        {
            cont = new ContentManager(_cont.ServiceProvider, _cont.RootDirectory);
            buildingList.Add(new BWoodCutter(new Vector2(200, 400), cont));
            buildingList.Add(new BMill(new Vector2(400, 400), cont));
            buildingList.Add(new BSawmill(new Vector2(600, 400), cont));
            buildingList.Add(new BBaker(new Vector2(800, 600), cont));
            buildingList.Add(new BIronMelt(new Vector2(200, 600), cont));
            buildingList.Add(new BIronMine(new Vector2(400, 600), cont));
            buildingList.Add(new BCoalMine(new Vector2(600, 600), cont));
            buildingList.Add(new BFarm(new Vector2(800, 600), cont));
            buildingList.Add(new BBaker(new Vector2(400, 800), cont));
            buildingList.Add(new MainBuilding(new Vector2(200, 200), cont));
            

        }
    }
}
