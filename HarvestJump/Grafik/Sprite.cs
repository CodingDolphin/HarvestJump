using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    class Sprite
    {
        public Rectangle spriteRectangle { get; set; }
        public Texture2D spriteTexture { get; set; }

        public Sprite(int x, int y, int width, int height)
        {
            spriteRectangle = new Rectangle(x, y, width, height);
        }

        public void LoadContent(ContentManager content, string assetName)
        {
            spriteTexture = content.Load<Texture2D>(assetName);
        }
        
        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, Color.White);
        }
    }
}
