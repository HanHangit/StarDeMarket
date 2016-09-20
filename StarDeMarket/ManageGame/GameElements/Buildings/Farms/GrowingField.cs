using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class GrowingField
    {
        //True wenn die Frucht fertig gewachsen ist.
        public bool finished { get; private set; }

        //Wenn der Acker entfernt werden kann.
        public bool wasted;

        //Texturen: 0 - noch nicht gewachsen, 1 - ausgewachsene Frucht
        Texture2D[] text;

        //Aktuelle TExtur
        Texture2D currText;

        //Position
        Point position;

        //Wie lange die Pflanze zum wachsen braucht.
        float growingTime;

        //Das zugehörige Tile auf der Map
        Tile targetTile;

        //Der Gegenstand der am Ende rauskommen soll.
        EItem finishedGoods;

        //Timer für das Wachstum.
        float timer;

        public GrowingField(Point position, Texture2D[] textures, float growTime, EItem type)
        {
            targetTile = null;
            this.position = position;
            finished = false;
            wasted = false;
            finishedGoods = type;
            growingTime = growTime;
            text = textures;
            timer = 0;
            currText = text[0];
        }


        public void Update(GameTime gTime)
        {
            if (targetTile == null)
            {
                timer += (float)gTime.ElapsedGameTime.TotalSeconds;
                if (timer > growingTime) //Wenn growingTIme erreicht ist.
                {
                    //Tile auf der Map wird zwischengespeichert.
                    targetTile = BuildingHandler.Instance.map.GetTile(position);

                    //Textur wird verändert.
                    currText = text[1];

                    //Pflanze ist ausgewachsen und kann geerntet werden.
                    finished = true;

                    //Auf dem Tile darf gearbeitet werden.
                    targetTile.WorkAble = true;

                    //Die fertigen Rohstoffe werden dem Storage hinzugefügt
                    targetTile.storage.Add(finishedGoods, 3);

                    //Wie lange die Worker zum abbernten benötigen
                    targetTile.WorkTime = 1f;
                }
            }
            else
            {
                //Wenn keine Rohstoffe mehr vorhanden sind
                if (!targetTile.storage.Check(finishedGoods))
                {
                    //Acker kann entfernt werden.
                    wasted = true;

                    //Tile wird auf Grasslandschaft zurückgesetzt
                    targetTile.Reset(ETile.Grass);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Es wird gezeichnet.
            spriteBatch.Draw(currText, position.ToVector2(), Color.White);
        }

    }
}
