using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Harvest_Jump
{
    class MenuScreen : GameScreen
    {
        string screennamme;

        public MenuScreen(string screenName, int screenWidth, int screenHeight) : base(screenName, screenWidth, screenHeight)
        {
            this.screennamme = screenName;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
