using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarDeMarket
{
    abstract class HWorker : Human
    {
        Storage personalStorage;

        virtual public void Update(GameTime gTime)
        {
            throw new NotImplementedException();
        }

        virtual public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
