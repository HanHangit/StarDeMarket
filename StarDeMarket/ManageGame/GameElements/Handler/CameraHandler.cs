using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class CameraHandler
    {

        public Camera camera;
        static CameraHandler instance;

        CameraHandler()
        {

        }

        public static CameraHandler Instance
        {
            get
            {
                if (instance == null)
                    instance = new CameraHandler();

                return instance;
            }
        }
    }
}
