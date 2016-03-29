using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestJump
{
    public interface ICollide
    {
        Rectangle boundingRectangle { get; set; }
        Vector2 position { get; set; }
        Vector2 oldPosition { get; set; }
        bool noClip { get; set; }
        void HandleCollision(ICollide collisionObject);

    }
}
