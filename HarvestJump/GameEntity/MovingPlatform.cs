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
            boundingBox = new BoundingBox(position, boundingBox.width, boundingBox.height);
        }

        public void CreateWayPoints()
        {
            switch (moveDirection)
            {
                case IsMoving.horizontal:
                    endPosition = (int)position.X + distance;
                    startPosition = (int)position.X - distance; break;
                case IsMoving.vertical:
                    endPosition = (int)position.Y + distance;
                    startPosition = (int)position.Y - distance; break;
            }
        }

        public void MovePlatform()
        {
            if (moveDirection == IsMoving.horizontal)
                position += velocity;
            else
                position += velocity;

            if (position.X > endPosition | position.X < startPosition && moveDirection == IsMoving.horizontal)
                velocity = velocity * -1;
            else if (position.Y > endPosition | position.Y < startPosition && moveDirection == IsMoving.vertical)
                velocity = velocity * -1;

            UpdateTiles();
        }

        public void CreateVelocityVector(int moveSpeed)
        {
            switch (moveDirection)
            {
                case IsMoving.horizontal: velocity = new Vector2(moveSpeed, 0);
                    break;
                case IsMoving.vertical: velocity = new Vector2(0 , moveSpeed);
                    break;
                case IsMoving.directional: velocity = new Vector2(moveSpeed, moveSpeed);
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
                    tileList[x].position = new Vector2(position.X + x * tileWidth, position.Y + y * tileHeight);
                    tileList[x].tileSprite.position = new Vector2(position.X + x * tileWidth, position.Y + y * tileHeight);
                }
            }
        }
    }
 }

