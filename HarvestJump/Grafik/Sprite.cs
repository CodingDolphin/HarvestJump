using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    class Sprite
    {
        //Klassenvariablen

        public Rectangle spriteRectangle { get; set; }
        public Texture2D spriteTexture { get; set; }

        //Konstruktoren

        public Sprite()
        {
        }

        public Sprite(int x, int y, int width, int height)
        {
            spriteRectangle = new Rectangle(x, y, width, height);
        }

        //Methoden

        public virtual void LoadContent(ContentManager content, string assetName)
        {
            spriteTexture = content.Load<Texture2D>(assetName);
        }
        
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, Color.White);
        }
    }
}
