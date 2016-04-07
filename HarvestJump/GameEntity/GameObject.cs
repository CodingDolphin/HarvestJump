using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    public enum Status
    {
        activ,
        inactiv
    }

    abstract class GameObject : ICollide
    {
        //Klassenvariablen

        protected Vector2 gravity { get; set; }
        public Vector2 velocity { get; set; }
        protected Vector2 friction { get; set; }
        protected Sprite currentSprite { get; set; }
        protected Sprite leftSprite { get; set; }
        protected Sprite rightSprite { get; set; }
        protected double deltaTime { get; set; }
        public double slowMotion { get; set; }

        //ICollide Interface

        public bool noClip { get; set; }
        public Vector2 oldPosition { get; set; }
        public Vector2 position { get; set; }
        public BoundingBox boundingBox { get; set; }
        public bool isJumping { get; set; }

        //Konstruktors

        public GameObject()
        {
        }

        public GameObject(Vector2 position,int width, int height)
        {
            this.position = position;
            boundingBox = new BoundingBox(position, width, height);
            slowMotion = 1;
            noClip = false;
            gravity = new Vector2(0, 2000);
        }
        
        public abstract void LoadContent(ContentManager content, string assetName);

        public virtual void Update(GameTime gameTime)
        {
            deltaTime = gameTime.ElapsedGameTime.TotalSeconds / slowMotion;

            ApplyForce();
            ApplyVelocityToPosition();
            boundingBox = new BoundingBox(position, boundingBox.width, boundingBox.height);

            currentSprite.Update(gameTime);
            UpdateAnimation();
        }

        public virtual void ApplyForce()
        {
            velocity *= new Vector2(0.95f, 0.98f);
            velocity += gravity * (float)deltaTime;
        }

        public virtual void ApplyVelocityToPosition()
        {
            position += velocity * (float)deltaTime;
        }

        public virtual void HandleCollision(ICollide collisionObject)
        {
            float penetrationTop = position.Y + boundingBox.height - collisionObject.boundingBox.position.Y;
            float penetrationLeft = position.X + boundingBox.width - collisionObject.boundingBox.position.X;
            float penetrationBottom = collisionObject.boundingBox.position.Y + collisionObject.boundingBox.height - position.Y;
            float penetrationRight = collisionObject.boundingBox.position.X + collisionObject.boundingBox.width - position.X;

            float lowestPenetation = CollisionHelper.getLowestNumber(penetrationTop, penetrationLeft, penetrationBottom, penetrationRight);

            float edgeThreshold = 2f;

            float lowestPenetrationX = Math.Min(penetrationLeft, penetrationRight);
            float lowestPenetrationY = Math.Min(penetrationTop, penetrationBottom);

            if (Math.Max(lowestPenetrationX, lowestPenetrationY) <= edgeThreshold)
                return;

            if (lowestPenetation == penetrationTop)
            {
                position = new Vector2(position.X, position.Y - penetrationTop);
                velocity = new Vector2(velocity.X, 0);
            }
            else if (lowestPenetation == penetrationLeft)
            {
                position = new Vector2(position.X - penetrationLeft, position.Y);
                velocity = new Vector2(0, velocity.Y);
            }
            else if (lowestPenetation == penetrationRight)
            {
                position = new Vector2(position.X + penetrationRight, position.Y);
                velocity = new Vector2(0, velocity.Y);
            }
            else if (lowestPenetation == penetrationBottom)
            {
                position = new Vector2(position.X, position.Y + penetrationBottom);
                velocity = new Vector2(velocity.X, 0);
            }

            boundingBox = new BoundingBox(position, boundingBox.width, boundingBox.height);
            isJumping = false;
        }

        public void UpdateAnimation()
        {
            currentSprite.position = position;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
