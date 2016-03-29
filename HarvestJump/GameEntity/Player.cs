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
        private Animation idleAnimation { get; set; }
        private KeyboardState prevState;

        public Player(Vector2 startPosition, int width, int height) : base(startPosition,width,height)
        {
            idleAnimation = new Animation(startPosition, 0, 86, 75, 0.3, 9);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            idleAnimation.LoadContent(content, assetName);
        }

        public override void Update(GameTime gameTime)
        {
            CheckKeyboardAndUpdateMovement();
            UpdateAnimation(gameTime);

            base.Update(gameTime);
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            idleAnimation.position = position;
            idleAnimation.Update(gameTime);
        }

        private void CheckKeyboardAndUpdateMovement()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            prevState = keyboardState;

            if (keyboardState.IsKeyDown(Keys.Left)) { velocity += new Vector2(-75, 0); }
            if (keyboardState.IsKeyDown(Keys.Right)) { velocity += new Vector2(75, 0); }
            if (keyboardState.IsKeyDown(Keys.Up) && !isJumping) { velocity += new Vector2(0, -1250);isJumping = true;   }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            idleAnimation.Draw(spriteBatch);
        }
    }
}
