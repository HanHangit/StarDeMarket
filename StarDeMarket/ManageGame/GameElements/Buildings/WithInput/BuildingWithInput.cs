using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace StarDeMarket
{
  
    abstract class BuildingWithInput : Building
    {
        protected bool currentlyProducing = false;
        public float productionCounter = 0, productionTime = 0;
        protected Stopwatch foodWatch;

        public bool Production(GameTime gTime)
        {

                if (CheckRessourcen() && !currentlyProducing)
                {
                    currentlyProducing = true;
                    for (int i = 0; i < input.Length; ++i)
                    {
                        storage.Get(input[i], inputCount[i]);
                    }
                    productionCounter = productionTime;
                }
                if (currentlyProducing)
                {
                    productionCounter -= (float)gTime.ElapsedGameTime.TotalSeconds;
                    if (productionCounter < 0)
                    {
                        currentlyProducing = false;
                        for (int i = 0; i < output.Length; i++)
                        {
                            storage.Add(output[i], outputCount[i]);
                        }
                    }
                }

            return currentlyProducing;

           
        }

        public bool CheckRessourcen()
        {
            for (int i = 0; i < input.Length; ++i)
            {
                if (!storage.Check(input[i], inputCount[i]))
                    return false;
            }
            return true;
        }
        public virtual void ConsumeFood()
        {
            if (storage.Check(EItem.Fisch) || storage.Check(EItem.Brot))
            {
                if (storage.getCount(EItem.Fisch) >= 1)
                    storage.Get(EItem.Fisch);
                else if (storage.getCount(EItem.Brot) >= 1)
                    storage.Get(EItem.Brot);
                foodWatch.Restart();
            }
        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);

        }

        //Überprüft ob alle Ressourcen vorhanden sind.


    }
}
