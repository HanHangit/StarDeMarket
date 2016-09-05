using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
        enum EGameState
        {
            None = -1,
            Mainmenu,
            Map
        }

        interface IGameState
        {

            void Initialize();
            void LoadContent();
            void UnloadContent();
            void Draw(SpriteBatch spriteBatch);
            EGameState Update(GameTime gTime);

        }
}
