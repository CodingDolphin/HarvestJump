using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HarvestJump
{
    class Player : GameObject
    {
        private KeyboardState prevState;

        public Player(Vector2 startPosition, int width, int height) : base(startPosition,width,height)
        {
            sprite = new Animation(startPosition, 0, 86, 75, 0.3, 9);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            sprite.LoadContent(content, assetName);
        }

        public override void Update(GameTime gameTime)
        {
            CheckKeyboardAndUpdateMovement();

            base.Update(gameTime);
        }

        private void CheckKeyboardAndUpdateMovement()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            prevState = keyboardState;

            if (keyboardState.IsKeyDown(Keys.Left)) { velocity += new Vector2(-20, 0); }
            if (keyboardState.IsKeyDown(Keys.Right)) { velocity += new Vector2(20, 0); }
            if (keyboardState.IsKeyDown(Keys.Up) && !isJumping) { velocity += new Vector2(0, -1000);isJumping = true; hasContact = false;  }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
