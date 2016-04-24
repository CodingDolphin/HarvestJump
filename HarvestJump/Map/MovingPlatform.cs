using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HarvestJump
{
    class MovingPlatform : Platform
    {
        public int distance { get; set; }
        public int endPosition { get; set; }
        public int startPosition { get; set; }

        public MovingPlatform(Vector2 position, int platformWidth, int platformHeight, int tileWidth, int tileHeight, int moveSpeed, int distance, IsMoving moveDirection) : base(position, platformWidth, platformHeight, tileWidth, tileHeight)
        {
            this.distance = distance;
            this.moveDirection = moveDirection;

            CreateVelocityVector(moveSpeed);
            CreateWayPoints();
        }

        public override void Update(GameTime gameTime)
        {
            MovePlatform();
            BoundingBox = new BoundingBox(Position, BoundingBox.width, BoundingBox.height);
        }

        public void CreateWayPoints()
        {
            switch (moveDirection)
            {
                case IsMoving.horizontal:
                    endPosition = (int)Position.X + distance;
                    startPosition = (int)Position.X - distance; break;
                case IsMoving.vertical:
                    endPosition = (int)Position.Y + distance;
                    startPosition = (int)Position.Y - distance; break;
            }
        }

        public void MovePlatform()
        {
            if (moveDirection == IsMoving.horizontal)
                Position += Velocity;
            else
                Position += Velocity;

            if (Position.X > endPosition | Position.X < startPosition && moveDirection == IsMoving.horizontal)
                Velocity = Velocity * -1;
            else if (Position.Y > endPosition | Position.Y < startPosition && moveDirection == IsMoving.vertical)
                Velocity = Velocity * -1;

            UpdateTiles();
        }

        public void CreateVelocityVector(float moveSpeed)
        {
            switch (moveDirection)
            {
                case IsMoving.horizontal: Velocity = new Vector2(moveSpeed, 0);
                    break;
                case IsMoving.vertical: Velocity = new Vector2(0 , moveSpeed);
                    break;
                case IsMoving.directional: Velocity = new Vector2(moveSpeed, moveSpeed);
                    break;
                case IsMoving.none:
                    break;
                default:
                    break;
            }
        }

        public void UpdateTiles()
        {
            for (int y = 0; y < platformHeight; y++)
            {
                for (int x = 0; x < tileList.Count; x++)
                {
                    tileList[x].Position = new Vector2(Position.X + x * tileWidth, Position.Y + y * tileHeight);
                    tileList[x].tileSprite.position = new Vector2(Position.X + x * tileWidth, Position.Y + y * tileHeight);
                }
            }
        }
    }
 }

