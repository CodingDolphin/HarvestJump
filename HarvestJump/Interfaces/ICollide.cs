using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestJump
{
    public interface ICollide
    {
        BoundingBox boundingBox { get; set; }
        Vector2 position { get; set; }
        Vector2 velocity { get; set; }
        bool noClip { get; set; }
        void HandleCollision(ICollide collisionObject);
    }
}
