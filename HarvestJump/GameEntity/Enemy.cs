using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
        public Vector2 speed { get; set; }
        public Vector2 jumpStrength { get; set; }
        public List<ITarget> targetList { get; set; }
        public ITarget currentTarget { get; set; }
        public Direction wayPointDirection { get; set; }
        public bool wayPointAdded { get; set; }
        public double targetSwitchTimer { get; set; }
        public double dirSwitchTimer { get; set; }
        public double atackDuration { get; set; }
        public double atackTimer { get; set; }
        public bool isAtacking { get; set; }

        //ISmart Interface

        public AIState aiState { get; set; }
        public float chaseTreshold { get; set; }
        public float seeRadius { get; set; }

        public Enemy(Vector2 position, int width, int height) : base(position, width, height)
        {
            this.targetList = new List<ITarget>();
            this.aiState = AIState.searching;
            this.chaseTreshold = 400;
            this.seeRadius = 1000;
            this.targetSwitchTimer = 5;
            this.isAtacking = false;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
            this.onContentLoad();
        }

        public void onContentLoad()
        {
            atackDuration = StateData[AnimationStatus.atacking].Item1.AnimationDuration;
        }

        public override void Update(GameTime gameTime)
        {
            targetSwitchTimer += gameTime.ElapsedGameTime.TotalSeconds;
            dirSwitchTimer += gameTime.ElapsedGameTime.TotalSeconds;
            atackTimer += gameTime.ElapsedGameTime.TotalSeconds;

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

            base.Update(gameTime);

            if (wayPointAdded)
                Direction = wayPointDirection;

            wayPointAdded = false;

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

            speed = new Vector2(5, 0);
            SwitchAnimation(AnimationStatus.run);

            if (Direction == Direction.right && Position.X + StateData[AnimationStatus.atacking].Item2.width >= currentTarget.Position.X)
            {
                StateData[AnimationStatus.atacking].Item1.index = 0;
                aiState = AIState.atacking;
            }

            if (Direction == Direction.left && currentTarget.BoundingBox.position.X + currentTarget.BoundingBox.width >= BoundingBox.position.X)
            {
                StateData[AnimationStatus.atacking].Item1.index = 0;
                aiState = AIState.atacking;
            }

            if (Vector2.Distance(currentTarget.Position, Position) >= chaseTreshold)
                aiState = AIState.searching;
        }

        private void Search()
        {
            speed = new Vector2(2, 0);
            CurrentAnimation = StateData[AnimationStatus.walking].Item1;

            foreach (ITarget target in targetList)
            {
                if (Vector2.Distance(target.Position, Position) <= chaseTreshold)
                {
                    aiState = AIState.chasing;

                    if (targetSwitchTimer >= 5)
                    {
                        targetSwitchTimer = 0;
                        currentTarget = target;
                    }
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
            Velocity = Vector2.Zero;
            speed = Vector2.Zero;
            SwitchAnimation(AnimationStatus.atacking);

            if (CurrentAnimation.index == StateData[AnimationStatus.atacking].Item1.frameCount)
            {
                aiState = AIState.searching;
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
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
