﻿using System;
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

        ContentManager Content;

        public PlayState(ContentManager cont)
        {
            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
        }


        public void Initialize()
        {
            BuildingHandler.Instance.map = new Tilemap(Content.Load<Texture2D>("Map/Basic Map"), Content);
        }

        public void LoadContent()
        {

        }

        public void UnloadContent()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BuildingHandler.Instance.map.Draw(spriteBatch);
        }

        public EGameState Update(GameTime gTime)
        {
            BuildingHandler.Instance.map.Update(gTime);
            return EGameState.PlayState;
        }
    }
}
