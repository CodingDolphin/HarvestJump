using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestJump
{
    class CollisionSystem
    {
        List<Platform> randomJumpMap;

        public CollisionSystem(List<Platform>randomJumpMap)
        {
            this.randomJumpMap = randomJumpMap;
        }

        public void checkCollision(ICollide rectangleOne)
        {
            foreach (Platform platform in randomJumpMap)
            {
                if (rectangleOne.boundingBox.Intersects(platform.boundingBox))
                {
                    rectangleOne.HandleCollision(platform);
                }
            }
        }
    }
}
