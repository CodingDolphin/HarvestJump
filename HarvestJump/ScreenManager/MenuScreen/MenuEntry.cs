using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    abstract class MenuEntry
    {
        public Rectangle position;
        protected Texture2D texture;

        public MenuEntry(Rectangle position)
        {
            this.position = position;
        }

        public virtual void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void SlideAppear()
        {
        }

        public virtual void AlphaTransition()
        {
        }

        public  virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
