using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    public enum AnimationStatus
    {
        walking,
        jumping,
        atacking,
        idle,
        flee,
        slide,
        run,
        hurt,
        dead,
    }

    public enum Direction
    {
        left,
        right,
    }

    abstract class GameObject : ICollide
    {
        //Klassenvariablen

        protected Vector2 gravity { get; set; }
        public Vector2 velocity { get; set; }
        protected Vector2 friction { get; set; }
        protected Animation currentAnimation { get; set; }
        protected Dictionary<AnimationStatus, Animation> animationDictionary { get; set; }
        protected Direction direction { get; set; }
        protected double deltaTime { get; set; }
        public double slowMotion { get; set; }

        //ICollide Interface

        public bool noClip { get; set; }
        public Vector2 position { get; set; }
        public BoundingBox boundingBox { get; set; }
        public bool isJumping { get; set; }

        //Testing

        public Sprite debugRectangle;

        //Konstruktors

        public GameObject()
        {
        }

        public GameObject(Vector2 position,int width, int height)
        {
            animationDictionary = new Dictionary<AnimationStatus, Animation>();
            direction = Direction.right;
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

            UpdateAnimation(gameTime);
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

        //TODO Variable einbauen um Velocity zu berechnen oder nicht.

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
                position = new Vector2(position.X + collisionObject.velocity.X, position.Y - penetrationTop + collisionObject.velocity.Y);
                velocity = new Vector2(velocity.X, 0);
            }
            else if (lowestPenetation == penetrationLeft)
            {
                position = new Vector2(position.X - penetrationLeft + collisionObject.velocity.X, position.Y + collisionObject.velocity.Y);
                velocity = new Vector2(0, velocity.Y);
            }
            else if (lowestPenetation == penetrationRight)
            {
                position = new Vector2(position.X + penetrationRight + collisionObject.velocity.X, position.Y + collisionObject.velocity.Y);
                velocity = new Vector2(0, velocity.Y);
            }
            else if (lowestPenetation == penetrationBottom)
            {
                position = new Vector2(position.X + collisionObject.velocity.X, position.Y + penetrationBottom + collisionObject.velocity.Y);
                velocity = new Vector2(velocity.X, 0);
            }

            boundingBox = new BoundingBox(position, boundingBox.width, boundingBox.height);
            isJumping = false;

            if(this is Player)
            {
                animationDictionary[AnimationStatus.idle].position = currentAnimation.position;
                animationDictionary[AnimationStatus.idle].direction = currentAnimation.direction;
                currentAnimation = animationDictionary[AnimationStatus.idle];
            }
            else
            {
                animationDictionary[AnimationStatus.idle].position = currentAnimation.position;
                animationDictionary[AnimationStatus.idle].direction = currentAnimation.direction;
                currentAnimation = animationDictionary[AnimationStatus.walking];
            }

        }

        public void AddAnimation(AnimationStatus status, Vector2 position, int index, int frameWidth, int frameHeight, float frameCycle, int frameCount)
        {
            animationDictionary.Add(status, new Animation(position, index, frameWidth, frameHeight, frameCycle, frameCount));
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            currentAnimation.Update(gameTime);
            currentAnimation.position = position;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
