using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{

    enum EPlayerMode
    {
        None,
        View,
        Build
    }




    class Player
    {

        EPlayerMode mode;
        EPlayerMode prevMode;

        ContentManager Content;

        public Player(ContentManager cont)
        {
            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);

            mode = EPlayerMode.View;
            prevMode = EPlayerMode.None;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        void HandlePlayerMode()
        {
            if(prevMode != mode)
            {
                GUIHandler.Instance.gui.plyMode = mode;

                switch (mode)
                {
                    case EPlayerMode.View:
                        break;
                    case EPlayerMode.Build:
                        GUIHandler.Instance.gui.SetMarkSize(new Point(16, 16));
                        break;
                    default:
                        break;
                }
            }

        }

        public void Update(GameTime gTime)
        {
            if (InputHandler.Instance.IsKeyPressedOnce(Keys.B))
            {
                mode = EPlayerMode.Build;
            }

            if (InputHandler.Instance.IsKeyPressedOnce(Keys.V))
            {
                mode = EPlayerMode.View;
            }

            switch (mode)
            {
                case EPlayerMode.Build:
                    break;
                default:
                    break;
            }
                HandlePlayerMode();
        }
    }


}
