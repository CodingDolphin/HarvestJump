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
        //Konstanten hier deklarieren

        private const double introDuration = 3d;

        //Klassenvariablen hier deklarieren

        private double deltaTime { get; set; }
        private Sprite menuBackground { get; set; }
        private SoundEffect introSound { get; set; }

        //Konstruktoren hier deklarieren

        public IntroScreen(int screenWidth, int screenHeight) : base(screenWidth, screenHeight)
        {
            menuBackground = new Sprite(0, 0, screenWidth, screenHeight);
        }

        //Allen Content hier laden

        public override void LoadContent(ContentManager content)
        {
            menuBackground.spriteTexture = content.Load<Texture2D>("IntroAssets/CompanyLogo");
        }

        //GameScreen hier updaten

        public override void Update(GameTime gameTime)
        {
            deltaTime += gameTime.ElapsedGameTime.TotalSeconds;

            if(deltaTime >= introDuration)
            {
                onIntroEnd();
            }
        }

        //Intro Ende hier behandeln

        private void onIntroEnd()
        {
            NotifyScreenChange(ScreenName.PLAYSCREEN);
        }

        //Aktuellen Screen zeichnen

        public override void Draw(SpriteBatch spriteBatch)
        {
            menuBackground.Draw(spriteBatch);
        }
    }
}
