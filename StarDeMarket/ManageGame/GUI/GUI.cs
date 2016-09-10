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

        Texture2D yellowMarkTile;
        Texture2D redMarkTile;

        Building debugBuild;

        public Rectangle markBounds;

        public EPlayerMode plyMode;

        public GUI(ContentManager cont)
        {
            markBounds = new Rectangle(0, 0, 0, 0);

            plyMode = EPlayerMode.View;

            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);

            fpsFont = Content.Load<SpriteFont>("Font/FPSFont");



            int tilesize = BuildingHandler.Instance.map.tilesize;

            SetMarkSize(new Point(tilesize, tilesize));

        }



        public void Update(GameTime gTime)
        {
            fps = (int)(1000f / (float)gTime.ElapsedGameTime.Milliseconds);



            markBounds.Location = BuildingHandler.Instance.map.GetTile(CameraHandler.Instance.screenCamera.position.ToPoint() + Mouse.GetState().Position).bounds.Location;





        }

        public void SetBuilding(Building target)
        {
            debugBuild = target;
        }

        /// <summary>
        /// Setzt die Größe des Auswahlkästchen
        /// </summary>
        /// <param name="size">Größe des Kasten</param>
        public void SetMarkSize(Point size)
        {

            markBounds.Size = size;

            yellowMarkTile = new Texture2D(Graphics.graph.GraphicsDevice, size.X, size.Y);

            redMarkTile = new Texture2D(Graphics.graph.GraphicsDevice, size.X, size.Y);


            Color[] yellMarkColor = new Color[size.X * size.Y];
            Color yellowMark = Color.Yellow;

            Color[] redMarkColor = new Color[size.X * size.Y];
            Color redMark = Color.Red;

            for (int i = 0; i < yellMarkColor.Length; ++i)
            {
                if (i < size.X)
                {
                    yellMarkColor[i] = yellowMark;
                    redMarkColor[i] = redMark;
                }
                if (i % size.X == 0)
                {
                    yellMarkColor[i] = yellowMark;
                    redMarkColor[i] = redMark;
                }
                if (i % size.X == size.X - 1)
                {
                    yellMarkColor[i] = yellowMark;
                    redMarkColor[i] = redMark;
                }
                if (i / size.X == size.Y - 1)
                {
                    yellMarkColor[i] = yellowMark;
                    redMarkColor[i] = redMark;
                }
            }

            yellowMarkTile.SetData(yellMarkColor);
            redMarkTile.SetData(redMarkColor);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (plyMode == EPlayerMode.Build)
            {


                spriteBatch.DrawString(fpsFont, "Press 'V' to go to View Mode. ", CameraHandler.Instance.screenCamera.position + new Vector2(10, 55), Color.Black);


                spriteBatch.DrawString(fpsFont, debugBuild.ToString(), CameraHandler.Instance.screenCamera.position + new Vector2(10, 80), Color.Black);
            }
            else if (plyMode == EPlayerMode.View)
            {
                spriteBatch.DrawString(fpsFont, "Press 'B' to Build. ", CameraHandler.Instance.screenCamera.position + new Vector2(10, 55), Color.Black);

                spriteBatch.DrawString(fpsFont, BuildingHandler.Instance.map.GetTile(markBounds.Location).ToString(), CameraHandler.Instance.screenCamera.position + new Vector2(10, 80), Color.Black);
            }

            //Der Marker wird gezeichnet
            if (BuildingHandler.Instance.map.Buildable(markBounds))
                spriteBatch.Draw(yellowMarkTile, markBounds.Location.ToVector2(), Color.White);
            else
                spriteBatch.Draw(redMarkTile, markBounds.Location.ToVector2(), Color.White);

            //Die FPS Anzeoge
            spriteBatch.DrawString(fpsFont, "FPS: " + fps, CameraHandler.Instance.screenCamera.position + new Vector2(10, 10), Color.Black);

            //DIe Position des Mauszeiger
            spriteBatch.DrawString(fpsFont, "Position: " + BuildingHandler.Instance.map.GetTile(CameraHandler.Instance.screenCamera.position.ToPoint() + Mouse.GetState().Position).bounds.Location.ToVector2(), CameraHandler.Instance.screenCamera.position + new Vector2(10, 30), Color.Black);
        }
    }
}
