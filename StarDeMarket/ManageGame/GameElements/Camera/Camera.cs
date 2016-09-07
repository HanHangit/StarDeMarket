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
    class Camera
    {

        public Viewport viewport { get; private set; }

        public Vector2 position;
        public Vector2 origin;

        public Rectangle view;

        ContentManager Content;

        public float scale { get; private set; }

        public Camera(Viewport _viewport, ContentManager cont)
        {
            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);

            scale = 1;
            viewport = _viewport;
            position = new Vector2(200, 200);
            origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);

        }

        public void Reset()
        {
            position = new Vector2(0, 0);
            scale = 1;
            origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public Matrix GetViewMatrix()
        {

            if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && position.Y > 0)
                position.Y -= 5;

            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && position.Y + viewport.Height < BuildingHandler.Instance.map.bounds.Height)
                position.Y += 5;
            if ((Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) && position.X > 0)
                position.X -= 5;
            if ((Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) && position.X + viewport.Width < BuildingHandler.Instance.map.bounds.Width)
                position.X += 5;

            view = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(viewport.Width, viewport.Height));

            //view = viewport.Bounds;

            return Matrix.CreateScale(scale)
                * Matrix.CreateTranslation(new Vector3(-position, 1));
        }

    }
}
