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

        public MainBuilding(Vector2 _position, ContentManager cont)
        {
            position = _position;
            name = "MainBuilding";
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/Main");

            storage.Add(EItem.Holz, 30);
            storage.Add(EItem.Bretter, 30);


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
            if(listWorker.Count < 10)
                listWorker.Add(new Human(position, 5, cont.Load<Texture2D>("Human/BasicHuman"), this));
        }

        public override void Workerwork()
        {
        }
    }
}
