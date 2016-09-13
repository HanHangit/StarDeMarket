using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using StarDeMarket;

namespace StarDeMarket
{
    //Enums for Buildings
    enum EBuilding
    {
        MainBuilding,
        Mill,
        FishingHut,
        Stonemason,
        Woodcutter,
        Count
    }

    abstract class Building
    {

        public abstract void Draw(SpriteBatch spriteBatch);                //Zeichnet
        public abstract void Update(GameTime gTime);
        public abstract void Workerwork();          //Erstellt Instanz vom Arbeiter und weist ihnen die Arbeit zu
        public abstract bool HasFullWorkforce();


        public override string ToString()
        {
            return name;
        }

        //Größe des Gebäudes
        public Rectangle Bounds
        {
            get
            {
                if (bounds.Width == 0 || bounds.Height == 0)
                {
                    bounds = new Rectangle((int)position.X, (int)position.Y, texture2D.Width, texture2D.Height);
                }

                return bounds;
            }
            set
            {
                bounds = value;
            }
        }

        public Storage Storage
        {
            get
            {
                return storage;
            }

            set
            {
            }
        }



        public HanHanTask GetTask(MyHuman human)
        {

            if (taskQueue.Count == 0)
                return null;
            else
            {
                HanHanTask t = taskQueue.Dequeue();
                t.SetHuman(human);
                return t;
            }

        }

        protected Queue<HanHanTask> taskQueue = new Queue<HanHanTask>();

        protected Rectangle bounds;
        protected ContentManager cont;
        protected List<MyHuman> listWorker;
        protected Texture2D texture2D;
        protected Vector2 position;
        protected int costs;
        protected int output;
        protected int person;
        protected string name;

        protected Storage storage = new Storage();


    }
}
