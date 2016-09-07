using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarDeMarket
{
    class InputHandler
    {

        static InputHandler instance;
        public KeyboardState keyPrevState;
        public MouseState mousePrevState;

        InputHandler()
        {

        }

        public static InputHandler Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputHandler();

                return instance;
            }
        }

        public bool IsKeyPressedOnce(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && !keyPrevState.IsKeyDown(key);
        }

        public bool IsLeftMouseButtonPressedOnce()
        {
            return (Mouse.GetState().LeftButton == ButtonState.Pressed && mousePrevState.LeftButton != ButtonState.Pressed);
        }
    }
}
