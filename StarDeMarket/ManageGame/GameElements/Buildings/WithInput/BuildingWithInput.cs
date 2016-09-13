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
        public abstract void Production(GameTime gTime);              //Beginnt mit der Produktion
        public abstract Boolean CheckRessourcen();      //Überprüft ob alle Ressourcen vorhanden sind.

    }
}
