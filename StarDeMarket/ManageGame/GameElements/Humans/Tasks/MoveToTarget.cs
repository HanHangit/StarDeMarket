using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    class MoveToTarget : Task
    {
        Vector2 target;

        public MoveToTarget(Human _human, Vector2 _target)
        {
            hasSubTask = false;
            human = _human;
            target = _target;
        }

        public override Vector2 GetTargetVector()
        {
            return target;
        }
    }
}
