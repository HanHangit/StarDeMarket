using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using StarDeMarket;
using Priority_Queue;

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
        Sawmill,
        Baker,
        Ironmine,
        Ironmelt,
        Coalmine,
        Farm,
        Herbalist,
        Count
    }

    abstract class Building
    {
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Human h in listWorker)
                h.Draw(spriteBatch);
        }
        public virtual void Update(GameTime gTime)
        {

                foreach (Human h in listWorker)
                    h.Update(gTime);


                if (listWorker.Count < 2)
                {
                    BuildingHandler.Instance.buildingList.Find(b => b is MainBuilding && b.GetWorker(this, 1));
                }


        }
        public abstract void Workerwork();          //Erstellt Instanz vom Arbeiter und weist ihnen die Arbeit zu
        public abstract bool HasFullWorkforce();



        public override string ToString()
        {
            return name;
        }


        public bool GetWorker(Building build, int amount)
        {
            if (listWorker.Count >= amount)
            {
                for (int i = 0; i < amount; ++i)
                {
                    Console.WriteLine("Worker Tranferiert");
                    build.EmployHuman(listWorker[0]);
                    listWorker[0].SetBuilding(build);
                    listWorker.RemoveAt(0);
                }
                return true;
            }
            return false;
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
                bounds = new Rectangle(value.X, value.Y, value.Width, value.Height);
                position = bounds.Location.ToVector2();
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



        public Task GetTask(Human human)
        {
            
            if (taskQueue.Count == 0)
                return null;
            else
            {
                Task t = taskQueue.Dequeue();
                t.SetHuman(human);
                if (!(storage.Check(EItem.Fisch) || t is GetFood))
                    t = null;
                if(!(t is GetFood))
                    storage.Get(EItem.Fisch);
                return t;
            }

        }

        public void EmployHuman(Human human)
        {
            listWorker.Add(human);
        }

        protected SimplePriorityQueue<Task> taskQueue = new SimplePriorityQueue<Task>();

        protected Rectangle bounds;
        protected ContentManager cont;
        protected List<Human> listWorker = new List<Human>();
        protected Texture2D texture2D;
        protected Vector2 position;
        protected int costs;
        protected int person;
        protected string name;
        protected EItem[] constrRessource;
        protected int[] amountRessource;
        public bool finished = false;

        public EItem[] ConstrRessource
        {
            get
            {
                if (constrRessource == null)
                {
                    constrRessource = new EItem[2];
                    constrRessource[0] = EItem.Holz;
                    constrRessource[1] = EItem.Bretter;
                }
                    return constrRessource;
            }
        }

        public int[] AmountRessource
        {
            get
            {
                if(amountRessource == null)
                {
                    amountRessource = new int[2];
                    amountRessource[0] = 5;
                    amountRessource[1] = 4;
                }
                return amountRessource;
            }
        }

        protected EItem[] input;
        protected int[] inputCount;

        protected EItem[] foodInput;
        protected int[] foodCount;

        protected EItem[] output;
        protected int[] outputCount;

        protected Storage storage = new Storage();


    }
}
