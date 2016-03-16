using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestJump.Map
{
    class Tile
    {
        Texture2D tileTexture { get; set; }
        Rectangle collisionBox { get; set; }

        int tileWidth { get; set; }
        int tileHeight { get; set; }

        public Tile(int tileWidth, int tileHeight)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            collisionBox = new Rectangle(0, 0, tileWidth, tileHeight);
        }

        public void LoadContent(ContentManager content, string assetName)
        {
            tileTexture = content.Load<Texture2D>(assetName);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileTexture, Vector2.Zero, Color.White);
        }
    }
}
