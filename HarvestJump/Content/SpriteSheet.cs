using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump.Content
{
    class SpriteSheet : Sprite
    {
        //Klassenvariablen

        private Rectangle destinationRectangle;
        private Rectangle sourceRectangle;
        public int index;

        public SpriteSheet()
        {
        }
    }
}
