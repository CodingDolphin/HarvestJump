using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    public enum State
    {
        active,
        inactive,
    }

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

        public static double slowMotion { get; set; } = 1;

        //Properties

        protected Vector2 gravity { get; set; }
        protected Vector2 friction { get; set; }
        protected Animation currentAnimation { get; set; }
        protected Dictionary<AnimationStatus, Tuple<Animation, BoundingBox>> stateData { get; set; }
        protected double deltaTime { get; set; }
        protected Vector2 speed { get; set; }
        protected Vector2 jumpStrength { get; set; }
        public bool isJumping { get; set; }
        public State state { get; set; }

        //Interfaces

        public Vector2 position { get; set; }
        public BoundingBox boundingBox { get; set; }
        public Vector2 velocity { get; set; }
        public bool noClip { get; set; }

        //Testing

        public Sprite debugRectangle { get; set; }
        protected Vector2 boxXTranslate { get; set; }
        protected SpriteFont debugFont { get; set; }

        private Direction direction;
        public Direction Direction
        {
            get { return direction; }

            set
            {
                if (value == Direction.left && value != direction)
                {
                    position = new Vector2(position.X - currentAnimation.frameWidth + currentAnimation.rotationPoint.X * 2, position.Y);
                    currentAnimation.Direction = Direction.left;
                }
                else if(value == Direction.right && value != direction)
                {
                    position = new Vector2(position.X + currentAnimation.frameWidth - currentAnimation.rotationPoint.X * 2, position.Y);
                    currentAnimation.Direction = Direction.right;
                }
                direction = value;
            }
        }

        //Konstruktoren

        public GameObject()
        {
        }

        public GameObject(Vector2 position, int width, int height)
        {
            this.stateData = new Dictionary<AnimationStatus, Tuple<Animation, BoundingBox>>();
            this.debugRectangle = new Sprite(position);
            this.position = position;
            this.boundingBox = new BoundingBox(position, width, height);
            this.noClip = false;
            this.gravity = new Vector2(0, 2000);
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

            currentAnimation.Update(gameTime);
            UpdateAnimation();
        }

        protected void ApplyForce()
        {
            velocity *= new Vector2(0.95f, 0.98f);
            velocity += gravity * (float)deltaTime;
        }

        protected void ApplyVelocityToPosition()
        {
            position += velocity * (float)deltaTime;
        }

        protected void CreateBoundingBox()
        {
            if(direction == Direction.right)
            boundingBox = new BoundingBox(position, boundingBox.width, boundingBox.height);
            if (direction == Direction.left)
                boundingBox = new BoundingBox(Vector2.Add(position, new Vector2(currentAnimation.frameWidth - boundingBox.width, 0)), boundingBox.width, boundingBox.height);
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

            isJumping = false;
            UpdateAnimation();
        }

        public void SwitchAnimation(AnimationStatus animation)
        {
            currentAnimation = stateData[animation].Item1;
            boundingBox = stateData[animation].Item2;
        }

        public void AddState(AnimationStatus status, Vector2 position, int index, int frameWidth, int frameHeight, float frameCycle, int frameCount,bool isLooping, int width, int height)
        {
            stateData.Add(status, new Tuple<Animation, BoundingBox>(new Animation(position, index, frameWidth, frameHeight, frameCycle, frameCount, isLooping),
                                            new BoundingBox(position, width, height)));
        }

        protected void UpdateAnimation()
        {
            currentAnimation.position = position;
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
