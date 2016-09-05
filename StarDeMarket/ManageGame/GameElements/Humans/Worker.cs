using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarDeMarket
{
    public abstract class Worker : Human
    {
        SSystem personalStorage;

        virtual public void Update(GameTime gTime)
        {
            throw new NotImplementedException();
        }

        virtual public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
