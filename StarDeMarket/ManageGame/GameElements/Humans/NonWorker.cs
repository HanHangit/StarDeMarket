using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace StarDeMarket
{
    class NonWorker : Human
    {
        ContentManager cont;

        public NonWorker(Vector2 _position, EGender _gender)
        {
            cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            position = _position;
            gender = _gender;
            speed = 2f;
            sprite = cont.Load<SpriteBatch>("Human/BasicHuman");
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
