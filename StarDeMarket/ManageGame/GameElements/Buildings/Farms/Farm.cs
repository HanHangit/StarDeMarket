using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarDeMarket
{
    abstract class Farm : Building
    {
        public List<GrowingField> fields = new List<GrowingField>();

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);
            for (int i = 0; i < fields.Count; ++i)
                if (fields[i].wasted)
                    fields.RemoveAt(i--);
                else
                    fields[i].Update(gTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            for (int i = 0; i < fields.Count; ++i)
                fields[i].Draw(spriteBatch);
        }

        public abstract void PlantField(Point plantPos);
    }
}
