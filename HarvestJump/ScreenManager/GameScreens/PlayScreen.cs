﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestJump
{
    class PlayScreen : GameScreen
    {
        Sprite testSprite;

        public PlayScreen(string screenName, int screenWidth, int screenHeight) : base(screenName, screenWidth, screenHeight)
        {
            testSprite = new Sprite();
        }

        public override void LoadContent(ContentManager content)
        {
            testSprite.spriteTexture = content.Load <Texture2D>("IntroAssets/CompanyLogo");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            testSprite.Draw(spriteBatch);
        }
    }
}
