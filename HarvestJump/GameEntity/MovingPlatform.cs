using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HarvestJump.GameEntity
{
    class MovingPlatform : Platform
    {
        public int pathX { get; set; }
        public int pathY { get; set; }
        public int moveSpeed { get; set; }
        public bool isMoving { get; set; }

        public MovingPlatform(Vector2 position, int platformWidth, int platformHeight, int tileWidth, int tileHeight, int pathX, int pathY, int moveSpeed, bool isMoving) : base(position, platformWidth, platformHeight, tileWidth, tileHeight)
        {
            this.pathX = pathX;
            this.pathY = pathY;
            this.moveSpeed = moveSpeed;
        }
    }
}
