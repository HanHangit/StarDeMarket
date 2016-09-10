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
        public int tilesize = 8;

        //Muss ein Vielfaches der TileSize sein
        public int splitSize = 256;

        //Die aufgeteilte TileMap
        Tile[,] tileMap;

        //Die Textur der Map, die am Ende gezeichnet wird
        //public Texture2D textMap { get; private set; }

        //Die Textur des Hintergrunds der MiniMap
        Texture2D miniMapBackground;

        Texture2D miniMapCurrentView;

        Texture2D[,] textSplitMap;

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
            Tile.tileColorData[5] = new Color[tilesize * tilesize];

            Tile.tileText[0] = Content.Load<Texture2D>("Tile/Grass01");
            Tile.tileText[1] = Content.Load<Texture2D>("Tile/Grass01");
            Tile.tileText[2] = Content.Load<Texture2D>("Tile/Water01");
            Tile.tileText[3] = Content.Load<Texture2D>("Tile/Rock01");
            Tile.tileText[4] = Content.Load<Texture2D>("Tile/Tree01");
            Tile.tileText[5] = Content.Load<Texture2D>("Tile/road01");

            for (int i = 0; i < Tile.tileText.Length; ++i)
            {
                Tile.tileText[i].GetData(Tile.tileColorData[i]);
            }

            miniMapBackground = cont.Load<Texture2D>("Map/MiniMapBack");
            miniMapCurrentView = cont.Load<Texture2D>("Map/MapArea");
            miniMapSize = new Point(200, 200);

            font = Content.Load<SpriteFont>("Font/FPSFont");

            BuildMap(strMap);



        }


        /// <summary>
        /// Baut die Map auf Grundlage einer BitMap auf.
        /// </summary>
        /// <param name="map">Die Bitmap</param>
        public void BuildMap(string strMap)
        {

            Texture2D map = Content.Load<Texture2D>("Map/" + strMap);

            bounds = new Rectangle(new Point(0, 0), new Point(map.Width * tilesize, map.Height * tilesize));

            //textMap = new Texture2D(Graphics.graph.GraphicsDevice, map.Width * tilesize, map.Height * tilesize);

            textSplitMap = new Texture2D[(bounds.Width / splitSize) + 1, (bounds.Height / splitSize) + 1];

            for (int i = 0; i < textSplitMap.GetLength(0); ++i)
                for (int j = 0; j < textSplitMap.GetLength(1); ++j)
                    textSplitMap[i, j] = new Texture2D(Graphics.graph.GraphicsDevice, splitSize, splitSize);

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
                        Console.WriteLine("Finished " + (j + i * map.Width) / ((map.Width * map.Height) / 100) + "%");

                    int r = i * tilesize / splitSize;
                    int c = j * tilesize / splitSize;

                    textSplitMap[r, c].SetData(0, new Rectangle(i * tilesize % splitSize, j * tilesize % splitSize, tilesize, tilesize), tileMap[i, j].color, 0, tilesize * tilesize);

                    //textMap.SetData(0, new Rectangle(i * tilesize, j * tilesize, tilesize, tilesize), tileMap[i, j].color, 0, tilesize * tilesize);

                }
            Console.WriteLine("Finished Complete Map");

        }

        public void BuildMap(Point location, Color[] color)
        {

            Point p = new Point(location.X % splitSize, location.Y % splitSize);
            for (int i = p.Y; i < p.Y + tilesize; ++i)
            {
                textSplitMap[location.X / splitSize, location.Y / splitSize].SetData(0, new Rectangle(p, new Point(tilesize, tilesize)), color, 0, tilesize * tilesize);
            }
        }

        public void BuildRoad(Rectangle bounds)
        {
            for (int i = bounds.X; i < bounds.X + bounds.Width; i += tilesize)
                for (int j = bounds.Y; j < bounds.Y + bounds.Height; j += tilesize)
                {
                    Tile tile = GetTile(new Point(i/tilesize, j/tilesize));
                    tile.BuildRoad();
                    BuildMap(new Point(i, j), tile.color);
                }
        }

        public void Build(Rectangle bounds, Building building)
        {
            if(Buildable(bounds))
            {
                for (int i = bounds.X; i <= bounds.X + bounds.Width; i += tilesize)
                    for (int j = bounds.Y; j <= bounds.Y + bounds.Height; j += tilesize)
                    {
                        Tile tile = GetTile(new Point(i, j));
                        tile.Buildable = false;
                        tile.refBuilding = building;
                        BuildingHandler.Instance.buildingList.Add(building);
                    }
            }
        }

        public bool Buildable(Rectangle bounds)
        {
            for (int i = bounds.X; i < bounds.X + bounds.Width; i += tilesize)
                for (int j = bounds.Y; j < bounds.Y + Math.Abs(bounds.Height); j += tilesize)
                {
                    if (!GetTile(new Point(i, j)).Buildable)
                        return false;
                }

            return true;
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
            //TODO: Nur Tiles updaten die man sehen kann
            /*
            for (int i = 0; i < tileMap.GetLength(0); ++i)
                for (int j = 0; j < tileMap.GetLength(1); ++j)
                    tileMap[i, j].Update(gTime);
                    */
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int offset = 15;

            Rectangle miniMapRect = new Rectangle(new Point(CameraHandler.Instance.screenCamera.view.X + 1050, CameraHandler.Instance.screenCamera.view.Y + 30), miniMapSize);

            Rectangle miniMapOffset = new Rectangle(miniMapRect.Location - new Point(offset, offset), miniMapRect.Size + new Point(2 * offset, 2 * offset));


            Rectangle miniMapCameraRect = new Rectangle(miniMapRect.X + CameraHandler.Instance.screenCamera.view.X / (bounds.Width / miniMapSize.X), miniMapRect.Y + CameraHandler.Instance.screenCamera.view.Y / (bounds.Height / miniMapSize.Y), (miniMapRect.Width * 1280) / bounds.Width, (miniMapRect.Height * 720) / bounds.Height);

            //The Current View
            //spriteBatch.Draw(textMap, CameraHandler.Instance.screenCamera.view, CameraHandler.Instance.screenCamera.view, Color.White);

            Rectangle cam = CameraHandler.Instance.screenCamera.view;

            int minrow = cam.X / splitSize;
            int maxrow = (cam.X + cam.Width) / splitSize;
            int mincol = cam.Y / splitSize;
            int maxcol = (cam.Y + cam.Height) / splitSize;

            MathHelper.Clamp(minrow, 0, textSplitMap.GetLength(0));
            MathHelper.Clamp(mincol, 0, textSplitMap.GetLength(0));
            MathHelper.Clamp(maxrow, 0, textSplitMap.GetLength(1));
            MathHelper.Clamp(maxcol, 0, textSplitMap.GetLength(1));






            for (int i = minrow; i <= maxrow; ++i)
                for (int j = mincol; j <= maxcol; ++j)
                {
                    Rectangle mapPos = new Rectangle(i * splitSize, j * splitSize, splitSize, splitSize);

                    spriteBatch.Draw(textSplitMap[i, j], mapPos, Color.White);
                }

            spriteBatch.Draw(miniMapBackground, miniMapOffset, Color.White);

            for (int i = 0; i < textSplitMap.GetLength(0); ++i)
                for (int j = 0; j < textSplitMap.GetLength(1); ++j)
                {
                    Rectangle exactMiniMap = new Rectangle(miniMapRect.X + i * miniMapSize.X / textSplitMap.GetLength(0),
                        miniMapRect.Y + j * miniMapSize.X / textSplitMap.GetLength(1)
                        , miniMapSize.X / textSplitMap.GetLength(0) + 1, miniMapSize.X / textSplitMap.GetLength(1) + 1);

                    spriteBatch.Draw(textSplitMap[i, j], exactMiniMap, Color.White);
                }



            spriteBatch.Draw(miniMapCurrentView, miniMapCameraRect, new Color(Color.White, 128));
        }


    }
}
