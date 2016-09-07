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
    class B_WoodCutter : OnlyOutput
    {
        Storage storage;
        EItem[] output = { EItem.Holz };
        int[] outputCount = { 2 };


        public B_WoodCutter(Vector2 _pos)
        {
            position = _pos;
            storage = new Storage();
        }
        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Workerwork()
        {
            throw new NotImplementedException();
        }
    }
}
