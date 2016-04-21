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
        right,
        left,
    }

    abstract class GameObject : ICollide, IFocus , ITarget
    {
        //Klassenvariablen

        protected Vector2 gravity { get; set; }
        protected Vector2 friction { get; set; }
        protected Animation currentAnimation { get; set; }
        protected Dictionary<AnimationStatus, Tuple<Animation, BoundingBox>> stateData { get; set; }
        public Direction direction { get; set; }
        protected double deltaTime { get; set; }
        protected Vector2 speed { get; set; }
        protected Vector2 jumpStrength { get; set; }
        public static double slowMotion { get; set; }
        public bool isJumping { get; set; }

        //ICollide Interface

        public Vector2 position { get; set; }
        public BoundingBox boundingBox { get; set; }
        public Vector2 velocity { get; set; }
        public bool noClip { get; set; }


        //Testing

        public Sprite debugRectangle { get; set; }
        protected Vector2 boxXTranslate { get; set; }
        protected SpriteFont debugFont { get; set; }

        //Konstruktoren

        public GameObject()
        {
        }

        public GameObject(Vector2 position, int width, int height)
        {
            this.debugRectangle = new Sprite(position);
            this.stateData = new Dictionary<AnimationStatus, Tuple<Animation, BoundingBox>>();
            this.position = position;
            this.boundingBox = new BoundingBox(position, width, height);
            this.noClip = false;
            this.gravity = new Vector2(0, 2000);
            this.boxXTranslate = Vector2.Zero;

            GameObject.slowMotion = 1;
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

        public void ApplyForce()
        {
            velocity *= new Vector2(0.95f, 0.98f);
            velocity += gravity * (float)deltaTime;
        }

        public void ApplyVelocityToPosition()
        {
            position += velocity * (float)deltaTime;
        }

        public void CreateBoundingBox()
        {
            boundingBox = new BoundingBox(Vector2.Add(position, boxXTranslate), boundingBox.width, boundingBox.height);
        }

        public void HandleCollision(ICollide collisionObject)      
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
                stateData[AnimationStatus.idle].Item1.position = position;
                stateData[AnimationStatus.idle].Item1.direction = currentAnimation.direction;
                currentAnimation = stateData[AnimationStatus.idle].Item1;
            }
        }

        public void AddState(AnimationStatus status, Vector2 position, int index, int frameWidth, int frameHeight, float frameCycle, int frameCount,bool isLooping, int width, int height)
        {
            stateData.Add(status, new Tuple<Animation, BoundingBox>(new Animation(position, index, frameWidth, frameHeight, frameCycle, frameCount, isLooping),
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
                    boxXTranslate = new Vector2(currentAnimation.frameWidth - boundingBox.width, 0);
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

            if(Game1.debug == true)
            {
                spriteBatch.Draw(debugRectangle.texture, boundingBox.position, new Rectangle((int)boundingBox.position.X, (int)boundingBox.position.Y, (int)boundingBox.width , (int)boundingBox.height), new Color(1, 1, 1, 0.5f));
                spriteBatch.DrawString(debugFont, CreateString(this), boundingBox.position, Color.Red);
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
