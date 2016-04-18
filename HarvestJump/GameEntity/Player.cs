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
        public Player(Vector2 startPosition, int width = 67, int height = 95) : base(startPosition, width, height)
        {
            direction = Direction.left;
            initializePlayerAnimation();
        }

        public void initializePlayerAnimation()
        {
            this.AddState(AnimationStatus.walking, position, 0, 67, 97, 0.25f, 9, true, 67, 95);
            this.AddState(AnimationStatus.idle, position, 0, 67, 97, 0.3f, 9, true, 67, 95);
            this.AddState(AnimationStatus.jumping, position, 0, 67, 97, 0.3f, 6, false, 67, 95);
            this.AddState(AnimationStatus.dead, position, 0, 102, 93, 0.25f, 7, false, 80, 70);
            this.AddState(AnimationStatus.run, position, 0, 64, 96, 0.25f, 7, true, 67, 95);
            this.currentAnimation = stateData[AnimationStatus.idle].Item1;
        }


        public override void LoadContent(ContentManager content, string assetName)
        {
            string contentPath = assetName;

            foreach (var item in stateData)
            {
                switch (item.Key)
                {
                    case AnimationStatus.walking:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerWalkAnimation");
                        break;
                    case AnimationStatus.jumping:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerJumpAnimation");
                        break;
                    case AnimationStatus.idle:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerIdleAnimation");
                        break;
                    case AnimationStatus.slide:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerSlideAnimation");
                        break;
                    case AnimationStatus.dead:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerDeadAnimation");
                        break;
                    case AnimationStatus.run:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerRunAnimation");
                        break;
                }
            }

            base.LoadContent(content, assetName);
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
              SetDirection(Direction.left);
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                velocity += new Vector2(20, 0);
                currentAnimation.direction = SpriteEffects.None;
            }

            if ((keyboardState.IsKeyDown(Keys.Up) || (keyboardState.IsKeyDown(Keys.Space))) && !isJumping)
            {
                velocity += new Vector2(0, -1000);isJumping = true;
                currentAnimation = stateData[AnimationStatus.jumping].Item1;
                currentAnimation.index = 0;
                currentAnimation.animationIsActive = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
