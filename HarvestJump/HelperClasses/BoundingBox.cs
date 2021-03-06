﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HarvestJump
{
    public class BoundingBox
    {
        public Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float width { get; set; }
        public float height { get; set; }
        public float bottom { get; set; }
        public float top { get; set; }
        public float left { get; set; }
        public float right { get; set; }

        public BoundingBox(Vector2 position, float width, float height)
        {
            this.position = position;
            this.width = width;
            this.height = height;
            this.bottom = position.Y + height;
            this.top = position.Y;
            this.right = position.X + width;
            this.left = position.X;
        }

        public void ChangePosition(Vector2 position)
        {
            this.position = position;
        }

        public bool Intersects(BoundingBox boundingBox)
        {
            return  (this.left < boundingBox.right && this.right > boundingBox.left &&
                       this.top < boundingBox.bottom && this.bottom > boundingBox.top) ;
        }
    }
}
