using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarDeMarket.ManageGame.GameElements.Handler
{
    class Road
    {

        Rectangle bounds;
        Texture2D text;

        public Road(Point location, Texture2D _text)
        {
            bounds = new Rectangle(location, new Point(BuildingHandler.Instance.map.tilesize, BuildingHandler.Instance.map.tilesize));

            text = _text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(text, bounds, Color.White);
        }

        public void Workerwork()
        {
        }
    }
}
