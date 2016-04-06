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
        protected Rectangle position { get; set; }
        protected Texture2D texture { get; set; }
        protected int endPosX { get; set; }
        protected bool slideAppear { get; set; }
        protected int appearSpeed { get; set; }

        public MenuEntry(Rectangle position, bool slideAppear)
        {
            this.position = new Rectangle(position.X, position.Y, position.Width, position.Height);
            endPosX = position.X;

            if (slideAppear)
                this.position = new Rectangle(0 - position.Width, position.Y, position.Width, position.Height);

            appearSpeed = 5;
        }

        public virtual void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        public virtual void Update(GameTime gameTime)
        {
            SlideAppear();
        }

        public virtual void SlideAppear()
        {
            if (position.X <= endPosX)
                position = new Rectangle(position.X + appearSpeed, position.Y, position.Width, position.Height);
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
