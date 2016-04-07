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
                    endPosition = moveSpace + (int)position.X;
                    startPosition = (int)position.X - moveSpace; break;
                case isMoving.vertical:
                    break;
                case isMoving.none:
                    break;
                default:
                    break;
            }
        }

        public void movePlatform()
        {
            position = new Vector2(position.X + moveSpeed, position.Y);

            if (position.X > endPosition | position.X <= startPosition)
                moveSpeed = moveSpeed * -1;


            for (int x = 0; x < platformWidth; x++)
            {
                tileList[x].position = new Vector2(position.X + x * tileWidth, position.Y);
                tileList[x].tileSprite.position = new Vector2(position.X + x * tileWidth, position.Y);
            }
        }
    }
}
