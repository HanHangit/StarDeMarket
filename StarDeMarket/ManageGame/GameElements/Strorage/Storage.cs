using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{

    enum EItem
    {
        Holz,
        Bretter,
        Stein,
        Getreide,
        Mehl,
        Brot,
        Fisch,
        Kohle,
        Eisen,
        Eisenbarren,
        Count
    }

    class Storage

    {

        int[] system = new int[(int)EItem.Count];

        public void Get(EItem item, int value)
        {
            system[(int)item] -= value;
        }
         


        public bool Check(EItem item, int value)
        {
            if (system[(int)item] >= value)
                return true;
            else
                return false;
        }



        public void Add(EItem item)
        {
            system[(int)item] += 1;
        }



        public void Add(EItem item, int count)
        {
            system[(int)item] += count;
        }



        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < system.Length; ++i)
                str += system[i] + "\n";

            return str;
        }

    }
}
