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

        protected EStatus status;

        public string name { get; protected set; }

        public abstract bool DoTask(GameTime gTime);
        public void SetHuman(Human hum)
        {
            human = hum;
        }



    }
}
