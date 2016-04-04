using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HarvestJump
{
    public struct BoundingBox
    {
        public float x { get; set; }
        public float y { get; set; }
        public Vector2 position { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public float bottom { get; set; }
        public float top { get; set; }
        public float left { get; set; }
        public float right { get; set; }

        public BoundingBox(Vector2 position, float width, float height)
        {
            this.x = position.X;
            this.y = position.Y;
            this.position = position;
            this.width = width;
            this.height = height;
            this.bottom = y + height;
            this.top = y;
            this.right = x + width;
            this.left = x;
        }

        public bool Intersects(BoundingBox boundingBox)
        {
            return  (this.left < boundingBox.right && this.right > boundingBox.left &&
                       this.top < boundingBox.bottom && this.bottom > boundingBox.top) ;
        }
    }
}
