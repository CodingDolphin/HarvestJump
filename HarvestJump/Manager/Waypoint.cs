using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HarvestJump
{
    struct Waypoint
    {
        public Direction direction { get; set; }
        public BoundingBox wayPointCollider { get; set; }

        public Waypoint(Direction direction, BoundingBox wayPointCollider)
        {
            this.direction = direction;
            this.wayPointCollider = wayPointCollider;
        }
    }
}
