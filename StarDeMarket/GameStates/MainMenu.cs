using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace StarDeMarket
{
    class MainMenu : IGameState
    {

        ContentManager Content;
        SpriteFont font;

        public MainMenu(ContentManager cont)
        {
            Content = new ContentManager(cont.ServiceProvider,cont.RootDirectory);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "MainMenu", new Vector2(200, 200), Color.Black);
        }

        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            font = Content.Load<SpriteFont>("Font/Title");   
        }

        public void UnloadContent()
        {
            
        }

        public EGameState Update(GameTime gTime)
        {

            KeyboardState state = Keyboard.GetState();

            if (InputHandler.Instance.IsKeyPressedOnce(Keys.Enter))
                return EGameState.PlayState;

            return EGameState.Mainmenu;


        }
    }
}
