using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestJump
{
    public interface ICollide
    {
        BoundingBox BoundingBox { get; set; }
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        bool NoClip { get; set; }
        void HandleCollision(ICollide collisionObject);
    }
}
