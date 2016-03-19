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
        RandomPlatformerMap platformerWorld;

        public PlayScreen(int screenWidth, int screenHeight) : base(screenWidth, screenHeight)
        {
            platformerWorld = new RandomPlatformerMap();
        }

        public override void LoadContent(ContentManager content)
        {
            platformerWorld.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            platformerWorld.Draw(spriteBatch);
        }
    }
}