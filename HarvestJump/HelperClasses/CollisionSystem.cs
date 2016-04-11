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

        public void checkCollision(ICollide entity)
        {
            foreach (Platform platform in randomJumpMap)
            {
                if (entity.boundingBox.Intersects(platform.boundingBox))
                {
                        entity.HandleCollision(platform);
                }
            }
        }
    }
}
