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
    class Enemy : GameObject, ISmart
    {
        public Vector2 speed { get; set; }
        public Vector2 jumpStrength { get; set; }

        public Enemy(Vector2 position, int width, int height) : base(position, width, height)
        {
            this.isJumping = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
        }

        public void HandleWaypoint(Direction direction)
        {
            SetDirection(direction);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
