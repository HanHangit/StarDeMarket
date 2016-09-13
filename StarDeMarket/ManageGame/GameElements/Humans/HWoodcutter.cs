﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace StarDeMarket
{
    public class HWoodcutter : HWorker
    {
        Vector2 target;

        public HWoodcutter(Vector2 _position, EGender _gender, ContentManager _cont)
        {
            position = _position;
            gender = _gender;
            speed = 1f;
            cont = _cont;
            texture = cont.Load<Texture2D>("Human/Woodcutter");
        }

        public HWoodcutter(Human human, Building _building)
        {
            
            position = human.position;
            gender = human.gender;
            speed = human.speed;
            taskList = human.taskList;
            cont = human.cont;
            texture = cont.Load<Texture2D>("Human/Woodcutter");
            building = _building;
        }

        public void SetTarget(Vector2 _target)
        {
            target = _target;
        }

        void MoveToTarget(GameTime gTime)
        {
            if (position == target)
            {
                taskList.RemoveAt(0);
            }
            Vector2 move = target - position;
            float curSpeed = speed * (float)gTime.ElapsedGameTime.TotalMilliseconds / 20;
            if (Math.Abs(move.X) > curSpeed || Math.Abs(move.Y) > curSpeed)
            {
                move.Normalize();
                move *= curSpeed;
            }

            if (move.Length() > 2f)
            {
            }
            position += move;
        }

        public override void Update(GameTime gTime)
        {
            if(taskList != null && taskList.Count > 0)
            {
                splitFirstTask();
                if(taskList[0] is MoveToTarget)
                {
                    SetTarget(taskList[0].GetTargetVector());
                    MoveToTarget(gTime);
                }
                if(taskList[0] is PickUpNewJob)
                {
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, 32, 64), Color.White);
        }
    }
}
