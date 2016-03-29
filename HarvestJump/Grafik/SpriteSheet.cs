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
        public int frameWidth { get; set; }
        public int frameHeight { get; set; }

        public SpriteSheet(Vector2 position, int index, int frameWidth, int frameHeight) : base(position)
        {
            this.index = index;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
            onContentLoad();
        }

        public void onContentLoad()
       {
            rows = texture.Width / frameWidth;
            columns = texture.Height / frameHeight;
            sourceRectangle = createSourceRectangle();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public Rectangle createSourceRectangle()
        {
            int row = index / columns;
            int column = index % columns;
            return new Rectangle(frameWidth * row, frameHeight * column, frameWidth, frameHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White);
        }
    }
}
