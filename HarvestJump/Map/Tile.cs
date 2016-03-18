using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestJump
{
    class Tile
    {
        public Sprite tileSprite { get; set; }
         
        public Tile(int x, int y, int tileWidth, int tileHeight)
        {
            tileSprite = new Sprite(x, y, tileWidth, tileHeight);
        }

        public void LoadContent(ContentManager content, string assetName)
        {
            tileSprite.LoadContent(content, assetName);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileSprite.spriteTexture, Vector2.Zero, Color.White);
        }
    }
}
