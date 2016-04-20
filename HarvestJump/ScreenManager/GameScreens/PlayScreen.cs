using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace HarvestJump
{
    class PlayScreen : GameScreen
    {
        public RandomPlatformerMap platformerWorld { get; set; }
        public Song backgroundMusicJumpMap { get; set; }
        private Camera camera { get; set; }

        public PlayScreen(int screenWidth, int screenHeight) : base(screenWidth, screenHeight)
        {
            platformerWorld = new RandomPlatformerMap();
            camera = new Camera(new Viewport(0, 0, screenWidth, screenHeight));
        }

        public override void LoadContent(ContentManager content)
        {
            platformerWorld.LoadContent(content);
            backgroundMusicJumpMap = content.Load<Song>("SoundAssets/PlayAssets/LevelBackground");
        }

        public override void Update(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Volume = 0.4f;
                MediaPlayer.Play(backgroundMusicJumpMap);
            }

            platformerWorld.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());
            platformerWorld.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}