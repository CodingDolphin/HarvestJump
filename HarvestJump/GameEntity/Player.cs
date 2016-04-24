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
        private static int playerCount { get; set; }
        private InputManager inputManager { get; set; }
        private Vector2 movementVector { get; set; }

        public Player(Vector2 startPosition, int width = 67, int height = 95) : base(startPosition, width, height)
        {
            speed = new Vector2(28f, 0);
            jumpStrength = new Vector2(0, -1100);
            inputManager = new InputManager(playerCount);
            playerCount++;

            initializePlayerAnimation();
        }

        public void initializePlayerAnimation()
        {
            this.AddState(AnimationStatus.walking, position, 0, 67, 97, 0.1f, 9, true, 67, 95);
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
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerIdleAnimation"); stateData[AnimationStatus.idle].Item1.rotationPoint = new Vector2(33.5f, 0);
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
            inputManager.Update(gameTime);
            MoveCharacter();

            base.Update(gameTime);
        }

        private void MoveCharacter()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                velocity -= speed;
                currentAnimation.Direction = Direction = Direction.left;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                velocity += speed;
                currentAnimation.Direction = Direction = Direction.right;
            }

            if ((keyboardState.IsKeyDown(Keys.Up) || inputManager.getAButtonPressed()) && !isJumping)
            {
                velocity += jumpStrength;isJumping = true;
            }

            movementVector = inputManager.getLeftThumbStickMovement();

            if (movementVector.X >= 0 && movementVector.X != 0)
                Direction = Direction.right;

            else if (movementVector.X <= 0 && movementVector.X != 0)
            {
                Direction = Direction.left;
            }

            velocity += movementVector * speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
