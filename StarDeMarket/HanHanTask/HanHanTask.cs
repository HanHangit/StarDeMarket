using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{



    abstract class HanHanTask
    {

        public HanHanTask(Building building)
        {
            build = building;
        }

        protected MyHuman human;
        protected Building build;

        public abstract bool DoTask(GameTime gTime);
        public void SetHuman(MyHuman hum)
        {
            human = hum;
        }



    }
}
