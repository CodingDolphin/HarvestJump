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

        public Sprite()
        {
        }

        public void LoadContent(ContentManager content, string assetName)
        {
            spriteTexture = content.Load<Texture2D>(assetName);
            onContentLoaded();
        }

        private void onContentLoaded()
        {
            this.spriteRectangle = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
        }
        
        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, Vector2.Zero, Color.White);
        }
    }
}
