using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class BuildingHandler
    {

        public Camera camera;
        public Tilemap map;
        static BuildingHandler instance;
        public KeyboardState prevState;

        BuildingHandler()
        {
            
        }

        public static BuildingHandler Instance
        {
            get
            {
                if (instance == null)
                    instance = new BuildingHandler();

                return instance;
            }
        }


    }
}
