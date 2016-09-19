using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{

    //Struct für die Waren, mit Type und Anzahl
    struct Good
    {

        public Good(EItem _type, int _amount)
        {
            type = _type;
            amount = _amount;
        }

        public int Get(int count)
        {
            if(amount < count)
            {
                int h = amount;
                amount = 0;
                return h;
            }

            amount -= count;

            return count;
        }

        public bool IsEmpty()
        {
            if (amount > 0)
                return false;
            else
                return true;
        }

        public EItem type;
        public int amount;
    }

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
        Gold,
        Pilze,
        Count,
        None
    }

    class Storage

    {
        EItem[] goods =
        {
        EItem.Holz,
        EItem.Bretter,
        EItem.Stein,
        EItem.Getreide,
        EItem.Mehl,
        EItem.Brot,
        EItem.Fisch,
        EItem.Kohle,
        EItem.Eisen,
        EItem.Eisenbarren,
        EItem.Gold,
        EItem.Pilze
        };

        //Array für alle Essbaren Waren
        EItem[] foodGoods =
        {
            EItem.Fisch,
            EItem.Brot,
            EItem.Pilze
        };

        Good[] system = new Good[(int)EItem.Count];

        public Storage()
        {
            for (int i = 0; i < system.Length; ++i)
            {
                system[i] = new Good(goods[i], 0);
            }
        }

        /// <summary>
        /// Gibt die gesamte Anzahl des verfügbaren Essen zurück
        /// </summary>
        /// <returns></returns>
        public int FoodCount()
        {
            int h = 0;

            for (int i = 0; i < foodGoods.Length; ++i)
            {
                h += system[(int)foodGoods[i]].amount;
            }

            return h;
        }

        /// <summary>
        /// Gibt True zurück, wenn Essen vorhanden ist.
        /// </summary>
        /// <returns></returns>
        public bool CheckFood()
        {
            for (int i = 0; i < foodGoods.Length; ++i)
            {
                if (!system[(int)foodGoods[i]].IsEmpty())
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Nimmt das Essen aus dem Lagern und gibt es als Good zurück
        /// </summary>
        /// <param name="count">Anzahl wieviel rausgenommen werden soll</param>
        /// <returns></returns>
        public Good GetFood(int count)
        {
            for (int i = 0; i < foodGoods.Length; ++i)
                if (!system[(int)foodGoods[i]].IsEmpty())
                {
                    int h = system[(int)foodGoods[i]].Get(count);
                    return new Good(foodGoods[i], h);
                }
            return new Good(EItem.Fisch,0);
        }

        public bool IsEmpty()
        {
            for (int i = 0; i < system.Length; ++i)
                if (system[i].amount != 0)
                    return false;

            return true;
        }

        public int Get(EItem item, int value)
        {
            if (Check(item, value))
            {
                system[(int)item].amount -= value;
                return value;
            }
            else
            {
                int h = system[(int)item].amount;
                system[(int)item].amount = 0;
                return h;
            }
        }

        public int Get(EItem item)
        {
            if (Check(item, 1))
            {
                system[(int)item].amount -= 1;
                return 1;
            }
            else
                return 0;
        }

        public bool Check(EItem item, int value)
        {
            if (system[(int)item].amount >= value)
                return true;
            else
                return false;
        }

        public bool Check(EItem item)
        {
            if (system[(int)item].amount >= 1)
                return true;
            else
                return false;
        }

        public int getCount(EItem item)
        {
            return system[(int)item].amount;
        }

        public void Add(Good good)
        {
            system[(int)good.type].amount += good.amount;
        }

        public void Add(EItem item)
        {
            system[(int)item].amount += 1;
        }



        public void Add(EItem item, int count)
        {
            system[(int)item].amount += count;
        }



        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < system.Length; ++i)
            {
                if (system[i].amount > 0)
                    str += ((EItem)i).ToString() + ": " + system[i].amount + "\n";
            }

            return str;
        }

    }
}
