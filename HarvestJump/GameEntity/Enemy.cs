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
            testSprite = new Sprite(Vector2.Zero);
            currentAnimation = new Animation(new Vector2(100, 0), 5, 191, 115, 0.1f, 9);
            isJumping = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            testSprite.LoadContent(content, "blackPixel");
            currentAnimation.LoadContent(content, assetName);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);
            spriteBatch.Draw(testSprite.texture, position, new Rectangle((int)position.X, (int)position.Y, (int)boundingBox.width, (int)boundingBox.height), new Color(255,1,1,0.5f));
        }
    }
}
