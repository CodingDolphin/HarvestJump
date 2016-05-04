using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace HarvestJump
{
    public enum AIState
    {
        atacking,
        chasing,
        searching,
    }

    class Enemy : GameObject, ISmart
    {
        public List<ITarget> targetList { get; set; }
        public ITarget currentTarget { get; set; }
        protected Vector2 speed { get; set; }
        protected Vector2 jumpStrength { get; set; }
        private Direction wayPointDirection { get; set; }
        private SoundEffect biteSound { get; set; }
        protected bool wayPointAdded { get; set; }
        protected double targetSwitchTimer { get; set; }
        protected double dirSwitchTimer { get; set; }
        protected double atackDuration { get; set; }
        protected double atackTimer { get; set; }
        protected bool isAtacking { get; set; }
        protected double speedSwitchTimer { get; set; }

        //ISmart Interface

        public AIState aiState { get; set; }
        public float chaseTreshold { get; set; }
        public float seeRadius { get; set; }

        public Enemy(Vector2 position, int width, int height) : base(position, width, height)
        {
            this.targetList = new List<ITarget>();
            this.aiState = AIState.searching;
            this.chaseTreshold = 350;
            this.seeRadius = 800;
            this.targetSwitchTimer = 5;
            this.dirSwitchTimer = 0.5;
            this.speedSwitchTimer = 1;
            this.isAtacking = false;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            this.biteSound = content.Load<SoundEffect>("SoundAssets/PlayAssets/bite");
            base.LoadContent(content, assetName);
            this.onContentLoad();
        }

        public void onContentLoad()
        {
            atackDuration = StateData[AnimationStatus.atacking].Item1.AnimationDuration;
        }

        public override void Update(GameTime gameTime)
        {
            if (State == State.active)
            {
                targetSwitchTimer += gameTime.ElapsedGameTime.TotalSeconds;
                dirSwitchTimer += gameTime.ElapsedGameTime.TotalSeconds;
                atackTimer += gameTime.ElapsedGameTime.TotalSeconds;
                speedSwitchTimer += gameTime.ElapsedGameTime.TotalSeconds;

                SetState();
                DebugEnemyInput();

                if (Direction == Direction.right)
                {
                    Velocity += speed;
                }
                else if (Direction == Direction.left)
                {
                    Velocity -= speed;
                }

                if (wayPointAdded)
                    Direction = wayPointDirection;

                wayPointAdded = false;

            }

            base.Update(gameTime);
        }

        private void SetState()
        {
            switch (aiState)
            {
                case AIState.atacking:
                    Atack();
                    break;
                case AIState.searching:
                    Search();
                    break;
                case AIState.chasing:
                    Chase();
                    break;
            }
        }

        private void Chase()
        {
            if (currentTarget != null)
                SwitchDirectionToTarget();

            switchToRun();

            if (Direction == Direction.right && Position.X + StateData[AnimationStatus.atacking].Item2.width >= currentTarget.Position.X && Position.Y - currentTarget.Position.Y <= currentTarget.BoundingBox.height)
            {
                StateData[AnimationStatus.atacking].Item1.index = 0;
                aiState = AIState.atacking;
            }

            if (Direction == Direction.left && currentTarget.BoundingBox.position.X + currentTarget.BoundingBox.width >= BoundingBox.position.X && Position.Y - currentTarget.Position.Y <= currentTarget.BoundingBox.height)
            {
                StateData[AnimationStatus.atacking].Item1.index = 0;
                aiState = AIState.atacking;
            }

            if (Vector2.Distance(currentTarget.Position, Position) >= chaseTreshold)
                aiState = AIState.searching;
        }

        private void Search()
        {
            List<ITarget> removeList = new List<ITarget>();
            switchToWalk();

            foreach (ITarget target in targetList)
            {
                if (Vector2.Distance(target.Position, Position) <= chaseTreshold)
                {
                    aiState = AIState.chasing;

                    if (targetSwitchTimer >= 3)
                    {
                        targetSwitchTimer = 0;
                        currentTarget = target;
                    }
                }
                else if (Vector2.Distance(target.Position, Position) >= seeRadius)
                {
                    removeList.Add(target);
                }
            }

            foreach (var item in removeList)
            {
                targetList.Remove(item);

                if (!targetList.Any())
                {
                    currentTarget = null;
                }
            }
        }

        public virtual void HandleWaypoint(Direction direction)
        {
            if (aiState == AIState.searching && aiState != AIState.chasing)
            {
                wayPointDirection = direction;
                wayPointAdded = true;
            }

        }

        public void AddTarget(ITarget target)
        {
            targetList.Add(target);
        }

        protected void Atack()
        {
            Velocity = new Vector2(0, Velocity.Y);
            speed = Vector2.Zero;
            SwitchAnimation(AnimationStatus.atacking);

            if (atackTimer >= 0.9 * GameObject.slowMotion)
            {
                biteSound.Play(0.5f, -0.1f, 0);
                atackTimer = 0;
            }

            if (CurrentAnimation.index == StateData[AnimationStatus.atacking].Item1.frameCount)
            {
                aiState = AIState.searching;
                currentTarget.HandleHit();
            }
        }

        protected void CreateAtackArea()
        {
            Vector2 atackPosition = Vector2.Zero;

            if (Direction == Direction.left)
            {
                atackPosition = new Vector2(StateData[AnimationStatus.atacking].Item2.position.X, Position.Y);
            }
            else if (Direction == Direction.right)
            {
                atackPosition = new Vector2(StateData[AnimationStatus.atacking].Item2.position.X + StateData[AnimationStatus.atacking].Item2.width, Position.Y);
            }

            BoundingBox atackArea = new BoundingBox(atackPosition, BoundingBox.width, BoundingBox.height);
        }

        protected void CheckIfTargetsInAtackArea()
        {
        }

        private void switchToRun()
        {
            if (speedSwitchTimer >= 0.1)
            {
                speedSwitchTimer = 0;
                speed = new Vector2(10f, 0);
                SwitchAnimation(AnimationStatus.run);
            }
        }

        private void switchToWalk()
        {
            if (speedSwitchTimer >= 0.1)
            {
                speedSwitchTimer = 0;
                speed = new Vector2(2f, 0);
                SwitchAnimation(AnimationStatus.walking);
            }
        }

        protected void SwitchDirectionToTarget()
        {
            if (dirSwitchTimer >= 0.5)
            {
                if (this.MidPositionX.X <= currentTarget.MidPositionX.X)
                    Direction = Direction.right;

                if (this.MidPositionX.X >= currentTarget.MidPositionX.X)
                    Direction = Direction.left;

                dirSwitchTimer = 0;
            }
        }

        private void DebugEnemyInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A))
            {
                this.Direction = Direction.left;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                this.Direction = Direction.right;
            }

            if (keyboardState.IsKeyDown(Keys.RightControl))
            {
                SwitchAnimation(AnimationStatus.atacking);
                speed = Vector2.Zero;
            }

            if (keyboardState.IsKeyDown(Keys.LeftControl))
            {
                SwitchAnimation(AnimationStatus.walking);
                speed = new Vector2(2, 0);
            }

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                SwitchAnimation(AnimationStatus.dead);
                CurrentAnimation.index = 0;
                speed = new Vector2(0, 0);
                State = State.inactive;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (currentTarget != null)
                spriteBatch.Draw(CurrentTargetArrow.texture, new Vector2(currentTarget.Position.X + currentTarget.BoundingBox.width / 2 - CurrentTargetArrow.width / 2,
                                                                         currentTarget.Position.Y - CurrentTargetArrow.height), Color.Red);

            spriteBatch.DrawString(DebugFont, String.Format("{0:0}", targetSwitchTimer), Vector2.Zero, Color.Red);

            base.Draw(spriteBatch);
        }
    }
}
