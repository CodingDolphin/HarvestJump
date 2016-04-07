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
        public Player(Vector2 startPosition, int width, int height) : base(startPosition,width,height)
        {
            currentSprite = new Animation(startPosition, 0, 65, 90, 0.3, 8);
            rightSprite = new Animation(startPosition, 0, 65, 90, 0.3, 8);
            leftSprite = new Animation(startPosition, 0, 65, 90, 0.3, 8);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            rightSprite.LoadContent(content, assetName);
            leftSprite.LoadContent(content, "GraphicAssets/PlayAssets/CatIdleAnimationLeft");
            currentSprite = rightSprite;
        }

        public override void Update(GameTime gameTime)
        {
            CheckKeyboardAndUpdateMovement();

            base.Update(gameTime);
        }

        public void ChangeDirectionToLeft()
        {
            currentSprite = leftSprite;
        }

        public void ChangeDirectionToRight()
        {
            currentSprite = rightSprite;
        }

        private void CheckKeyboardAndUpdateMovement()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left)) { velocity += new Vector2(-20, 0); ChangeDirectionToLeft(); }
            if (keyboardState.IsKeyDown(Keys.Right)) { velocity += new Vector2(20, 0); ChangeDirectionToRight(); }
            if (keyboardState.IsKeyDown(Keys.Up) && !isJumping) { velocity += new Vector2(0, -1000);isJumping = true; hasContact = false;  }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentSprite.Draw(spriteBatch);
        }
    }
}
