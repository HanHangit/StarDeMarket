using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StarDeMarket
{
    enum ETask
    {
        MoveToTarget
    }

    class Task
    {
        public bool hasSubTask { get; protected set; }
        protected Human human;
        public List<Task> subTaskList;

        Task GetTaskFromEnum(ETask _task, Human human)
        {
            switch(_task)
            {
                case ETask.MoveToTarget:
                    return new Task();

                default:
                    return new Task();
            }
        }

        public virtual Vector2 GetTargetVector()
        {
            throw new Exception("DoesNotHaveATarget");
        }

        public virtual void ExecuteSpecialTask()
        {
            throw new Exception("DoesNotHaveASpecialTask");
        }
    }
}
