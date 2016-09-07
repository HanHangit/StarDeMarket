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

        public Point markPosition;

        public EPlayerMode plyMode;

        public GUI(ContentManager cont)
        {
            markPosition = new Point(0, 0);

            plyMode = EPlayerMode.View;

            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);

            fpsFont = Content.Load<SpriteFont>("Font/FPSFont");

            markTile = new Texture2D(Graphics.graph.GraphicsDevice, BuildingHandler.Instance.map.tilesize, BuildingHandler.Instance.map.tilesize);

            int tilesize = BuildingHandler.Instance.map.tilesize;

            Color[] markColor = new Color[tilesize * tilesize];
            Color mark = Color.Yellow;

            for (int i = 0; i < markColor.Length; ++i)
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



            markPosition = BuildingHandler.Instance.map.GetTile(CameraHandler.Instance.screenCamera.position.ToPoint() + Mouse.GetState().Position).bounds.Location;





        }

        /// <summary>
        /// Setzt die Größe des Auswahlkästchen
        /// </summary>
        /// <param name="size">Größe des Kasten</param>
        public void SetMarkSize(Point size)
        {
            markTile = new Texture2D(Graphics.graph.GraphicsDevice, size.X,size.Y);

            Color[] markColor = new Color[size.X * size.Y];
            Color mark = Color.Yellow;

            for (int i = 0; i < markColor.Length; ++i)
            {
                if (i < size.X)
                    markColor[i] = mark;
                if (i % size.X == 0)
                    markColor[i] = mark;
                if (i % size.X == size.X - 1)
                    markColor[i] = mark;
                if (i / size.X == size.X - 1)
                    markColor[i] = mark;
            }

            markTile.SetData(markColor);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (plyMode == EPlayerMode.Build)
            {
                spriteBatch.Draw(markTile, markPosition.ToVector2(), Color.White);

                spriteBatch.DrawString(fpsFont, "Press 'V' to go to View Mode. ", CameraHandler.Instance.screenCamera.position + new Vector2(10, 55), Color.Black);
            }
            else if(plyMode == EPlayerMode.View)
            {
                spriteBatch.DrawString(fpsFont, "Press 'B' to Build. ", CameraHandler.Instance.screenCamera.position + new Vector2(10, 55), Color.Black);
            }

            spriteBatch.DrawString(fpsFont, "FPS: " + fps, CameraHandler.Instance.screenCamera.position + new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(fpsFont, "Position: " + BuildingHandler.Instance.map.GetTile(CameraHandler.Instance.screenCamera.position.ToPoint() + Mouse.GetState().Position).bounds.Location.ToVector2(), CameraHandler.Instance.screenCamera.position + new Vector2(10, 30), Color.Black);
        }
    }
}
