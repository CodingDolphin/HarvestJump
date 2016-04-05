using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace HarvestJump
{
    class MenuScreen : GameScreen
    {
        private Sprite gameBanner { get; set; }
        private Sprite menuBackground { get; set; }
        private Song menuMusic { get; set; }
        private double deltaTime { get; set; }

        public MenuScreen(int screenWidth, int screenHeight) : base(screenWidth, screenHeight)  
        {
            gameBanner = new Sprite(Vector2.Zero);
            menuBackground = new Sprite(Vector2.Zero);
        }

        public override void LoadContent(ContentManager content)
        {
            gameBanner.LoadContent(content, "MenuAssets/MenuBanner");
            menuBackground.LoadContent(content, "MenuAssets/MenuBackground");
            menuMusic = content.Load<Song>("MenuAssets/MenuMusic");
            gameBanner.position = new Vector2(800 / 2 - gameBanner.texture.Width / 2, 25);
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.Play(menuMusic);
            }

            if (deltaTime >= 5f)
            {
                onIntroEnd();
            }
        }

        private void onIntroEnd()
        {
            MediaPlayer.Stop();
            NotifyScreenChange(ScreenName.PLAYSCREEN);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            menuBackground.Draw(spriteBatch);
            gameBanner.Draw(spriteBatch);
        }
    }
}
