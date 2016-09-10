using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class ProduceRessource : Task
    {
        public ProduceRessource(Human _human)
        {
            hasSubTask = false;
            human = _human;
        }

        
    }
}
