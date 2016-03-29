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
        protected Sprite sprite { get; set; }
        public double deltaTime { get; set; }
        public double slowMotion { get; set; }
        public bool hasContact { get; set; }

        //ICollide Interface

        public bool noClip { get; set; }
        public Vector2 oldPosition { get; set; }
        public Vector2 position { get; set; }
        public Rectangle boundingRectangle { get; set; }
        public bool isJumping;

        //Konstruktor

        public GameObject()
        {
        }

        public GameObject(Vector2 position,int width, int height)
        {
            this.position = position;
            createBondingRectangle(position, width, height);
            slowMotion = 1;
            noClip = false;
            gravity = new Vector2(0, 2000);
        }

        public abstract void LoadContent(ContentManager content, string assetName);

        public virtual void Update(GameTime gameTime)
        {
            deltaTime = gameTime.ElapsedGameTime.TotalSeconds / slowMotion;
            oldPosition = new Vector2(position.X, position.Y);

            ApplyForce();
            ApplyVelocityToPosition();
            createBondingRectangle(position, boundingRectangle.Width, boundingRectangle.Height);
            UpdateAnimation(gameTime);
        }

        public virtual void ApplyForce()
        {
            velocity *= new Vector2(0.95f, 0.98f);
            velocity += gravity * (float)deltaTime;
        }

        public virtual void createBondingRectangle(Vector2 position, int width, int height)
        {
            boundingRectangle = new Rectangle((int)position.X,(int)position.Y, width, height);
        }

        public virtual void ApplyVelocityToPosition()
        {
            position += velocity * (float)deltaTime;
        }

        public virtual void HandleCollision(ICollide collisionObject)
        {
            float penetrationTop = position.Y + boundingRectangle.Height - collisionObject.boundingRectangle.Y;
            float penetrationLeft = position.X + boundingRectangle.Width - collisionObject.boundingRectangle.X;
            float penetrationBottom = collisionObject.boundingRectangle.Y + collisionObject.boundingRectangle.Height - position.Y;
            float penetrationRight = collisionObject.boundingRectangle.X + collisionObject.boundingRectangle.Width - position.X;

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

            sprite.position = new Vector2(position.X, position.Y);
            createBondingRectangle(position, boundingRectangle.Width, boundingRectangle.Height);
            isJumping = false;
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            sprite.position = position;
            sprite.Update(gameTime);
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
