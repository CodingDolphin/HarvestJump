using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace HarvestJump
{
    class InputManager
    {
        public MouseState currentMouseState;
        public MouseState oldMouseState;

        public void Update(GameTime gameTime)
        {
            oldMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        public Vector2 getMousePosition()
        {
            return new Vector2(currentMouseState.X * 0.666f, currentMouseState.Y * 0.666f);
        }

        public bool GetLeftClickOnce()
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetLeftClick()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }
    }
}