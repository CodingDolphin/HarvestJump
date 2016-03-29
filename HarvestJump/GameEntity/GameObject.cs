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
        public Vector2 friction { get; set; }
        public double deltaTime { get; set; }
        public double slowMotion { get; set; }
        public bool hasContact { get; set; }

        //ICollide Interface

        public bool noClip { get; set; }
        public Vector2 oldPosition { get; set; }
        public Vector2 position { get; set; }
        public Rectangle boundingRectangle { get; set; }
        public bool isJumping;

        public GameObject(Vector2 position,int width, int height)
        {
            this.position = position;
            createBondingRectangle(position, width, height);
            slowMotion = 1;
            noClip = false;
            gravity = new Vector2(0, 3000);
        }

        public abstract void LoadContent(ContentManager content, string assetName);

        public virtual void Update(GameTime gameTime)
        {
            deltaTime = gameTime.ElapsedGameTime.TotalSeconds / slowMotion;
            oldPosition = new Vector2(position.X, position.Y);

            ApplyForce();
            ApplyVelocityToPosition();
            createBondingRectangle(position, boundingRectangle.Width, boundingRectangle.Height);
        }

        public virtual void ApplyForce()
        {
            velocity += gravity * (float)deltaTime;
        }

        public virtual void createBondingRectangle(Vector2 position, int width, int height)
        {
            boundingRectangle = new Rectangle((int)position.X,(int)position.Y, width, height);
        }

        public virtual void ApplyVelocityToPosition()
        {
            velocity *= new Vector2(0.9f, 0.9f);
            position += velocity * (float)deltaTime;
        }

        public virtual void HandleCollision(ICollide collisionObject)
        {
            velocity = new Vector2(velocity.X, 0);
            position = new Vector2(position.X, collisionObject.boundingRectangle.Y);
            createBondingRectangle(position, boundingRectangle.Width, boundingRectangle.Height);
            isJumping = false;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
