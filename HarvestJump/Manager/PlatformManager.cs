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
        public int latestPositionX { get; set; }
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
            foreach (Platform platform in platformList)
            {
                if (platform is MovingPlatform)
                    platform.Update(gameTime);

            }
        }

        public void CreatePlatform(Vector2 position, int platformWidth, int platformHeight)
        {
            platformList.Add(new Platform(position, platformWidth, platformHeight, tileWidth, tileHeight));
            calculateFurthestDistance();

        }

        public void CreateMovingPlatform(Vector2 position, int platformWidth, int platformHeight, int moveSpeed, int distance, IsMoving moveDirection)
        {
            platformList.Add(new MovingPlatform(position, platformWidth, platformHeight, tileWidth, tileHeight, moveSpeed, distance, moveDirection));
            calculateFurthestDistance(distance);
        }

        public void calculateFurthestDistance(int distance = 0)
        {
            int lastIndex = platformList.Count - 1;
 
            if (furthestPositionX < platformList[lastIndex].position.X + platformList[lastIndex].platformWidth * tileWidth + distance)
            {
                if(platformList[lastIndex].moveDirection == IsMoving.horizontal)
                    furthestPositionX = (int)platformList[lastIndex].position.X + platformList[lastIndex].platformWidth * tileWidth + distance;
                else
                    furthestPositionX = (int)platformList[lastIndex].position.X + platformList[lastIndex].platformWidth * tileWidth + distance;
            }
            if (furthestPositionY < platformList[lastIndex].position.Y + platformList[lastIndex].platformHeight * tileHeight + distance)
            {
                if (platformList[lastIndex].moveDirection == IsMoving.vertical)
                    furthestPositionY = (int)platformList[lastIndex].position.Y + platformList[lastIndex].platformHeight * tileHeight + distance;
                else
                    furthestPositionY = (int)platformList[lastIndex].position.Y + platformList[lastIndex].platformHeight * tileWidth;
            }
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
