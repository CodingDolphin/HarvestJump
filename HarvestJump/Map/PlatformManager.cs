using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace HarvestJump
{
    class PlatformManager
    {
        public List<Platform> platformList { get; set; }
        public int furthestPositionX { get; set; }
        public int furthestPositionY { get; set; }
        private int tileWidth { get; set; }
        private int tileHeight { get; set; }

        public PlatformManager(int tileWidth, int tileHeight)
        {
            platformList = new List<Platform>();
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }

        public void CreatePlatform(Vector2 position, int platformWidth, int platformHeight)
        {
            platformList.Add(new Platform(position, platformWidth, platformHeight, tileWidth, tileHeight));
        }

        public void CreateMovingPlatform(Vector2 position, int platformWidth, int platformHeight, int moveSpeed, int distance, isMoving moveDirection)
        {
            platformList.Add(new MovingPlatform(position, platformWidth, platformHeight, tileWidth, tileHeight, moveSpeed, distance, moveDirection));
        }
    }
}
