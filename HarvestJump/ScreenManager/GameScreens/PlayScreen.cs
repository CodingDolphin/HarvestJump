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
        public RandomPlatformerMap platformerWorld;
        public Song backgroundMusicJumpMap;

        public PlayScreen(int screenWidth, int screenHeight) : base(screenWidth, screenHeight)
        {
            platformerWorld = new RandomPlatformerMap();
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
                MediaPlayer.Volume = 0.3f;
                MediaPlayer.Play(backgroundMusicJumpMap);
            }

            platformerWorld.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            platformerWorld.Draw(spriteBatch);
        }
    }
}