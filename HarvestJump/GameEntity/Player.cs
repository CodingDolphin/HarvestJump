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
        private PlayerType playerType { get; set; }
        private InputManager inputManager { get; set; }
        private Vector2 movementVector { get; set; }

        public Player(Vector2 startPosition, PlayerType playerType) : base(startPosition)
        {
            this.playerType = playerType;
            this.Speed = new Vector2(28f, 0);
            this.JumpStrength = new Vector2(0, -1100);
            this.inputManager = new InputManager(playerCount);
            this.initializePlayerAnimation();

            playerCount++;
        }

        public void initializePlayerAnimation()
        {
            #region AnimationData
            if (playerType == PlayerType.dog)
            {
                this.AddState(AnimationStatus.walking, Position, 33.5f, 0, 65, 100, 0.1f, 9, true, 67, 95);
                this.AddState(AnimationStatus.idle, Position, 33.5f, 0, 65, 97, 0.3f, 9, true, 67, 95);
                this.AddState(AnimationStatus.jumping, Position, 33.5f, 0, 65, 97, 0.3f, 6, false, 67, 95);
                this.AddState(AnimationStatus.dead, Position, 51f, 0, 102, 93, 0.25f, 7, false, 80, 70);
                this.AddState(AnimationStatus.run, Position, 32, 0, 65, 100, 0.25f, 7, true, 67, 95);
            }
            else if(playerType == PlayerType.cat)
            {
                this.AddState(AnimationStatus.walking, Position, 33.5f, 0, 67, 97, 0.1f, 9, true, 67, 95);
                this.AddState(AnimationStatus.idle, Position, 33.5f, 0, 67, 97, 0.3f, 9, true, 67, 95);
                this.AddState(AnimationStatus.jumping, Position, 33.5f, 0, 67, 97, 0.3f, 6, false, 67, 95);
                this.AddState(AnimationStatus.dead, Position, 51f, 0, 102, 93, 0.25f, 7, false, 80, 70);
                this.AddState(AnimationStatus.run, Position, 32, 0, 64, 96, 0.25f, 7, true, 67, 95);
            }
            #endregion Animationdata

            SwitchAnimation(AnimationStatus.idle);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            #region LoadContent
            string contentPath = assetName;
            string pType;

            if (playerType == PlayerType.dog)
                pType = "Dog";
            else
                pType = "Cat";

            foreach (var item in StateData)
            {
                switch (item.Key)
                {
                    case AnimationStatus.walking:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerAssets/" + pType + "WalkAnimation");
                        break;
                    case AnimationStatus.jumping:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerAssets/" + pType + "JumpAnimation");
                        break;
                    case AnimationStatus.idle:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerAssets/" + pType + "IdleAnimation");
                        break;
                    case AnimationStatus.slide:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerAssets/" + pType + "SlideAnimation");
                        break;
                    case AnimationStatus.dead:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerAssets/" + pType + "DeadAnimation");
                        break;
                    case AnimationStatus.run:
                        item.Value.Item1.LoadContent(content, contentPath + "PlayerAssets/" + pType + "RunAnimation");
                        break;
                }
            }

            #endregion LoadContent

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
            movementVector = inputManager.getLeftThumbStickMovement();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Velocity -= Speed;
                CurrentAnimation.Direction = Direction = Direction.left;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Velocity += Speed;
                CurrentAnimation.Direction = Direction = Direction.right;
            }

            if ((keyboardState.IsKeyDown(Keys.Up) || inputManager.getAButtonPressed()) && !IsJumping)
            {
                Velocity += JumpStrength; IsJumping = true;
                StateData[AnimationStatus.jumping].Item1.index = 0;
                SwitchAnimation(AnimationStatus.jumping);
            }

            if (movementVector.X >= 0 && movementVector.X != 0)
                Direction = Direction.right;
            else if (movementVector.X <= 0 && movementVector.X != 0)
                Direction = Direction.left;

            Velocity += movementVector * Speed;
            SwitchAnimationAccordingToVelocity();
        }

        private void SwitchAnimationAccordingToVelocity()
        {
            if (!IsJumping)
            {
                if (Velocity.X >= 10 && Direction == Direction.right)
                    SwitchAnimation(AnimationStatus.walking);

                if (Velocity.X <= -10 && Direction == Direction.left)
                    SwitchAnimation(AnimationStatus.walking);

                if (Velocity.X >= 350 && Direction == Direction.right)
                    SwitchAnimation(AnimationStatus.run);

                if (Velocity.X <= -350 && Direction == Direction.left)
                    SwitchAnimation(AnimationStatus.run);

                if (Velocity.X <= 10 && Direction == Direction.right)
                    SwitchAnimation(AnimationStatus.idle);

                if (Velocity.X >= -10 && Direction == Direction.left)
                    SwitchAnimation(AnimationStatus.idle);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
