using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class Human
    {

        public Vector2 position { get; protected set; }
        public float speed { get; protected set; }
        public float workSpeed { get; protected set; }
        public int carry;
        Texture2D texture;
        Building building;
        public Task currTask;
        Point target;
        public Storage storage;

        public Point Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }



        public Human(Vector2 pos, float spd, Texture2D text, Building build)
        {
            storage = new Storage();
            position = pos;
            speed = spd;
            texture = text;
            building = build;
            workSpeed = 1;
            carry = 3;
        }

        public void SetBuilding(Building build)
        {
            building = build;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public bool MoveToTarget(GameTime gTime)
        {

            Vector2 normMove = (target.ToVector2() - position);

            if (normMove.Length() < speed)
            {
                position += normMove;
                return true;
            }
            else
            {
                normMove.Normalize();
                normMove *= speed * (float)gTime.ElapsedGameTime.TotalMilliseconds / 20f;
                position += normMove;
                return false;
            }
        }

        public void Update(GameTime gTime)
        {
            if (currTask != null)
            {
                if (currTask.DoTask(gTime))
                {
                    currTask = null;
                }
            }
            else
            {
                currTask = building.GetTask(this);                
            }

        }
    }
}
