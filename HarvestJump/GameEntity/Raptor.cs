using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HarvestJump
{
    class Raptor : Enemy
    {
        public Raptor(Vector2 position, int width = 151, int height = 115) : base(position, width, height)
        {
            speed = new Vector2(2f, 0);
            jumpStrength = new Vector2(0f, -250f);
            initializeRaptorAnimation();
        }

        public void initializeRaptorAnimation()
        {
            this.AddAnimation(AnimationStatus.atacking, position, 0, 191, 115, 0.1f, 9);
            this.AddAnimation(AnimationStatus.idle, position, 0, 191, 115, 0.3f, 7);
            this.AddAnimation(AnimationStatus.run, position, 0, 191, 115, 0.3f, 7);
            this.AddAnimation(AnimationStatus.walking, position, 0, 191, 115, 0.3f, 9);
            this.AddAnimation(AnimationStatus.dead, position, 0, 209, 115, 0.3f, 8);
            this.currentAnimation = animationDictionary[AnimationStatus.walking];
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            string contentPath = assetName;

            foreach (var item in animationDictionary)
            {
                switch (item.Key)
                {
                    case AnimationStatus.run:
                        item.Value.LoadContent(content, contentPath + "RaptorRunAnimation");
                        break;
                    case AnimationStatus.walking:
                        item.Value.LoadContent(content, contentPath + "RaptorWalkAnimation");
                        break;
                    case AnimationStatus.dead:
                        item.Value.LoadContent(content, contentPath + "RaptorDeadAnimation");
                        break;
                    case AnimationStatus.jumping:
                        item.Value.LoadContent(content, contentPath + "RaptorJumpAnimation");
                        break;
                    case AnimationStatus.idle:
                        item.Value.LoadContent(content, contentPath + "RaptorIdleAnimation");
                        break;
                    case AnimationStatus.atacking:
                        item.Value.LoadContent(content, contentPath + "RaptorAtackAnimation");
                        break;
                }
            }

            base.LoadContent(content, "0");
        }

        private void SetDirection(Direction dir)
        {
            Vector2 flipTranslate = new Vector2(-(currentAnimation.frameWidth / 2), 0.0f);

            if (direction != dir)
            {
                if (direction == Direction.right)
                {
                    currentAnimation.direction = SpriteEffects.FlipHorizontally;
                    this.boxXTranslate = new Vector2(currentAnimation.frameWidth - boundingBox.width, 0);

                    position += flipTranslate;
                }
                else if (direction == Direction.left)
                {
                    position -= flipTranslate;
                    currentAnimation.direction = SpriteEffects.None;
                    boxXTranslate = Vector2.Zero;
                }
            }

            direction = dir;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                this.SetDirection(Direction.left);
            }

            if (keyboardState.IsKeyDown(Keys.RightShift))
            {
                this.SetDirection(Direction.right);
            }

            if (keyboardState.IsKeyDown(Keys.RightControl))
            {
            }

            if (direction == Direction.right)
            {
                velocity += speed;
            }
            else if( direction == Direction.left)
            {
                velocity -= speed;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
