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
        private Sprite tileSprite { get; set; }
         
        public Tile()
        {
            tileSprite = new Sprite();
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
