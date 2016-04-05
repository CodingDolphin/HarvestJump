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
    class FontButton : MenuEntry
    {
        protected SpriteFont font { get; set; }
        protected Vector2 stringPosition { get; set; }
        protected Vector2 stringSize { get; set; }
        protected Color buttonColor { get; set; }
        protected string buttonText { get; set; }

        public FontButton(Rectangle position, string buttonText) : base(position)
        {
            this.buttonColor = new Color(200, 56, 90);
            this.buttonText = buttonText;
        }

        public void LoadContent(ContentManager content, string textureName, string fontName)
        {
            font = content.Load<SpriteFont>(fontName);
            stringSize = font.MeasureString(buttonText);
            base.LoadContent(content, textureName);
        }

        public void AdjustFontToTexture()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, buttonColor);
            spriteBatch.DrawString(font, buttonText, new Vector2(stringSize.X + stringSize.Width / 2 - stringSize.X / 2 , stringSize.Y + stringSize.Height / 2 - stringSize.Y / 2), Color.White); 
        }
    }
}
