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
        private float _scale;
        private float _pulseValue = 0.001f;

        protected SpriteFont font { get; set; }
        protected Vector2 fontPosition { get; set; }
        protected Vector2 stringSize { get; set; }
        protected Color buttonColor { get; set; }
        protected string buttonText { get; set; }

        public FontButton(Rectangle position,bool slideAppear,ScreenName choice, string buttonText) : base(position, slideAppear, choice)
        {
            this.buttonColor = new Color(200, 56, 90);
            this.buttonText = buttonText;
            _scale = 1.25f;
        }

        public void LoadContent(ContentManager content, string textureName, string fontName, string soundName)
        {
            font = content.Load<SpriteFont>(fontName);
            stringSize = font.MeasureString(buttonText);
            AdjustFontToTexture();
            
            base.LoadContent(content, textureName, soundName);
        }

        public override void Update(GameTime gameTime)
        {
            AdjustFontToTexture();
            base.Update(gameTime);
        }
        public void AdjustFontToTexture()
        {
            fontPosition = new Vector2(position.X + position.Width / 2 - stringSize.X / 2 - 4,
                                       position.Y + position.Height / 2 - stringSize.Y / 2 - 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Pulse();
            spriteBatch.Draw(texture, position, buttonColor);
            spriteBatch.DrawString(font, buttonText, fontPosition, Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 1f);
        }

        public void Pulse()
        {
            _scale = MathHelper.Clamp(_scale += _pulseValue, 0.98f, 1.02f);

            if (isSelected)
            {
                if (_scale == 1.02f)
                    _pulseValue = _pulseValue * -1;

                if (_scale == 0.98f)
                    _pulseValue = _pulseValue * -1;
            }
            else
            {
                _scale = 1f;
            }
        }
    }
}
