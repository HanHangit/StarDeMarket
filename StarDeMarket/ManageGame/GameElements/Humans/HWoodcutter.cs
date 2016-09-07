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
        Vector2 target;

        public HWoodcutter(Vector2 _position, EGender _gender)
        {
            position = _position;
            gender = _gender;
            speed = 2f;
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
            if (Math.Abs(move.X) > speed || Math.Abs(move.Y) > speed)
            {
                move.Normalize();
                move *= speed;
            }
            position += move;
        }

        public override void Update(GameTime gTime)
        {
            MoveToTarget();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, 32, 64), Color.White);
        }
    }
}
