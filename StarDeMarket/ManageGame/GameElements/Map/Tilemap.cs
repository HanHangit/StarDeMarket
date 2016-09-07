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
        public int tilesize = 16;

        //Die aufgeteilte TileMap
        Tile[,] tileMap;

        //Die Textur der Map, die am Ende gezeichnet wird
        public Texture2D textMap { get; private set; }

        //Die Textur des Hintergrunds der MiniMap
        Texture2D miniMapBackground;

        Texture2D miniMapCurrentView;

        //Die Größe der MiniMap
        Point miniMapSize;

        //ContentManager
        ContentManager Content;

        public Tilemap(string strMap, ContentManager cont)
        {
            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);

            //For faster loading
            Tile.tileText = new Texture2D[(int)ETile.Count];
            Tile.tileColorData = new Color[(int)ETile.Count][];
            Tile.tileColorData[0] = new Color[tilesize * tilesize];
            Tile.tileColorData[1] = new Color[tilesize * tilesize];
            Tile.tileColorData[2] = new Color[tilesize * tilesize];
            Tile.tileColorData[3] = new Color[tilesize * tilesize];
            Tile.tileColorData[4] = new Color[tilesize * tilesize];

            Tile.tileText[0] = Content.Load<Texture2D>("Tile/Grass01");
            Tile.tileText[1] = Content.Load<Texture2D>("Tile/Grass01");
            Tile.tileText[2] = Content.Load<Texture2D>("Tile/Water01");
            Tile.tileText[3] = Content.Load<Texture2D>("Tile/Rock01");
            Tile.tileText[4] = Content.Load<Texture2D>("Tile/Tree01");

            for (int i = 0; i < Tile.tileText.Length; ++i)
            {
                Tile.tileText[i].GetData(Tile.tileColorData[i]);
            }

            miniMapBackground = cont.Load<Texture2D>("Map/MiniMapBack");
            miniMapCurrentView = cont.Load<Texture2D>("Map/MapArea");
            miniMapSize = new Point(200, 200);

            font = Content.Load<SpriteFont>("Font/FPSFont");

            //Für Menschen mit zu wenig Grafikartenspeicher oder was auch immer zu wenig sein soll
            try
            {
                BuildMapFile(strMap);
            }
            catch(System.IO.FileNotFoundException)
            {
                BuildMap(strMap);
            }
                

        }


        public void BuildMapFile(string path)
        {

            //TODO: Hier der Pfad für die Text Datei
            string[] lines = System.IO.File.ReadAllLines(Environment.CurrentDirectory + "/" + path + ".txt");



            Viewport map = new Viewport(0, 0, int.Parse(lines[0]), int.Parse(lines[1]));

            Color[] color = new Color[int.Parse(lines[0]) * int.Parse(lines[1])];

            for (int i = 2; i < color.Length + 2; ++i)
            {

                string[] values = lines[i].Split(new[] { ',' });
                int r = int.Parse(values[0]);
                int g = int.Parse(values[1]);
                int b = int.Parse(values[2]);

                color[i - 2] = new Color(r, g, b);
            }


            bounds = new Rectangle(new Point(0, 0), new Point(map.Width * tilesize, map.Height * tilesize));

            textMap = new Texture2D(Graphics.graph.GraphicsDevice, map.Width * tilesize, map.Height * tilesize);


            tileMap = new Tile[map.Width, map.Height];

            for (int i = 0; i < map.Width; ++i)
                for (int j = 0; j < map.Height; ++j)
                {

                    if (color[i + j * map.Width].Equals(Tile.tileColor[(int)ETile.Rock]))
                        tileMap[i, j] = new Tile(ETile.Rock, new Vector2(i * tilesize, j * tilesize), tilesize);
                    else if (color[i + j * map.Width].Equals(Tile.tileColor[(int)ETile.Water]))
                        tileMap[i, j] = new Tile(ETile.Water, new Vector2(i * tilesize, j * tilesize), tilesize);
                    else if (color[i + j * map.Width].Equals(Tile.tileColor[(int)ETile.Grass]))
                        tileMap[i, j] = new Tile(ETile.Grass, new Vector2(i * tilesize, j * tilesize), tilesize);
                    else if (color[i + j * map.Width].Equals(Tile.tileColor[(int)ETile.Tree]))
                        tileMap[i, j] = new Tile(ETile.Tree, new Vector2(i * tilesize, j * tilesize), tilesize);
                    else
                        tileMap[i, j] = new Tile(ETile.Grass, new Vector2(i * tilesize, j * tilesize), tilesize);
                    if (j == 0)
                        Console.WriteLine("Finished " + i / (map.Width / 100) + "%");

                    textMap.SetData(0, new Rectangle(i * tilesize, j * tilesize, tilesize, tilesize), tileMap[i, j].color, 0, tilesize * tilesize);

                }
        }


        /// <summary>
        /// Baut die Map auf Grundlage einer BitMap auf.
        /// </summary>
        /// <param name="map">Die Bitmap</param>
        public void BuildMap(string strMap)
        {

            Texture2D map = Content.Load<Texture2D>("Map/" + strMap);

            bounds = new Rectangle(new Point(0, 0), new Point(map.Width * tilesize, map.Height * tilesize));

            textMap = new Texture2D(Graphics.graph.GraphicsDevice, map.Width * tilesize, map.Height * tilesize);

            Color[] color = new Color[map.Width * map.Height];
            map.GetData(color);


            tileMap = new Tile[map.Width, map.Height];

            for (int i = 0; i < map.Width; ++i)
                for (int j = 0; j < map.Height; ++j)
                {

                    if (color[i + j * map.Width].Equals(Tile.tileColor[(int)ETile.Rock]))
                        tileMap[i, j] = new Tile(ETile.Rock, new Vector2(i * tilesize, j * tilesize), tilesize);
                    else if (color[i + j * map.Width].Equals(Tile.tileColor[(int)ETile.Water]))
                        tileMap[i, j] = new Tile(ETile.Water, new Vector2(i * tilesize, j * tilesize), tilesize);
                    else if (color[i + j * map.Width].Equals(Tile.tileColor[(int)ETile.Grass]))
                        tileMap[i, j] = new Tile(ETile.Grass, new Vector2(i * tilesize, j * tilesize), tilesize);
                    else if (color[i + j * map.Width].Equals(Tile.tileColor[(int)ETile.Tree]))
                        tileMap[i, j] = new Tile(ETile.Tree, new Vector2(i * tilesize, j * tilesize), tilesize);
                    else
                        tileMap[i, j] = new Tile(ETile.Grass, new Vector2(i * tilesize, j * tilesize), tilesize);
                    if (j == 0)
                        Console.WriteLine("Finished " + i / (map.Width / 100) + "%");

                    textMap.SetData(0, new Rectangle(i * tilesize, j * tilesize, tilesize, tilesize), tileMap[i, j].color, 0, tilesize * tilesize);

                }
            //!System.IO.File.Exists(@Environment.CurrentDirectory + "/" + strMap + ".txt")
            if (true)
            {
                string[] lines = new string[2 + color.Length];
                lines[0] = map.Width.ToString();
                lines[1] = map.Height.ToString();


                for (int i = 2; i < color.Length + 2; ++i)
                {
                    lines[i] = color[i - 2].R + "," + color[i - 2].G + "," + color[i - 2].B;
                }

                System.IO.File.WriteAllLines(Environment.CurrentDirectory + "/" + strMap + ".txt", lines);
            }

            Console.WriteLine("Finished Complete Map");

        }

        /// <summary>
        /// Ändert die Textur der Map.
        /// </summary>
        /// <param name="rect"> Ein Bereich, welche seine "Farbe" geändert hat.</param>
        /// <param name="color"> Die neue Farbe</param>
        public void BuildMap(Rectangle rect, Color[] color)
        {

            for (int i = 0; i < color.Length; ++i)
            {
                if (color[i].A == 0)
                    color[i] = tileMap[(rect.X + i % rect.Width) / tilesize, (rect.Y + i / rect.Width) / tilesize].color[i % (tilesize * tilesize)];
            }

            textMap.SetData(0, rect, color, 0, rect.Width * rect.Height);
        }



        public Tile GetTile(Point position)
        {
            try
            {
                return tileMap[position.X / tilesize, position.Y / tilesize];
            }
            catch (IndexOutOfRangeException)
            {
                return tileMap[0, 0];
            }
        }

        public void Update(GameTime gTime)
        {

            for (int i = 0; i < tileMap.GetLength(0); ++i)
                for (int j = 0; j < tileMap.GetLength(1); ++j)
                    tileMap[i, j].Update(gTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int offset = 15;

            Rectangle miniMapRect = new Rectangle(new Point(CameraHandler.Instance.screenCamera.view.X + 1050, CameraHandler.Instance.screenCamera.view.Y + 30), miniMapSize);

            Rectangle miniMapOffset = new Rectangle(miniMapRect.Location - new Point(offset, offset), miniMapRect.Size + new Point(2 * offset, 2 * offset));


            Rectangle miniMapCameraRect = new Rectangle(miniMapRect.X + CameraHandler.Instance.screenCamera.view.X / (bounds.Width / miniMapSize.X), miniMapRect.Y + CameraHandler.Instance.screenCamera.view.Y / (bounds.Height / miniMapSize.Y), (miniMapRect.Width * 1280) / bounds.Width, (miniMapRect.Height * 720) / bounds.Height);

            //The Current View
            spriteBatch.Draw(textMap, CameraHandler.Instance.screenCamera.view, CameraHandler.Instance.screenCamera.view, Color.White);


            spriteBatch.Draw(miniMapBackground, miniMapOffset, Color.White);

            spriteBatch.Draw(textMap, miniMapRect, Color.White);

            spriteBatch.Draw(miniMapCurrentView, miniMapCameraRect, new Color(Color.White, 128));
        }


    }
}
