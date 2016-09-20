using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarDeMarket
{
    abstract class Farm : Building
    {
        //Liste für den Acker
        public List<GrowingField> fields = new List<GrowingField>();

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);

            //Alle AckerTeile werden geupdated
            for (int i = 0; i < fields.Count; ++i)
                if (fields[i].wasted) //Falls der Acker entfernt werden kann.
                    fields.RemoveAt(i--); //Acker wird entfernt
                else
                    fields[i].Update(gTime); //Update
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            for (int i = 0; i < fields.Count; ++i)
                fields[i].Draw(spriteBatch); //Wird gezeichnet.
        }

        public abstract void PlantField(Point plantPos); 
    }
}
