
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
                buildingList[i].Update(gTime);

            for (int i = 0; i < constructionList.Count; ++i)
            {
                if (constructionList[i].finished)
                    constructionList.RemoveAt(i--);
                else
                    constructionList[i].Update(gTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);

            for (int i = 0; i < buildingList.Count; ++i)
                buildingList[i].Draw(spriteBatch);

            for (int i = 0; i < constructionList.Count; ++i)
            {
                if (constructionList[i].finished)
                    constructionList.RemoveAt(i--);
                else
                    constructionList[i].Draw(spriteBatch);
            }
        }
        public void SetContentManager(ContentManager _cont)
        {
            cont = new ContentManager(_cont.ServiceProvider, _cont.RootDirectory);
        }
    }
}
