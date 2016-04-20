using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace HarvestJump
{
   public class InputManager
    {
        public KeyboardState currentKeyboardState;
        public KeyboardState prevKeyboardState;
        public GamePadState gamePadState;
        public GamePadState prevGamePadState;
        public MouseState currentMouseState;
        public MouseState oldMouseState;
        public PlayerIndex playerIndex;
        
        public InputManager(int index)
        {
            playerIndex = (PlayerIndex)index;
        }

        public void Update(GameTime gameTime)
        {
            prevKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            prevGamePadState = gamePadState;
            gamePadState = GamePad.GetState(playerIndex);

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

        public bool getAButtonPressed()
        {
            return gamePadState.Buttons.A == ButtonState.Pressed;
        }

        public Vector2 getLeftThumbStickMovement()
        {
            return gamePadState.ThumbSticks.Left;
        }
    }
}