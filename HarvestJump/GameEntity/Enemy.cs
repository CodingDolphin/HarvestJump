using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestJump
{
    public enum AIState
    {
        walking,
        atacking,
        chasing,
        searching,
        idle,
        jumping,
        dead,
    }

    class Enemy : GameObject, ISmart
    {
        public Vector2 speed { get; set; }
        public Vector2 jumpStrength { get; set; }
        public AIState aiState { get; set; }
        public float chaseTreshold { get; set; }
        public float atackDuration { get; set; }
        public bool atackFinish { get; set; }
        public float animationStatus { get; set; }

        public Enemy(Vector2 position, int width, int height) : base(position, width, height)
        {
            this.aiState = AIState.walking;
            this.chaseTreshold = 350f;
            this.IsJumping = false;
        }

        public override void Update(GameTime gameTime)
        {
            animationStatus += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(animationStatus >= StateData[AnimationStatus.atacking].Item1.AnimationDuration)
            {
                animationStatus = 0f;
                atackFinish = true;
            }

            switch (aiState)
            {
                case AIState.walking: Walk();
                    break;
                case AIState.atacking:Atack();
                    break;
                case AIState.chasing: Chase();
                    break;
                case AIState.searching:
                    break;
                case AIState.idle:
                    break;
                case AIState.jumping:
                    break;
                case AIState.dead:
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
            onContentLoad();
        }

        public void onContentLoad()
        {
            atackDuration = (float)StateData[AnimationStatus.atacking].Item1.AnimationDuration;
        }

        public virtual void HandleWaypoint(Direction direction)
        {
            Direction = direction;
        }

        public void AddTarget(ITarget target)
        {
            if(Vector2.Distance(target.Position, Position) <= chaseTreshold)
            {
                aiState = AIState.chasing;

                if (atackFinish)
                {
                    if (target.Position.X >= Position.X && Direction == Direction.left)
                        Direction = Direction.right;
                    else if (target.Position.X <= Position.X && Direction == Direction.right)
                        Direction = Direction.left;

                    atackFinish = false;
                }

                if (Vector2.Distance(target.Position, Position) <= target.BoundingBox.width && Direction == Direction.left)
                {
                    aiState = AIState.atacking;
                }
                else if (Position.X + StateData[AnimationStatus.atacking].Item2.width >= target.Position.X && Direction == Direction.right && Position.X <= target.Position.X)
                {
                    aiState = AIState.atacking;
                }
            }
            else if(aiState != AIState.dead)
            {
                aiState = AIState.walking;
            }
        }

        protected void Chase()
        {
            SwitchAnimation(AnimationStatus.run);
            speed = new Vector2(6, 0);
        }

        protected void Walk()
        {
            SwitchAnimation(AnimationStatus.walking);
            speed = new Vector2(2, 0);
        }

        protected void Atack()
        {
            SwitchAnimation(AnimationStatus.atacking);
            speed = new Vector2(0, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
