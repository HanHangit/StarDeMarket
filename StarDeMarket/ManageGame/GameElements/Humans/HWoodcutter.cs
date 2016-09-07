using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarDeMarket
{
    public class HWoodcutter : HWorker
    {
        public HWoodcutter(Vector2 _position, EGender _gender)
        {
            position = _position;
            gender = _gender;
            speed = 2f;
        }

        public override void Update(GameTime gTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
