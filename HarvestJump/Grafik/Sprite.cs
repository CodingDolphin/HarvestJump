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
        public Vector2? initRotationPoint { get; set; }
        public int width { get; set; }
        public int height { get; set; }

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


        public Sprite(Vector2 position, Vector2? rotationPoint = null)
        {
            this.initRotationPoint = rotationPoint;
            this.direction = Direction.right;
            this.position = position;
            this.color = Color.White;
            this.rotation = 0f;
            this.scale = 1f;
        }

        public virtual void LoadContent(ContentManager content, string assetName)
        {
            this.texture = content.Load<Texture2D>(assetName);
            this.width = texture.Width;
            this.height = texture.Height;

            SetRotationPoint();
        }

        protected void SetRotationPoint()
        {
            if(initRotationPoint == null)
            {
                rotationPoint = new Vector2(width / 2, 0);
            }
            else
            {
                rotationPoint = (Vector2)initRotationPoint;
            }
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
