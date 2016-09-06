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
    class GUI
    {

        ContentManager Content;

        SpriteFont fpsFont;

        int fps;

        Texture2D markTile;

        Vector2 markPosition;

        public GUI(ContentManager cont)
        {
            markPosition = new Vector2(0, 0);

            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);

            fpsFont = Content.Load<SpriteFont>("Font/FPSFont");

            markTile = new Texture2D(Graphics.graph.GraphicsDevice, BuildingHandler.Instance.map.tilesize, BuildingHandler.Instance.map.tilesize);

            int tilesize = BuildingHandler.Instance.map.tilesize;

            Color[] markColor = new Color[tilesize * tilesize];
            Color mark = Color.Yellow;

            for(int i = 0; i < markColor.Length; ++i)
            {
                if (i < tilesize)
                    markColor[i] = mark;
                if (i % tilesize == 0)
                    markColor[i] = mark;
                if (i % tilesize == tilesize - 1)
                    markColor[i] = mark;
                if (i / tilesize == tilesize - 1)
                    markColor[i] = mark;
            }

            markTile.SetData(markColor);
        }

       

        public void Update(GameTime gTime)
        {
            fps = (int)(1000f / gTime.ElapsedGameTime.Milliseconds);



            markPosition = BuildingHandler.Instance.map.GetTile(CameraHandler.Instance.camera.position.ToPoint() + Mouse.GetState().Position).bounds.Location.ToVector2();





        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(markTile, markPosition, Color.White);

            spriteBatch.DrawString(fpsFont, "FPS: " + fps, CameraHandler.Instance.camera.position + new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(fpsFont, "Position: " + BuildingHandler.Instance.map.GetTile(CameraHandler.Instance.camera.position.ToPoint() + Mouse.GetState().Position).bounds.Location.ToVector2(), CameraHandler.Instance.camera.position + new Vector2(10, 30), Color.Black);
        }
    }
}
