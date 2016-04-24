using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    class SpriteSheet : Sprite
    {
        public Rectangle sourceRectangle { get; set; }
        public int index { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }

        public SpriteSheet(Vector2 position, int index, int frameWidth, int frameHeight, Vector2? rotationPoint = null) : base(position, rotationPoint)
        {
            this.index = index;
            this.width = frameWidth;
            this.height = frameHeight;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
            onContentLoad();
        }

        public void onContentLoad()
       {
            columns = texture.Width / width;
            rows = texture.Height / height;
            sourceRectangle = createSourceRectangle();
            SetRotationPoint();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public Rectangle createSourceRectangle()
        {
            int row = index / columns;
            int column = index % columns;
            return new Rectangle(width * column, height * row, width, height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, Vector2.Zero, scale, spriteEffect, 1f);
        }
    }
}