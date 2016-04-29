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
            float test = 1f;
            return new Vector2(currentMouseState.X * test, currentMouseState.Y * test);
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