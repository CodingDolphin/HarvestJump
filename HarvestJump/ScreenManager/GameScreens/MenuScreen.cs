using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestJump
{
    class MenuScreen : GameScreen
    {
        string screennamme;

        public MenuScreen(int screenWidth, int screenHeight) : base(screenWidth, screenHeight)
        {
            this.screennamme = screenName;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
