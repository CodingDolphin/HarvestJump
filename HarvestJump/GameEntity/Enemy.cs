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
    }

    class Enemy : GameObject, ISmart
    {
        public Vector2 speed { get; set; }
        public Vector2 jumpStrength { get; set; }
        public AIState aiState { get; set; }
        public float chaseTreshold { get; set; }

        public Enemy(Vector2 position, int width, int height) : base(position, width, height)
        {
            chaseTreshold = 200f;
            this.isJumping = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
        }

        public virtual void HandleWaypoint(Direction direction)
        {
            base.SetDirection(direction);
        }

        public void Chase(Vector2 target)
        {
            if (target.X > position.X && Vector2.Distance(target, position) >= currentAnimation.frameWidth / 2)
                SetDirection(Direction.right);
            else if(target.X < position.X && Vector2.Distance(target, position) >= currentAnimation.frameWidth / 2)
                SetDirection(Direction.left);

            if(Vector2.Distance(target, position) >= currentAnimation.frameWidth)
            {
                speed = Vector2.Zero;
                currentAnimation = stateData[AnimationStatus.atacking].Item1;
            }
            else if (target.X < position.X && Vector2.Distance(target, position) >= currentAnimation.frameWidth)
            {
                speed = Vector2.Zero;
                currentAnimation = stateData[AnimationStatus.atacking].Item1;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
