using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    class Sprite
    {
        public Vector2 position { get; set; }
        public Texture2D texture { get; set; }
        public SpriteEffects spriteEffect { get; set; }
        public float rotation { get; set; }
        public float scale { get; set; }
        public Color color { get; set; }
        public Vector2 rotationPoint { get; set; }

        private Direction direction;
        public Direction Direction
        {
            get { return direction; }

            set {
                if (value == Direction.left)
                {
                    spriteEffect = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    spriteEffect = SpriteEffects.None;
                }
                direction = value; }
        }


        public Sprite(Vector2 position)
        {
            this.direction = Direction.right;
            this.position = position;
            this.color = Color.White;
            this.rotation = 0f;
            this.scale = 1f;
        }

        public virtual void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, Vector2.Zero, scale, spriteEffect, 1f);
        }
    }
}
