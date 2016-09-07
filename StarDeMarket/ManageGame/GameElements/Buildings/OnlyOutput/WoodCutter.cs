using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class WoodCutter : OnlyOutput
    {
        Storage storage;
        EItem[] output = { EItem.Holz };
        int[] outputCount = { 2 };


        public WoodCutter()
        {
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
