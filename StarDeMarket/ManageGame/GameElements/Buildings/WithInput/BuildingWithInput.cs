using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    abstract class BuildingWithInput : Building
    {
        protected bool currentlyProducing = false;
        protected float productionCounter = 0, productionTime = 0;

        public void Production(GameTime gTime)
        {
            if (EnoughFood())
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
            }
           
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
                                                 //Überprüft ob alle Ressourcen vorhanden sind.

        public bool EnoughFood() //ToDo Chris
        {
            for (int i = 0; i < input.Length; ++i)
            {
                if (!storage.Check(input[i], inputCount[i]))
                    return false;
            }
            return true;
        }
    }
}
