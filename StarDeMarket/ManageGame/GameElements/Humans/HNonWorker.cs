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
    class HNonWorker : Human
    {
        Vector2 target;

        public HNonWorker(Vector2 _position, EGender _gender, Texture2D _texture)
        {
            position = _position;
            gender = _gender;
            speed = 2f;
            texture = _texture;
            SetTarget(new Vector2(200, 200));
        }

        public void SetTarget(Vector2 _target)
        {
            target = _target;
        }

        void MoveToTarget()
        {
            if (position == target)
            {
                Random rand = new Random(System.DateTime.Now.Millisecond);
                SetTarget(new Vector2(rand.Next(0, 1000), rand.Next(0, 1000)));
            }
            Vector2 move = target - position;
            if (Math.Abs(move.X) > speed || Math.Abs(move.Y)>speed)
            {
                move.Normalize();
                move *= speed;
            }
            position += move;
        }

        public void Update(GameTime gTime)
        {
            MoveToTarget();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, 32, 64), Color.White);
        }
    }
}
