using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class Gebäude
    {
        SSystem lager;

        EItem[] input = { EItem.Getreide, EItem.Holz, EItem.Kohle };
        int[] inputCount = { 1, 2, 4 };

        EItem[] output = { EItem.Getreide };
        int[] outputCount = { 6 };


        public Gebäude()
        {
            lager = new SSystem();
        }

        public bool checkItem()
        {
            for (int i = 0; i < input.Length; ++i)
            {
                if (!lager.Check(input[i], inputCount[i]))
                    return false;
            }
            return true;
        }

        public void GetOutput()
        {
            for (int i = 0; i < input.Length; ++i)
            {
                lager.Get(input[i], inputCount[i]);
            }

            for (int i = 0; i < output.Length; ++i)
            {
                lager.Add(output[i], outputCount[i]);
            }
        }
    }
}
