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
    class PlayState : IGameState
    {

        ContentManager cont;


        public PlayState(ContentManager _cont)
        {
            cont = _cont;

        }

        public void Initialize()
        {
            BuildingHandler.Instance.SetContentManager(cont); // Does need to be the first thing to initialize!!!
            BuildingHandler.Instance.map = new Tilemap(cont.Load<Texture2D>("Map/Basic Map"), cont);

            GUIHandler.Instance.gui = new GUI(cont);
        }

        public void LoadContent()
        {

        }

        public void UnloadContent()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BuildingHandler.Instance.Draw(spriteBatch);
            //BuildingHandler.Instance.map.Draw(spriteBatch);

            GUIHandler.Instance.gui.Draw(spriteBatch);
        }

        public EGameState Update(GameTime gTime)
        {
            GUIHandler.Instance.gui.Update(gTime);
            BuildingHandler.Instance.Update(gTime);
            //BuildingHandler.Instance.map.Update(gTime);

            return EGameState.PlayState;
        }
    }
}
