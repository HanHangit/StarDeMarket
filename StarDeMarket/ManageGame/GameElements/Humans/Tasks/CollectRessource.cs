using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class CollectRessource : Task
    {
        public CollectRessource(Human _human)
        {
            hasSubTask = false;
            human = _human;
        }
    }
}
