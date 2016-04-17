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
        //TEST
        protected Vector2 boxXTranslate { get; set; }
        protected SpriteFont debugFont { get; set; }

        //Klassenvariablen

        protected Vector2 gravity { get; set; }
        public Vector2 velocity { get; set; }
        protected Vector2 friction { get; set; }
        protected Animation currentAnimation { get; set; }
        protected Dictionary<AnimationStatus, Tuple<Animation, BoundingBox>> animationDictionary { get; set; }
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

        public GameObject(Vector2 position, int width, int height)
        {
            debugRectangle = new Sprite(position);
            animationDictionary = new Dictionary<AnimationStatus, Tuple<Animation, BoundingBox>>();
            direction = Direction.right;
            this.position = position;
            boundingBox = new BoundingBox(position, width, height);
            slowMotion = 1;
            noClip = false;
            gravity = new Vector2(0, 2000);

            boxXTranslate = Vector2.Zero;
        }
        
        public virtual void LoadContent(ContentManager content, string assetName)
        {
            debugRectangle.LoadContent(content, "blackPixel");
            debugFont = content.Load<SpriteFont>("Fonts/LeagueFont");
        }

        public virtual void Update(GameTime gameTime)
        {
            deltaTime = gameTime.ElapsedGameTime.TotalSeconds / slowMotion;

            ApplyForce();
            ApplyVelocityToPosition();

            CreateBoundingBox();
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

        public void CreateBoundingBox()
        {
            boundingBox = new BoundingBox(Vector2.Add(position, boxXTranslate), boundingBox.width, boundingBox.height);
        }

        //TODO Variable einbauen um Velocity zu berechnen oder nicht.

        public virtual void HandleCollision(ICollide collisionObject)      
        {
            float penetrationTop = boundingBox.position.Y + boundingBox.height - collisionObject.boundingBox.position.Y;
            float penetrationLeft = boundingBox.position.X + boundingBox.width - collisionObject.boundingBox.position.X;
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


            CreateBoundingBox();
            isJumping = false;
     
            if (this is Player)
            {
                animationDictionary[AnimationStatus.idle].Item1.position = currentAnimation.position;
                animationDictionary[AnimationStatus.idle].Item1.direction = currentAnimation.direction;
                currentAnimation = animationDictionary[AnimationStatus.idle].Item1;
            }
            //else
            //{
            //    animationDictionary[AnimationStatus.idle].Item1.position = currentAnimation.position;
            //    animationDictionary[AnimationStatus.idle].Item1.direction = currentAnimation.direction;
            //    currentAnimation = animationDictionary[AnimationStatus.walking].Item1;
            //}
        }

        public void AddAnimation(AnimationStatus status, Vector2 position, int index, int frameWidth, int frameHeight, float frameCycle, int frameCount, int width, int height)
        {
            animationDictionary.Add(status, new Tuple<Animation, BoundingBox>(new Animation(position, index, frameWidth, frameHeight, frameCycle, frameCount),
                                            new BoundingBox(position, width, height)));
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            currentAnimation.Update(gameTime);
            currentAnimation.position = position;
        }

        protected void SetDirection(Direction dir)
        {
            Vector2 flipTranslate = new Vector2(-(currentAnimation.frameWidth / 2), 0.0f);

            if (direction != dir)
            {
                if (direction == Direction.right)
                {
                    currentAnimation.direction = SpriteEffects.FlipHorizontally;
                    this.boxXTranslate = new Vector2(currentAnimation.frameWidth - boundingBox.width, 0);
                    position += flipTranslate;
                }
                else if (direction == Direction.left)
                {
                    position -= flipTranslate;
                    currentAnimation.direction = SpriteEffects.None;
                    boxXTranslate = Vector2.Zero;
                }
            }

            direction = dir;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);

            if(this is Tile)
            {
                spriteBatch.Draw(debugRectangle.texture, boundingBox.position, new Rectangle((int)boundingBox.position.X, (int)boundingBox.position.Y, (int)boundingBox.width , (int)boundingBox.height), new Color(1, 1, 1, 0.5f));
                spriteBatch.DrawString(debugFont, CreateString(this), Vector2.Zero, Color.Red);
            }
        }

        private string CreateString(GameObject gameObject)
        {
            string debugString = "BBPosX: " + String.Format("{0:0}", boundingBox.position.X) + "\n" + "BBPosY: " + String.Format("{0:0}", boundingBox.position.Y)
                               + "\n" + "BBWidth: " + String.Format("{0:0}", boundingBox.width) + "\n" + "BBHeight: " + String.Format("{0:0}", boundingBox.height)
                               + "\n" + "PosX: " + String.Format("{0:0}", position.X) + "\n" + "PosY: " + String.Format("{0:0}", position.Y);

            return debugString;
        }

    }
}
