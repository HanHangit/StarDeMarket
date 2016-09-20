using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //Muss ein Vielfaches TileSize sein
        public int splitSize = 256;

        //Die aufgeteilte TileMap
        Tile[,] tileMap;

        //Die Textur der Map, die am Ende gezeichnet wird
        //public Texture2D textMap { get; private set; }

        //Die Textur des Hintergrunds der MiniMap
        Texture2D miniMapBackground;

        Texture2D miniMapCurrentView;

        Texture2D[,] textSplitMap;

        Queue<Tile> updateQueue;

        //Die Größe der MiniMap
        Point miniMapSize;

        //ContentManager
        ContentManager Content;

        

        public Tilemap(string strMap, ContentManager cont)
        {
            updateQueue = new Queue<Tile>();

            Content = new ContentManager(cont.ServiceProvider, cont.RootDirectory);

            //For faster loading
            Tile.tileText = new Texture2D[(int)ETile.Count][];
            Tile.tileColorData = new Color[(int)ETile.Count][][];

            Tile.tileText[0] = new[] { Content.Load<Texture2D>("Tile/Grass01") };
            Tile.tileText[1] = new[] { Content.Load<Texture2D>("Tile/Grass01"), Content.Load<Texture2D>("Tile/Grass02") };
            Tile.tileText[2] = new[] { Content.Load<Texture2D>("Tile/Water01") };
            Tile.tileText[3] = new[] { Content.Load<Texture2D>("Tile/Rock01"), Content.Load<Texture2D>("Tile/Rock02") };
            Tile.tileText[4] = new[] { Content.Load<Texture2D>("Tile/Tree02") };
            Tile.tileText[5] = new[] { Content.Load<Texture2D>("Tile/Road01") };
            Tile.tileText[6] = new[] { Content.Load<Texture2D>("Tile/Coal01") };
            Tile.tileText[7] = new[] { Content.Load<Texture2D>("Tile/Gold01") };
            Tile.tileText[8] = new[] { Content.Load<Texture2D>("Tile/Iron01") };

            for (int i = 0; i < Tile.tileText.Length; ++i)
            {
                Tile.tileColorData[i] = new Color[Tile.tileText[i].Length][];
                for (int j = 0; j < Tile.tileText[i].Length; ++j)
                {
                    Tile.tileColorData[i][j] = new Color[tilesize * tilesize];
                    Tile.tileText[i][j].GetData(Tile.tileColorData[i][j]);
                }
            }

            miniMapBackground = cont.Load<Texture2D>("Map/MiniMapBack");
            miniMapCurrentView = cont.Load<Texture2D>("Map/MapArea");
            miniMapSize = new Point(200, 200);

            font = Content.Load<SpriteFont>("Font/FPSFont");

            Stopwatch watch = new Stopwatch();
            watch.Start();
            BuildMap(strMap);
            watch.Stop();
            Console.WriteLine("Needed Time: " + watch.Elapsed.ToString());

            Build(new Rectangle(2000, 2000, 128, 128), new MainBuilding(new Vector2(2000, 2000), cont));

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
                    else if (color[i + j * map.Width].Equals(Tile.tileColor[(int)ETile.Coal]))
                        tileMap[i, j] = new Tile(ETile.Coal, new Vector2(i * tilesize, j * tilesize), tilesize);
                    else if (color[i + j * map.Width].Equals(Tile.tileColor[(int)ETile.Iron]))
                        tileMap[i, j] = new Tile(ETile.Iron, new Vector2(i * tilesize, j * tilesize), tilesize);
                    else if (color[i + j * map.Width].Equals(Tile.tileColor[(int)ETile.Gold]))
                        tileMap[i, j] = new Tile(ETile.Gold, new Vector2(i * tilesize, j * tilesize), tilesize);
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

            bounds = AbsoluteRect(bounds);

            for (int i = bounds.X; i < bounds.X + bounds.Width; i += tilesize)
                for (int j = bounds.Y; j < bounds.Y + bounds.Height; j += tilesize)
                {
                    Tile tile = GetTile(new Point(i, j));
                    if (tile.Buildable)
                    {
                        tile.BuildRoad();
                        BuildMap(new Point(i, j), tile.color);
                    }
                }
        }

        Rectangle AbsoluteRect(Rectangle rect)
        {

            Rectangle nRect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);

            if (nRect.Width < 0)
            {
                nRect.X = nRect.Location.X + nRect.Width - tilesize;
                nRect.Width *= -1;
                nRect.Width += tilesize * 2;
            }

            if (nRect.Height < 0)
            {
                nRect.Y = nRect.Location.Y + nRect.Height - tilesize;
                nRect.Height *= -1;
                nRect.Height += tilesize * 2;
            }

            return nRect;
        }

        public void Build(Rectangle bounds, Building building)
        {

            bounds = AbsoluteRect(bounds);

            for (int i = bounds.X; i < bounds.X + bounds.Width; i += tilesize)
                for (int j = bounds.Y; j < bounds.Y + bounds.Height; j += tilesize)
                {
                    Tile tile = GetTile(new Point(i, j));
                    tile.Buildable = false;
                    tile.WorkAble = false;
                    tile.refBuilding = building;
                    tile.storage = building.Storage;
                }

            BuildingHandler.Instance.buildingList.Add(building);
        }

        public bool Buildable(Rectangle bounds)
        {

            bounds = AbsoluteRect(bounds);

            for (int i = bounds.X; i < bounds.X + bounds.Width; i += tilesize)
                for (int j = bounds.Y; j < bounds.Y + bounds.Height; j += tilesize)
                {

                    if (!GetTile(new Point(i, j)).Buildable)
                    {
                        return false;
                    }
                }

            return true;
        }

        public List<Point> GetAllRoadsOfBounds(Rectangle bounds)
        {
            List<Point> result = new List<Point>();

            int x = bounds.X;
            int y = bounds.Y - tilesize;
            for (; x < (bounds.X + bounds.Width); x += tilesize)
                AddWalkableTileToList(x, y, result);
            y = bounds.Y + bounds.Height + tilesize;
            x = bounds.X;
            for (; x < (bounds.X + bounds.Width); x += tilesize)
                AddWalkableTileToList(x, y, result);
            x = bounds.X - tilesize;
            y = bounds.Y;
            for (; y < bounds.Y + bounds.Height; y += tilesize)
                AddWalkableTileToList(x, y, result);
            x = bounds.X + bounds.Width;
            y = bounds.Y;
            for (; y < bounds.Y + bounds.Height; y += tilesize)
                AddWalkableTileToList(x, y, result);

            RemoveTheNeighborhood(result);

            Console.WriteLine("Having " + result.Count + " Streets closeby");
            if(result.Count == 0)
            {
                return null;
            }

            return result;
        }

        private void RemoveTheNeighborhood(List<Point> neighborhood)
        {
            Queue<Point> currentStreet = new Queue<Point>();
            List<Point> marked = new List<Point>();
            List<Point> buffer = new List<Point>();
            Point p;
            while (neighborhood.Count != marked.Count)
            {
                marked.Add(neighborhood[0]);
                currentStreet.Enqueue(neighborhood[0]);
                while (currentStreet.Count != 0)
                {
                    Point curP = currentStreet.Dequeue();
                    marked.Add(curP);
                    buffer = new List<Point>();
                    p = curP + new Point(0, tilesize);
                    Console.WriteLine(p.ToString());
                    if (ListContainsPoint(p, neighborhood))
                        buffer.Add(p);
                    p = curP + new Point(0, -tilesize);
                    Console.WriteLine(p.ToString());
                    if (ListContainsPoint(p, neighborhood))
                        buffer.Add(p);
                    p = curP + new Point(tilesize, 0);
                    Console.WriteLine(p.ToString());
                    if (ListContainsPoint(p, neighborhood))
                        buffer.Add(p);
                    p = curP + new Point(-tilesize, 0);
                    Console.WriteLine(p.ToString());
                    if (ListContainsPoint(p, neighborhood))
                        buffer.Add(p);
                    foreach (Point _p in buffer)
                    {
                        Console.WriteLine("buffer");
                        currentStreet.Enqueue(_p);
                        neighborhood.Remove(_p);
                    }
                    
                }
            }
        }

        private bool ListContainsPoint(Point p, List<Point> pList)
        {
            foreach(Point _p in pList)
            {
                if (ComparePoints(p, _p))
                    return true;
            }
            return false;
        }

        private bool ComparePoints(Point a, Point b)
        {
            return (a.X == b.X && a.Y == b.Y);
        }

        private void AddWalkableTileToList(int x, int y, List<Point> result)
        {
            Point p = new Point(x, y);
            if (GetTile(new Point(x, y)).Walkable)
            {
                result.Add(p);
            }
        }

        public void UpdateTile(Point position)
        {
            updateQueue.Enqueue(GetTile(position));
        }

        public void UpdateTile(Tile tile)
        {
            updateQueue.Enqueue(tile);
        }


        public Tile GetTile(Point position)
        {
            Tile tile;

            position = new Point(position.X / tilesize, position.Y / tilesize);

            if(position.X < 0 || position.X >= tileMap.GetLength(0) || position.Y < 0 || position.Y >= tileMap.GetLength(1))
                tile = tileMap[0, 0];
            else
                tile = tileMap[position.X, position.Y];

            return tile;
        }

        public Tile SearchTile(Point position, EItem type)
        {
            Tile targetTile = null;

            int k = tilesize;
            int i = position.X;
            int j = position.Y;

            Queue<Tile> queue = new Queue<Tile>();

            while (targetTile == null && k < 1028)
            {



                //TODO: Range des Humans
                k += tilesize;
                {
                    for (int s = i - k; s <= i + k; s += tilesize)
                        queue.Enqueue(GetTile(new Point(s, j - k)));
                    for (int s = i - k; s <= i + k; s += tilesize)
                        queue.Enqueue(GetTile(new Point(s, j + k)));
                    for (int s = j - k; s <= j + k; s += tilesize)
                        queue.Enqueue(GetTile(new Point(i - k, s)));
                    for (int s = j - k; s <= j + k; s += tilesize)
                        queue.Enqueue(GetTile(new Point(i + k, s)));
                }

                while (queue.Count > 0)
                {
                    Tile help = queue.Dequeue();

                    if (help.storage.Check(type) && help.WorkAble)
                    {
                        return help;
                    }

                }

            }

            return targetTile;
        }

        public Tile SearchTile(Point position, ETile type)
        {
            Tile targetTile = null;

            int k = tilesize;
            int i = position.X;
            int j = position.Y;

            Queue<Tile> queue = new Queue<Tile>();

            while (targetTile == null && k < 1028)
            {



                //TODO: Range des Humans
                k += tilesize;
                {
                    for (int s = i - k; s <= i + k; s += tilesize)
                        queue.Enqueue(GetTile(new Point(s, j - k)));
                    for (int s = i - k; s <= i + k; s += tilesize)
                        queue.Enqueue(GetTile(new Point(s, j + k)));
                    for (int s = j - k; s <= j + k; s += tilesize)
                        queue.Enqueue(GetTile(new Point(i - k, s)));
                    for (int s = j - k; s <= j + k; s += tilesize)
                        queue.Enqueue(GetTile(new Point(i + k, s)));
                }

                while (queue.Count > 0)
                {
                    Tile help = queue.Dequeue();

                    if (help.type == type && help.WorkAble)
                    {
                        targetTile = help;
                        return help;
                    }

                }

            }

            return targetTile;
        }

        public void Update(GameTime gTime)
        {
            while (updateQueue.Count > 0)
                updateQueue.Dequeue().Update(gTime);
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
