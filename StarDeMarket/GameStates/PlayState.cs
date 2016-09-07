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
        NonWorker tom;

        //Class to control our "Player"
        Player player;

        public PlayState(ContentManager cont)
        {
            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);
        }


        public void Initialize()
        {
            //MapAuswahl
            BuildingHandler.Instance.map = new Tilemap(Content.Load<Texture2D>("Map/RealBigMap"), Content);
            //BuildingHandler.Instance.map = new Tilemap(Content.Load<Texture2D>("Map/Basic Map"), Content);
            tom = new NonWorker(new Vector2(1, 2), Human.EGender.Male, Content.Load<Texture2D>("Human/Hunter"));
            GUIHandler.Instance.gui = new GUI(Content);

            player = new Player(Content);
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
            tom.Draw(spriteBatch);
            GUIHandler.Instance.gui.Draw(spriteBatch);

            player.Draw(spriteBatch);
        }

        public EGameState Update(GameTime gTime)
        {
            player.Update(gTime);


            GUIHandler.Instance.gui.Update(gTime);

            BuildingHandler.Instance.map.Update(gTime);
            tom.Update(gTime);
            return EGameState.PlayState;
        }
    }
}
