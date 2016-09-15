using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class GrowingField
    {
        public bool finished { get; private set; }

        public bool wasted;

        Texture2D[] text;
        Texture2D currText;

        Point position;

        float growingTime;

        Tile targetTile;

        EItem finishedGoods;

        float timer;

        public GrowingField(Point position, Texture2D[] textures, float growTime, EItem type)
        {
            targetTile = null;
            this.position = position;
            finished = false;
            wasted = false;
            finishedGoods = type;
            growingTime = growTime;
            text = textures;
            timer = 0;
            currText = text[0];
        }


        public void Update(GameTime gTime)
        {
            if (targetTile == null)
            {
                timer += (float)gTime.ElapsedGameTime.TotalSeconds;
                if (timer > growingTime)
                {
                    targetTile = BuildingHandler.Instance.map.GetTile(position);
                    currText = text[1];
                    finished = true;
                    targetTile.WorkAble = true;
                    targetTile.storage.Add(finishedGoods, 3);
                    targetTile.WorkTime = 1f;
                }
            }
            else
            {
                if (!targetTile.storage.Check(finishedGoods))
                {
                    wasted = true;
                    targetTile.Reset(ETile.Grass);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currText, position.ToVector2(), Color.White);
        }

    }
}
