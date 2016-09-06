using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class GUIHandler
    {

        public GUI gui;
        static GUIHandler instance;

        GUIHandler()
        {

        }

        public static GUIHandler Instance
        {
            get
            {
                if (instance == null)
                    instance = new GUIHandler();

                return instance;
            }
        }
    }
}
