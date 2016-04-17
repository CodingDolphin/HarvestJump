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
    public enum AIState
    {
        walking,
        atacking,
        idle,
        jumping,
    }
    class Enemy : GameObject
    {
        public Vector2 speed { get; set; }
        public Vector2 jumpStrength { get; set; }

        public Enemy(Vector2 position, int width, int height) : base(position, width, height)
        {
            isJumping = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            debugRectangle.LoadContent(content, "blackPixel");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);

            spriteBatch.Draw(debugRectangle.texture, Vector2.Add(position, boxXTranslate), new Rectangle((int)position.X, (int)position.Y, (int)boundingBox.width, (int)boundingBox.height), new Color(1, 1, 1, 0.5f));
        }
    }
}
