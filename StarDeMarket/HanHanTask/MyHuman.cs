using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class MyHuman
    {

        public Vector2 position { get; protected set; }
        public float speed { get; protected set; }
        public float workSpeed { get; protected set; }
        Texture2D texture;
        Building building;
        public HanHanTask currTask;
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



        public MyHuman(Vector2 pos, float spd, Texture2D text, Building build)
        {
            storage = new Storage();
            position = pos;
            speed = spd;
            texture = text;
            building = build;
            workSpeed = 1;
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
                position += normMove * (float)gTime.ElapsedGameTime.TotalSeconds;
                return true;
            }
            else
            {
                normMove.Normalize();
                normMove *= speed;
                position += normMove;
                return false;
            }
        }

        public void Update(GameTime gTime)
        {

            if (currTask != null)
            {
                currTask.SetHuman(this);
                if (currTask.DoTask(gTime))
                {
                    Console.WriteLine("Finished Job");
                    Console.WriteLine(building.Storage.ToString());
                    currTask = null;
                }
            }
            else
            {
                Console.WriteLine("New Task");

                currTask = building.GetTask;                
            }

        }

    }
}
