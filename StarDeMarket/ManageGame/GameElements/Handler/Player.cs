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

            Vector2 position = CameraHandler.Instance.screenCamera.position;

            Viewport viewport = CameraHandler.Instance.screenCamera.viewport;

            if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && position.Y > 0)
                position.Y -= CameraHandler.Instance.screenCamera.speed * gTime.ElapsedGameTime.Milliseconds;

            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && position.Y + viewport.Height < BuildingHandler.Instance.map.bounds.Height)
                position.Y += CameraHandler.Instance.screenCamera.speed * gTime.ElapsedGameTime.Milliseconds;
            if ((Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) && position.X > 0)
                position.X -= CameraHandler.Instance.screenCamera.speed * gTime.ElapsedGameTime.Milliseconds;
            if ((Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) && position.X + viewport.Width < BuildingHandler.Instance.map.bounds.Width)
                position.X += CameraHandler.Instance.screenCamera.speed * gTime.ElapsedGameTime.Milliseconds;

            CameraHandler.Instance.screenCamera.position = position;


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
