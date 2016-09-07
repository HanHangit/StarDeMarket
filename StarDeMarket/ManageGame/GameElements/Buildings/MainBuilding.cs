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
        List<HNonWorker> avaiableWorker;

        public MainBuilding(Vector2 _position, ContentManager _cont)
        {
            position = _position;
            texture2D = _cont.Load<Texture2D>("Building/Woodcutter01");
            avaiableWorker = new List<StarDeMarket.HNonWorker>(10);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Workerwork()
        {
            throw new NotImplementedException();
        }
    }
}
