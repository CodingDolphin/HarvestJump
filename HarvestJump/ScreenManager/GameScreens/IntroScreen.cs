using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace HarvestJump
{
    class IntroScreen : GameScreen
    {
        //Constants configurate Intro Time here

        private const double introDuration = 3d;

        //Declare Class Propertys here

        private double deltaTime { get; set; }
        private Sprite menuBackground { get; set; }
        private SoundEffect introSound { get; set; }

        public IntroScreen(int screenWidth, int screenHeight) : base(screenWidth, screenHeight)
        {
            menuBackground = new Sprite(0, 0, screenWidth, screenHeight);
        }

        public override void LoadContent(ContentManager content)
        {
            menuBackground.spriteTexture = content.Load<Texture2D>("IntroAssets/CompanyLogo");
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime += gameTime.ElapsedGameTime.TotalSeconds;

            if(deltaTime >= introDuration)
            {
                onIntroEnd();
            }
        }

        private void onIntroEnd()
        {
            NotifyScreenChange(ScreenName.PLAYSCREEN);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            menuBackground.Draw(spriteBatch);
        }
    }
}
