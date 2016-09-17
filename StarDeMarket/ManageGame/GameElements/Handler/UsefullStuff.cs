using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class UsefullStuff
    {

        private static UsefullStuff instance;
        public RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        private Random random;

        public static UsefullStuff Instance
        {
            get
            {
                if (instance == null)
                    instance = new UsefullStuff();
                return instance;
            }
        }

        public Random Random
        {
            get
            {
                if(random == null)
                    random = new Random(randomNumberGenerator.GetHashCode());
                return random;
            }
        }


    }
}
