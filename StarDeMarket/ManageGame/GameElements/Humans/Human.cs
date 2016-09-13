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
    abstract class Human
    {
        public enum EGender
        {
            Male,
            Female
        }

        public Vector2 position { get; protected set; }
        public EGender gender { get; protected set; }
        public float speed { get; protected set; }
        public Building building { get; protected set; }

        public List<Task> taskList;
        public ContentManager cont;
        protected Texture2D texture;

        public void splitFirstTask()
        {
            if((taskList != null) && (taskList.Count>0))
            {
                if(taskList[0].hasSubTask)
                {
                    Task buffer = taskList[0];
                    for(int i = buffer.subTaskList.Count-1; i >= 0; i--)
                    {
                        taskList.Insert(0, buffer.subTaskList[i]);
                    }
                    if (taskList[0].hasSubTask)
                    {
                        splitFirstTask();
                    }
                }
            }
        }
    }
}
