using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    public abstract class BuildingWithInput : Building
    {
        public abstract void Production();              //Beginnt mit der Produktion
        public abstract Boolean CheckRessourcen();      //Überprüft ob alle Ressourcen vorhanden sind.

    }
}
