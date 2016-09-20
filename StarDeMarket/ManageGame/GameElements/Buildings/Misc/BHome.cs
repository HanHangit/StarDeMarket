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
    class BHome : Building
    {

        public BHome(Vector2 _position, ContentManager cont)
        {
            position = _position;
            name = "Wohnhaus";
            this.cont = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
            texture2D = cont.Load<Texture2D>("Building/Main");

            for(int i = 0; i < 10; ++i)
            {
                listWorker.Add(new Human(position, 3, cont.Load<Texture2D>("Human/BasicHuman"),this));
            }


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Rectangle(position.ToPoint(), texture2D.Bounds.Size.ToVector2().ToPoint()), Color.White);
        }

        public override void Update(GameTime gTime)
        { 
            
        }

        public override string ToString()
        {
            return name + "\nAvailableWorker: " + listWorker.Count;
        }
    }
}
