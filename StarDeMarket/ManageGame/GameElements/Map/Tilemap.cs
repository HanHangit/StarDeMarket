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
    class Tilemap
    {

        //SchriftArt
        SpriteFont font;

        //Die Position und Größe der Karte
        public Rectangle bounds { get; private set; }

        //Tilesize
        public int tilesize = 32;

        //Die aufgeteilte TileMap
        Tile[,] tileMap;
        
        //Die Textur der Map, die am Ende gezeichnet wird
        public Texture2D textMap { get; private set; }

        

        //Die Textur der MiniMap
        Texture2D textMiniMap;

        //Die Textur des Hintergrunds der MiniMap
        Texture2D miniMapBackground;

        Texture2D miniMapCurrentView;

        //Der Scale der MiniMap
        int miniMapScale = 20;

        //Die Größe der MiniMap
        Point miniMapSize;

        //ContentManager
        ContentManager Content;

        public Tilemap(Texture2D map, ContentManager cont)
        {
            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);

            bounds = new Rectangle(new Point(0, 0), new Point(map.Width * tilesize, map.Height * tilesize));

            miniMapBackground = cont.Load<Texture2D>("Map/MiniMapBack");
            miniMapCurrentView = cont.Load<Texture2D>("Map/MapArea");
            miniMapSize = new Point(bounds.Width / miniMapScale, bounds.Height / miniMapScale);

            font = Content.Load<SpriteFont>("Font/FPSFont");

            BuildMap(map);
        }


        /// <summary>
        /// Baut die Map auf Grundlage einer BitMap auf.
        /// </summary>
        /// <param name="map">Die Bitmap</param>
        public void BuildMap(Texture2D map)
        {

            textMap = new Texture2D(Graphics.graph.GraphicsDevice, map.Width * tilesize, map.Height * tilesize);

            Color[] color = new Color[map.Width * map.Height];
            map.GetData(color);

            tileMap = new Tile[map.Width, map.Height];

            for (int i = 0; i < map.Width; ++i)
                for (int j = 0; j < map.Height; ++j)
                {

                    if (color[i + j * map.Width].Equals(new Color(127, 127, 127)))
                        tileMap[i, j] = new Tile(ETile.Rock, new Vector2(i * tilesize, j * tilesize), Content);
                    else if (color[i + j * map.Width].Equals(new Color(0, 162, 232)))
                        tileMap[i, j] = new Tile(ETile.Water, new Vector2(i * tilesize, j * tilesize), Content);
                    else if (color[i + j * map.Width].Equals(new Color(34, 177, 76)))
                        tileMap[i, j] = new Tile(ETile.Grass, new Vector2(i * tilesize, j * tilesize), Content);
                    else
                        tileMap[i, j] = new Tile(ETile.Grass, new Vector2(i * tilesize, j * tilesize), Content);

                    textMap.SetData(0, new Rectangle(i * tilesize, j * tilesize, tilesize, tilesize), tileMap[i, j].color, 0, tilesize * tilesize);

                }

        }

        /// <summary>
        /// Ändert die Textur der Map.
        /// </summary>
        /// <param name="rect"> Ein Bereich, welche seine "Farbe" geändert hat.</param>
        /// <param name="color"> Die neue Farbe</param>
        public void BuildMap(Rectangle rect, Color[] color)
        {
            textMap.SetData(0, rect, color, 0, rect.Width * rect.Height);
        }


        public Tile GetTile(Point position)
        {
            try {
                return tileMap[position.X / tilesize, position.Y / tilesize];
            }
            catch(IndexOutOfRangeException)
            {
                return tileMap[0, 0];
            }
        }

        public void Update(GameTime gTime)
        {

            if (InputHandler.Instance.IsKeyPressedOnce(Keys.Space))
            {
            }

            for (int i = 0; i < tileMap.GetLength(0); ++i)
                for (int j = 0; j < tileMap.GetLength(1); ++j)
                    tileMap[i, j].Update(gTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int offset = 15;

            Rectangle miniMapRect = new Rectangle(new Point(CameraHandler.Instance.screenCamera.view.X + 1050, CameraHandler.Instance.screenCamera.view.Y + 30), miniMapSize);

            Rectangle miniMapOffset = new Rectangle(miniMapRect.Location - new Point(offset, offset), miniMapRect.Size + new Point(2 * offset, 2 * offset));

            //TODO: may need to be reworked in case of a new Scale
            Rectangle miniMapCameraRect = new Rectangle(miniMapRect.X + CameraHandler.Instance.screenCamera.view.X/miniMapScale, miniMapRect.Y + CameraHandler.Instance.screenCamera.view.Y/miniMapScale, (miniMapRect.Width*1280)/bounds.Width, (miniMapRect.Height*720)/bounds.Height);

            //The Current View
            spriteBatch.Draw(textMap, CameraHandler.Instance.screenCamera.view, CameraHandler.Instance.screenCamera.view, Color.White);

            
            spriteBatch.Draw(miniMapBackground, miniMapOffset, Color.White);

            spriteBatch.Draw(textMap, miniMapRect, Color.White);

            spriteBatch.Draw(miniMapCurrentView, miniMapCameraRect, new Color(Color.White, 128));
        }


    }
}
