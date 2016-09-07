using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace StarDeMarket
{
    public abstract class Human
    {
        public enum EGender
        {
            Male,
            Female
        }

        protected Vector2 position;
        protected EGender gender;
        protected float speed;

        protected ContentManager cont;
        protected Texture2D texture;
    }
}
