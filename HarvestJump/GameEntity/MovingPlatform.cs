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
        public int moveSpace { get; set; }
        public int endPosition { get; set; }
        public int startPosition { get; set; }
        public int moveSpeed { get; set; }

        public MovingPlatform(Vector2 position, int platformWidth, int platformHeight, int tileWidth, int tileHeight, int moveSpeed, int moveSpace, isMoving moveDirection) : base(position, platformWidth, platformHeight, tileWidth, tileHeight)
        {
            this.moveSpace = moveSpace;
            this.moveSpeed = moveSpeed;
            this.moveDirection = moveDirection;

            CreateWayPoints();
        }

        public override void Update(GameTime gameTime)
        {
            movePlatform();
            boundingBox = new BoundingBox(position, boundingBox.width, boundingBox.height);
        }

        public void CreateWayPoints()
        {
            switch (moveDirection)
            {
                case isMoving.horizontal:
                    endPosition = (int)position.X + moveSpace;
                    startPosition = (int)position.X - moveSpace; break;
                case isMoving.vertical:
                    endPosition = (int)position.Y + moveSpace;
                    startPosition = (int)position.Y - moveSpace; break;
                case isMoving.none: break;
                default: break;
            }
        }

        public void movePlatform()
        {
            if(moveDirection == isMoving.horizontal)
            position = new Vector2(position.X + moveSpeed, position.Y);
            else
                position = new Vector2(position.X, position.Y + moveSpeed);

            if (position.X > endPosition | position.X <= startPosition && moveDirection == isMoving.horizontal)
                moveSpeed = moveSpeed * -1;
            else if (position.Y > endPosition | position.Y <= startPosition && moveDirection == isMoving.vertical)
                moveSpeed = moveSpeed * -1;

            UpdateSprite();
        }

        public void UpdateSprite()
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

