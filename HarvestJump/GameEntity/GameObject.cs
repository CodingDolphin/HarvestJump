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

        protected Vector2 Gravity { get; set; } = new Vector2(0, 2000);
        protected Vector2 Friction { get; set; } = new Vector2(0.95f, 0.98f);
        protected Dictionary<AnimationStatus, Tuple<Animation, BoundingBox>> StateData { get; set; }
        protected Animation CurrentAnimation { get; set; }
        protected Vector2 Speed { get; set; }
        protected Vector2 JumpStrength { get; set; }
        public bool IsJumping { get; set; }
        protected double DeltaTime { get; set; }

        //Interfaces

        public Vector2 Position { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public Vector2 Velocity { get; set; }
        public bool NoClip { get; set; }

        //Debug

        public Sprite DebugRectangle { get; set; }
        protected SpriteFont DebugFont { get; set; }

        private Direction direction;
        public Direction Direction
        {
            get { return direction; }

            set
            {
                if (value == Direction.left && value != direction)
                {
                    Position = new Vector2(Position.X - CurrentAnimation.width + CurrentAnimation.rotationPoint.X * 2, Position.Y);
                    CurrentAnimation.Direction = Direction.left;
                }
                else if(value == Direction.right && value != direction)
                {
                    Position = new Vector2(Position.X + CurrentAnimation.width - CurrentAnimation.rotationPoint.X * 2, Position.Y);
                    CurrentAnimation.Direction = Direction.right;
                }
                direction = value;
            }
        }

        //Konstruktoren

        public GameObject()
        {
        }

        public GameObject(Vector2 position, int width = 0, int height = 0)
        {
            this.StateData = new Dictionary<AnimationStatus, Tuple<Animation, BoundingBox>>();
            this.DebugRectangle = new Sprite(position);
            this.Position = position;
            this.BoundingBox = new BoundingBox(position, width, height);
            this.NoClip = false;
        }
        
        public virtual void LoadContent(ContentManager content, string assetName)
        {
            DebugRectangle.LoadContent(content, "blackPixel");
            DebugFont = content.Load<SpriteFont>("Fonts/LeagueFont");
        }

        public virtual void Update(GameTime gameTime)
        {
            DeltaTime = gameTime.ElapsedGameTime.TotalSeconds / slowMotion;

            ApplyForce();
            ApplyVelocityToPosition();
            CreateBoundingBox();

            CurrentAnimation.Update(gameTime);
            UpdateCurrentAnimationPosition();
        }

        protected void ApplyForce()
        {
            Velocity *= Friction;
            Velocity += Gravity * (float)DeltaTime;
        }

        protected void ApplyVelocityToPosition()
        {
            Position += Velocity * (float)DeltaTime;
        }

        protected void CreateBoundingBox()
        {
            if(direction == Direction.right)
            BoundingBox = new BoundingBox(Position, BoundingBox.width, BoundingBox.height);
            if (direction == Direction.left)
                BoundingBox = new BoundingBox(Vector2.Add(Position, new Vector2(CurrentAnimation.width - BoundingBox.width, 0)), BoundingBox.width, BoundingBox.height);
        }

        public void HandleCollision(ICollide collisionObject)      
        {
            float penetrationTop = BoundingBox.position.Y + BoundingBox.height - collisionObject.BoundingBox.position.Y;
            float penetrationLeft = BoundingBox.position.X + BoundingBox.width - collisionObject.BoundingBox.position.X;
            float penetrationBottom = collisionObject.BoundingBox.position.Y + collisionObject.BoundingBox.height - Position.Y;
            float penetrationRight = collisionObject.BoundingBox.position.X + collisionObject.BoundingBox.width - Position.X;
            float lowestPenetation = CollisionHelper.getLowestNumber(penetrationTop, penetrationLeft, penetrationBottom, penetrationRight);

            float edgeThreshold = 2f;
            float lowestPenetrationX = Math.Min(penetrationLeft, penetrationRight);
            float lowestPenetrationY = Math.Min(penetrationTop, penetrationBottom);
            if (Math.Max(lowestPenetrationX, lowestPenetrationY) <= edgeThreshold)
                return;

            if (lowestPenetation == penetrationTop)
            {
                Position = new Vector2(Position.X + collisionObject.Velocity.X, Position.Y - penetrationTop + collisionObject.Velocity.Y);
                Velocity = new Vector2(Velocity.X, 0);
            }
            else if (lowestPenetation == penetrationLeft)
            {
                Position = new Vector2(Position.X - penetrationLeft + collisionObject.Velocity.X, Position.Y + collisionObject.Velocity.Y);
                Velocity = new Vector2(0, Velocity.Y);
            }
            else if (lowestPenetation == penetrationRight)
            {
                Position = new Vector2(Position.X + penetrationRight + collisionObject.Velocity.X, Position.Y + collisionObject.Velocity.Y);
                Velocity = new Vector2(0, Velocity.Y);
            } 
            else if (lowestPenetation == penetrationBottom)
            {
                Position = new Vector2(Position.X + collisionObject.Velocity.X, Position.Y + penetrationBottom + collisionObject.Velocity.Y);
                Velocity = new Vector2(Velocity.X, 0);
            }

            IsJumping = false;

            if (this is Player)
            {
                if (Velocity.X >= 10 && direction == Direction.right)
                    SwitchAnimation(AnimationStatus.walking);

                if (Velocity.X <= -10 && direction == Direction.left)
                    SwitchAnimation(AnimationStatus.walking);

                if (Velocity.X >= 300 && direction == Direction.right)
                    SwitchAnimation(AnimationStatus.run);

                if (Velocity.X <= -300 && direction == Direction.left)
                    SwitchAnimation(AnimationStatus.run);

                if (Velocity.X <= 10 && Direction == Direction.right)
                    SwitchAnimation(AnimationStatus.idle);

                if (Velocity.X >= -10 && Direction == Direction.left)
                    SwitchAnimation(AnimationStatus.idle);
            }

            UpdateCurrentAnimationPosition();
            CreateBoundingBox();
        }

        public void SwitchAnimation(AnimationStatus animation)
        {
            StateData[animation].Item1.position = Position;
            StateData[animation].Item2.position = Position;
            StateData[animation].Item1.Direction = direction;

            CurrentAnimation = StateData[animation].Item1;
            BoundingBox = StateData[animation].Item2;       
        }

        public void AddState(AnimationStatus status, Vector2 position, float rotationPointX, int index, int frameWidth, int frameHeight, float frameCycle, int frameCount,bool isLooping, int width, int height)
        {
            StateData.Add(status, new Tuple<Animation, BoundingBox>(new Animation(position, index, frameWidth, frameHeight, frameCycle, frameCount, isLooping, new Vector2(rotationPointX, 0)),
                                  new BoundingBox(position, width, height)));
        }

        protected void UpdateCurrentAnimationPosition()
        {
            CurrentAnimation.position = Position;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            CurrentAnimation.Draw(spriteBatch);

            if(Game1.debug == true)
            {
                spriteBatch.Draw(DebugRectangle.texture, BoundingBox.position, new Rectangle((int)BoundingBox.position.X, (int)BoundingBox.position.Y, (int)BoundingBox.width , (int)BoundingBox.height), new Color(1, 1, 1, 0.5f));
                spriteBatch.DrawString(DebugFont, CreateString(this), BoundingBox.position, Color.Red);
            }
        }

        private string CreateString(GameObject gameObject)
        {
            string debugString = "BBPosX: " + String.Format("{0:0}", BoundingBox.position.X) + "\n" + "BBPosY: " + String.Format("{0:0}", BoundingBox.position.Y)
                               + "\n" + "BBWidth: " + String.Format("{0:0}", BoundingBox.width) + "\n" + "BBHeight: " + String.Format("{0:0}", BoundingBox.height)
                               + "\n" + "PosX: " + String.Format("{0:0}", Position.X) + "\n" + "PosY: " + String.Format("{0:0}", Position.Y)
                               + "\n" + "Velocity: " + String.Format("{0:0}", Velocity.X);

            return debugString;
        }
    }
}