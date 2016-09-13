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

        public int Get(EItem item, int value)
        {
            if (Check(item, value))
            {
                system[(int)item] -= value;
                return value;
            }
            else
            {
                int h = system[(int)item];
                system[(int)item] = 0;
                return h;
            }
        }

        public int Get(EItem item)
        {
            if (Check(item, 1))
            {
                system[(int)item] -= 1;
                return 1;
            }
            else
                return 0;
        }

        public bool Check(EItem item, int value)
        {
            if (system[(int)item] >= value)
                return true;
            else
                return false;
        }

        public int getCount(EItem item)
        {
            return system[(int)item];
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
