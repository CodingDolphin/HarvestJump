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
        string variant { get; set; }
        Random random { get; set; }
        public Raptor(Vector2 position, int width = 151, int height = 115) : base(position, width, height)
        {
            speed = new Vector2(2.5f, 0);
            jumpStrength = new Vector2(0f, -250f);
            random = new Random(System.DateTime.Now.Millisecond);

            decideVariant();
            initializeRaptorAnimation();
        }

        public void decideVariant()
        {
            if (random.Next(0,2) == 0)
                variant = "Green";
            else
                variant = "Blue";
        }

        public void initializeRaptorAnimation()
        {
            this.AddState(AnimationStatus.atacking, position, 0, 191, 115, 0.1f, 9, true, 151, 115);
            this.AddState(AnimationStatus.idle, position, 0, 191, 115, 0.3f, 7, true, 151, 115);
            this.AddState(AnimationStatus.run, position, 0, 191, 115, 0.3f, 7, true, 151, 115);
            this.AddState(AnimationStatus.walking, position, 0, 191, 115, 0.3f, 9, true, 150, 110);
            this.AddState(AnimationStatus.dead, position, 0, 209, 115, 0.3f, 8, false, 191, 85);
            this.currentAnimation = stateData[AnimationStatus.walking].Item1;
            this.boundingBox = stateData[AnimationStatus.walking].Item2;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            string contentPath = assetName;

            foreach (var item in stateData)
            {
                switch (item.Key)
                {
                    case AnimationStatus.run:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorRunAnimation" + variant);
                        break;
                    case AnimationStatus.walking:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorWalkAnimation" + variant);
                        break;
                    case AnimationStatus.dead:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorDeadAnimation" + variant);
                        break;
                    case AnimationStatus.jumping:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorJumpAnimation" + variant);
                        break;
                    case AnimationStatus.idle:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorIdleAnimation" + variant);
                        break;
                    case AnimationStatus.atacking:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorAtackAnimation" + variant);
                        break;
                }
            }

            base.LoadContent(content, string.Empty);
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
                this.stateData[AnimationStatus.dead].Item1.direction = currentAnimation.direction;
                this.currentAnimation = stateData[AnimationStatus.dead].Item1;
                this.boundingBox = stateData[AnimationStatus.dead].Item2;
                this.speed = Vector2.Zero;
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
