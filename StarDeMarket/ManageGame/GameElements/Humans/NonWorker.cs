using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarDeMarket
{
    class NonWorker : Human
    {
        
        public NonWorker(Vector2 _position, EGender _gender)
        {
            position = _position;
            gender = _gender;
            speed = 2f;
        }

        public void Update(GameTime gTime)
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
