using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{



    abstract class Task
    {

        public Task(Building building)
        {
            build = building;
        }

        protected Human human;
        protected Building build;

        public abstract bool DoTask(GameTime gTime);
        public void SetHuman(Human hum)
        {
            human = hum;
        }



    }
}
