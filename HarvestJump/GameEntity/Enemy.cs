using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestJump
{
    class Enemy : GameObject
    {
        public Enemy(Vector2 position, int width, int height) : base(position, width, height)
        {
            currentSprite = new Animation(new Vector2(100, 0), 5, 142, 105, 0.1f, 9);
            isJumping = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            currentSprite.LoadContent(content, assetName);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentSprite.Draw(spriteBatch);
        }
    }
}
