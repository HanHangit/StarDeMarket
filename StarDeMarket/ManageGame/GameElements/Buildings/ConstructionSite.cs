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
    class ConstructionSite : Building
    {

        EItem[] buildRessources;
        int[] amount;

        bool builded;

        Building building;

        public ConstructionSite(Building build, ContentManager cont)
        {
            builded = false;
            position = build.Bounds.Location.ToVector2();
            building = build;
            bounds = build.Bounds;
            texture2D = cont.Load<Texture2D>("Building/ConstructionSite");
            finished = false;
            buildRessources = build.ConstrRessource;
            amount = build.AmountRessource;
            storage = new Storage();
            name = building.ToString() + "Under Construction";
        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);
            if (taskQueue.Count == 0)
            {

                for (int i = 0; i < amount.Length; ++i)
                {
                    if (amount[i] > storage.getCount(buildRessources[i]))
                        taskQueue.Enqueue(new FromStorageTask(this, buildRessources[i], amount[i] - storage.getCount(buildRessources[i])),1);
                }
            }

            finished = true;

            for (int i = 0; i < amount.Length; ++i)
            {
                if (amount[i] > storage.getCount(buildRessources[i]))
                    finished = false;
            }

            if (finished && !builded)
            {
                builded = true;
                BuildingHandler.Instance.map.Build(bounds, building);
                Building home = BuildingHandler.Instance.buildingList.Find(b => b is BHome);
                for (int i = 0; i < listWorker.Count; ++i)
                    home.EmployHuman(listWorker[i]);
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(texture2D, Bounds, Color.White);
        }
    }
}
