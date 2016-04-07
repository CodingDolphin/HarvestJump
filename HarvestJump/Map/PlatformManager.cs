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
        public int lastestPositionX { get; set; }
        public int latestPositionY { get; set; }
        private int tileWidth { get; set; }
        private int tileHeight { get; set; }

        public PlatformManager(int tileWidth, int tileHeight)
        {
            platformList = new List<Platform>();
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Platform platform in platformList)
            {
                platform.LoadContent(content, "GraphicAssets/MapAssets/GrassPlatformSheet");
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Platform item in platformList)
            {
                if (item is MovingPlatform)
                    item.Update(gameTime);

            }
        }

        public void CreatePlatform(Vector2 position, int platformWidth, int platformHeight)
        {
            platformList.Add(new Platform(position, platformWidth, platformHeight, tileWidth, tileHeight));
            calculateFurthestPositions();
        }

        public void CreateMovingPlatform(Vector2 position, int platformWidth, int platformHeight, int moveSpeed, int distance, isMoving moveDirection)
        {
            platformList.Add(new MovingPlatform(position, platformWidth, platformHeight, tileWidth, tileHeight, moveSpeed, distance, moveDirection));
            calculateFurthestPositions();
        }

        public void calculateFurthestPositions()
        {
            int lastIndex = platformList.Count - 1;
            if (furthestPositionX < platformList[lastIndex].platformWidth * tileWidth)
                furthestPositionX = platformList[lastIndex].platformWidth * tileWidth;
            if (furthestPositionY < platformList[lastIndex].platformHeight * tileWidth)
                furthestPositionY = platformList[lastIndex].platformHeight * tileWidth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Platform platform in platformList)
            {
                platform.Draw(spriteBatch);
            }
        }
    }
}
