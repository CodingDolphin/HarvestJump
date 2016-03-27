using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestJump
{
    class Player : GameObject
    {
        private Animation idleAnimation { get; set; }
        private int firstPlatformHeight;

        public Player(Vector2 startPosition,int firstPlatformHeight)
        {
            this.firstPlatformHeight = firstPlatformHeight;
            this.isFalling = true;
            idleAnimation = new Animation(startPosition,0,86,75,0.1f,9);
            slowMotion = 111;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            idleAnimation.LoadContent(content, assetName);
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime += gameTime.ElapsedGameTime.TotalSeconds / slowMotion;

            if(isFalling)
            {
                position += velocity * (float)deltaTime;
                velocity += gravity * (float)deltaTime;
                if (position.Y +idleAnimation.frameHeight >= firstPlatformHeight )
                    isFalling = false;
            }

            idleAnimation.position = position;
            idleAnimation.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            idleAnimation.Draw(spriteBatch);
        }
    }
}
