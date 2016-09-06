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

        public Rectangle bounds { get; private set; }

        public int tilesize = 32;

        Tile[,] tileMap;

        Color[] miniMap;

        public Texture2D textMap { get; private set; }
        Texture2D textMiniMap;
        Texture2D miniMapBackground;

        int miniMapScale = 20;
        Point miniMapSize;

        ContentManager Content;

        public Tilemap(Texture2D map, ContentManager cont)
        {
            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);

            bounds = new Rectangle(new Point(0, 0), new Point(map.Width * tilesize, map.Height * tilesize));

            miniMapBackground = cont.Load<Texture2D>("Map/MiniMapBack");

            BuildMap(map);
            BuildMiniMap();

        }

        public void BuildMiniMap()
        {

            miniMap = new Color[bounds.Width / miniMapScale * bounds.Height / miniMapScale];

            miniMapSize = new Point(bounds.Width / miniMapScale, bounds.Height / miniMapScale);

            textMiniMap = new Texture2D(Graphics.graph.GraphicsDevice, bounds.Width / miniMapScale, bounds.Height / miniMapScale);

            for (int i = 0; i < miniMapSize.X; ++i)
                for (int j = 0; j < miniMapSize.Y; ++j)
                {
                    miniMap[i + j * miniMapSize.X] = tileMap[i * miniMapScale / tilesize, j * miniMapScale / tilesize].color[16 * 16];
                }

            textMiniMap.SetData(miniMap);

        }

        public void BuildMiniMap(Rectangle rect, Color[] color)
        {

            rect = new Rectangle(rect.X / miniMapScale, rect.Y / miniMapScale, rect.Width / miniMapScale + 1, rect.Height / miniMapScale + 1);

            Color[] miniColor = new Color[color.Length / miniMapScale];

            for (int i = 0; i < miniColor.Length; ++i)
            {
                miniColor[i] = color[i * (color.Length / miniColor.Length)];
            }

            textMiniMap.SetData(0, rect, color, 0, rect.Width * rect.Height);

        }

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

        public void BuildMap(Rectangle rect, Color[] color)
        {
            textMap.SetData(0, rect, color, 0, rect.Width * rect.Height);
            BuildMiniMap(rect, color);
        }

        public void Update(GameTime gTime)
        {

            if (InputHandler.Instance.IsKeyPressedOnce(Keys.Space))
            {
                tileMap[10, 10] = new Tile(ETile.Water, new Vector2(10 * tilesize, 10 * tilesize), Content);
                BuildMap(tileMap[10, 10].bounds, tileMap[10, 10].color);
                tileMap[11, 11] = new Tile(ETile.Water, new Vector2(11 * tilesize, 11 * tilesize), Content);
                BuildMap(tileMap[11, 11].bounds, tileMap[11, 11].color);
                tileMap[12, 12] = new Tile(ETile.Water, new Vector2(12 * tilesize, 12 * tilesize), Content);
                BuildMap(tileMap[12, 12].bounds, tileMap[12, 12].color);
                tileMap[13, 13] = new Tile(ETile.Water, new Vector2(13 * tilesize, 13 * tilesize), Content);
                BuildMap(tileMap[13, 13].bounds, tileMap[13, 13].color);
            }

            for (int i = 0; i < tileMap.GetLength(0); ++i)
                for (int j = 0; j < tileMap.GetLength(1); ++j)
                    tileMap[i, j].Update(gTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int offset = 15;

            Rectangle miniMapRect = new Rectangle(new Point(CameraHandler.Instance.camera.view.X + 1050, CameraHandler.Instance.camera.view.Y + 30), miniMapSize);

            Rectangle miniMapOffset = new Rectangle(miniMapRect.Location - new Point(offset, offset), miniMapRect.Size + new Point(2 * offset, 2 * offset));


            spriteBatch.Draw(textMap, CameraHandler.Instance.camera.view, CameraHandler.Instance.camera.view, Color.White);

            spriteBatch.Draw(miniMapBackground, miniMapOffset, Color.White);

            spriteBatch.Draw(textMiniMap, miniMapRect, Color.White);

        }


    }
}
