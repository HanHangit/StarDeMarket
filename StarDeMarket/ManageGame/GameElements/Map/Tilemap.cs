using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class Tilemap
    {

        Vector2 size;
        int tilesize = 32;
        Tile[,] tileMap;

        public Tilemap(Texture2D map, ContentManager cont)
        {
            size = new Vector2(map.Width, map.Height);


            Color[] color = new Color[map.Width * map.Height];
            map.GetData(color);

            tileMap = new Tile[map.Width, map.Height];

            for (int i = 0; i < size.X; ++i)
                for (int j = 0; j < size.Y; ++j)
                {

                    if (color[i + j * map.Width].Equals(new Color(127, 127, 127)))
                    tileMap[i,j] = new Tile(ETile.Rock, new Vector2(i * tilesize, j * tilesize), cont);
                else if (color[i + j * map.Width].Equals(new Color(0, 162, 232)))
                    tileMap[i,j] = new Tile(ETile.Water, new Vector2(i * tilesize, j * tilesize), cont);
                else if (color[i + j * map.Width].Equals(new Color(34, 177, 76)))
                    tileMap[i,j] = new Tile(ETile.Grass, new Vector2(i * tilesize, j * tilesize), cont);
                else
                    tileMap[i,j] = new Tile(ETile.Grass, new Vector2(i * tilesize, j * tilesize), cont);

            }

        }

        public void Update(GameTime gTime)
        {
            for (int i = 0; i < size.X; ++i)
                for (int j = 0; j < size.Y; ++j)
                    tileMap[i, j].Update(gTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < size.X; ++i)
                for (int j = 0; j < size.Y; ++j)
                    tileMap[i, j].Draw(spriteBatch);
        }


    }
}
