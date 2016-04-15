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
        public SpriteEffects direction { get; set; }
        public float rotation { get; set; }
        public float scale { get; set; }
        public Color color { get; set; }


        public Sprite(Vector2 position)
        {
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
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
