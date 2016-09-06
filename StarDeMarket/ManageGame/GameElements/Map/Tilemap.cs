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
        public Texture2D textMap { get; private set; }
        Color[] miniMap;
        Texture2D textMiniMap;

        int miniMapScale = 20;
        Point miniMapSize;

        ContentManager Content;

        public Tilemap(Texture2D map, ContentManager cont)
        {
            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);

            bounds = new Rectangle(new Point(0, 0), new Point(map.Width * tilesize, map.Height * tilesize));


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
                    miniMap[i + j * miniMapSize.X] = tileMap[i * miniMapScale / tilesize , j * miniMapScale / tilesize ].color[16 * 16];
                }

            textMiniMap.SetData(miniMap);

        }

        public void BuildMiniMap(Rectangle rect, Color[] color)
        {

            for (int i = rect.X / tilesize; i < rect.Width / tilesize; ++i)
                for (int j = rect.Y / tilesize; j < rect.Height / tilesize; ++j)
                {
                    miniMap[i + j * miniMapSize.X] = tileMap[i * miniMapScale / tilesize, j * miniMapScale / tilesize].color[16 * 16];
                }

            textMiniMap.SetData(miniMap);

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
        }

        public void Update(GameTime gTime)
        {

            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            
            for (int i = 0; i < tileMap.GetLength(0); ++i)
                for (int j = 0; j < tileMap.GetLength(1); ++j)
                    tileMap[i, j].Update(gTime);
                   
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            

            spriteBatch.Draw(textMap, CameraHandler.Instance.camera.view, CameraHandler.Instance.camera.view, Color.White);

            spriteBatch.Draw(textMiniMap, new Rectangle(new Point(CameraHandler.Instance.camera.view.X + 950, CameraHandler.Instance.camera.view.Y + 50), miniMapSize), Color.White);

        }


    }
}
