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
        MainBuilding mainBuilding;

        public HNonWorker(Vector2 _position, EGender _gender, ContentManager _cont, MainBuilding _mainBuilding)
        {
            position = _position;
            gender = _gender;
            speed = 2f;
            cont = _cont;
            texture = cont.Load<Texture2D>("Human/BasicHuman");
            SetTarget(new Vector2(200, 200));
            mainBuilding = _mainBuilding;
            building = mainBuilding;
            taskList = new List<Task>();
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
            Building _building = BuildingHandler.Instance.buildingList.Find(b => b is BWoodCutter && !b.HasFullWorkforce());
            if (_building != null)
            {
                Console.WriteLine("I am gettin a new Job");
                taskList.Add(new GetNewJob(_building, mainBuilding, this));
            }
            splitFirstTask();
            if (taskList != null && !(taskList.Count == 0))
            {
                if(taskList[0] is PickUpNewJob)
                {
                    
                    taskList[0].ExecuteSpecialTask();
                    taskList.RemoveAt(0);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, 32, 64), Color.White);
        }
    }
}
