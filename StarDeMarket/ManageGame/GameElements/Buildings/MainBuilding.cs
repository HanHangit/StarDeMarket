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
    class MainBuilding : Building
    {
        public List<HNonWorker> avaiableWorker;

        public MainBuilding(Vector2 _position, ContentManager _cont)
        {
            position = _position;
            cont = _cont;
            texture2D = cont.Load<Texture2D>("Building/Main");
            avaiableWorker = new List<StarDeMarket.HNonWorker>();
            for(int i = 0; i < 5; i++)
            {
                avaiableWorker.Add(new HNonWorker(position, Human.EGender.Male, cont, this));
            }
            listWorker = null;
            name = "MainBuilding";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), texture2D.Bounds.Size.ToVector2().ToPoint()), Color.White);
        }

        // should not yet be used
        public override bool HasFullWorkforce()
        {
            return true;
        }

        public override void Update(GameTime gTime)
        {
            if(avaiableWorker.Count > 0)
            {
                avaiableWorker[0].Update(gTime);
            }
        }

        public override void Workerwork()
        {

        }

        public override void EmployHuman(Human human)
        {
            throw new NotImplementedException();
        }
    }
}
