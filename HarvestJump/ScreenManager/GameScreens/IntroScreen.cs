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

        private const double introDuration = 6d;

        //Klassenvariablen hier deklarieren

        private double deltaTime { get; set; }
        private bool introStarted { get; set; }
        private Sprite menuBackground { get; set; }
        private SoundEffect introSound { get; set; }

        //Konstruktoren hier deklarieren

        public IntroScreen(int screenWidth, int screenHeight) : base(screenWidth, screenHeight)
        {
            menuBackground = new Sprite(Vector2.Zero);
            introStarted = true;
        }

        //Allen Content hier laden

        public override void LoadContent(ContentManager content)
        {
            menuBackground.LoadContent(content, "IntroAssets/CompanyLogo");
            introSound = content.Load<SoundEffect>("IntroAssets/LogoSound");
        }

        //GameScreen hier updaten

        public override void Update(GameTime gameTime)
        {
            deltaTime += gameTime.ElapsedGameTime.TotalSeconds;

            if(deltaTime >= introDuration)
            {
                onIntroEnd();
            }

            if (introStarted)
            {
                introSound.Play(1f,-0.3f,0.5f);
                introStarted = false;
            }

            menuBackground.Update(gameTime);
        }

        //Intro Ende hier behandeln und ScreenManager informieren

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
