using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class DropRessource : Task
    {
        public DropRessource(Human _human)
        {
            hasSubTask = false;
            human = _human;
        }
    }
}
