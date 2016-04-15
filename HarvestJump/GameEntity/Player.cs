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
            testSprite = new Sprite(Vector2.Zero);
            initializePlayerAnimation();
        }

        public void initializePlayerAnimation()
        {
            this.AddAnimation(AnimationStatus.walking, position, 0, 67, 97, 0.25f, 9);
            this.AddAnimation(AnimationStatus.run, position, 0, 68, 97, 0.2f, 7);
            this.AddAnimation(AnimationStatus.idle, position, 0, 67, 97, 0.3f, 9);
            this.AddAnimation(AnimationStatus.jumping, position, 0, 67, 97, 0.3f, 7);
            this.AddAnimation(AnimationStatus.dead, position, 0, 102, 93, 0.25f, 7);
            this.currentAnimation = animationDictionary[AnimationStatus.idle];
        }


        public override void LoadContent(ContentManager content, string assetName)
        {
            string contentPath = "GraphicAssets/PlayAssets/";
            testSprite.LoadContent(content, "blackPixel");

            foreach (var item in animationDictionary)
            {
                switch (item.Key)
                {
                    case AnimationStatus.walking:item.Value.LoadContent(content, contentPath + "PlayerWalkAnimation");
                        break;
                    case AnimationStatus.run:item.Value.LoadContent(content, contentPath + "PlayerRunAnimation");
                        break;
                    case AnimationStatus.jumping:item.Value.LoadContent(content, contentPath + "PlayerJumpAnimation");
                        break;
                    case AnimationStatus.idle:item.Value.LoadContent(content, contentPath + "PlayerIdleAnimation");
                        break;
                    case AnimationStatus.slide:item.Value.LoadContent(content, contentPath + "PlayerSlideAnimation");
                        break;
                    case AnimationStatus.dead:item.Value.LoadContent(content, contentPath + "PlayerDeadAnimation");
                        break;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            CheckKeyboardAndUpdateMovement();

            base.Update(gameTime);
        }

        private void CheckKeyboardAndUpdateMovement()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
              velocity += new Vector2(-20, 0);
              currentAnimation.direction = SpriteEffects.FlipHorizontally;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                velocity += new Vector2(20, 0);
                currentAnimation.direction = SpriteEffects.None;
            }

            if ((keyboardState.IsKeyDown(Keys.Up) || (keyboardState.IsKeyDown(Keys.Space))) && !isJumping)
            {
                velocity += new Vector2(0, -1000);isJumping = true;
                currentAnimation = animationDictionary[AnimationStatus.jumping];
                currentAnimation.index = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);
            spriteBatch.Draw(testSprite.texture, position, new Rectangle((int)position.X,(int) position.Y, 65, 100), new Color(255,1,1,0.5f));
        }
    }
}
