
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
        public List<ConstructionSite> constructionList;

        ContentManager cont;

        BuildingHandler()
        {
            buildingList = new List<Building>();
            constructionList = new List<ConstructionSite>();
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

            for (int i = 0; i < buildingList.Count; ++i)
            {
                if (buildingList[i].finished)
                {
                    buildingList.RemoveAt(i--);
                }
                else
                    buildingList[i].Update(gTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);

            for (int i = 0; i < buildingList.Count; ++i)
            {
                if(buildingList[i].finished)
                {
                    buildingList.RemoveAt(i--);
                }
                else
                    buildingList[i].Draw(spriteBatch);
            }
            
        }
        public void SetContentManager(ContentManager _cont)
        {
            cont = new ContentManager(_cont.ServiceProvider, _cont.RootDirectory);
        }
    }
}
