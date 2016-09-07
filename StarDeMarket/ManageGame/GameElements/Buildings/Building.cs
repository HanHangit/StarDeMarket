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
    public abstract class Building
    {
        public abstract void Draw(SpriteBatch spriteBatch);                //Zeichnet
        public abstract void Update(GameTime gTime);
        public abstract void Workerwork();          //Erstellt Instanz vom Arbeiter und weist ihnen die Arbeit zu
        public abstract bool HasFullWorkforce();

        protected ContentManager cont;
        protected List<HWorker> listWorker; 
        protected Texture2D texture2D;
        protected Vector2 position;
        protected int costs;
        protected int output;
        protected int person;


    }
}
