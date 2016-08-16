using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{

    enum ETile
    {
        Land,
        Baum,
        Erz,
        Stein,
        Wasser
    }

    class Tile
    {

        public Lager lager;



        public Tile(ETile type)
        {
            lager = new Lager();

            switch (type)
            {
                case ETile.Wasser:
                    lager.Add(EItem.Holz, 1000);
                    break;
                default:
                    break;
            }
        }


    }
}
