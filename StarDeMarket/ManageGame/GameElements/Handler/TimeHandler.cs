using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    enum ESeason
    {
        spring,
        Summer,
        Fall,
        Winter
    }


    class TimeHandler
    {

        private static TimeHandler instance;
        public DateTime date { get; private set; }
        ESeason season;

        private TimeHandler()
        {
            date = new DateTime(1, 4, 1);
            season = ESeason.spring;
        }

        public static TimeHandler Instance
        {
            get
            {
                if (instance == null)
                    instance = new TimeHandler();

                return instance;
            }
        }

        public void Update(GameTime gTime)
        {
            date = date.AddHours(gTime.ElapsedGameTime.TotalSeconds * 4);

        }

        public string DateString()
        {
            return date.ToShortDateString();
        }

    }
}
