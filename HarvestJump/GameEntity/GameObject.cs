using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    public enum Status
    {
        activ,
        inactiv
    }

    abstract class GameObject : ICollide
    {
        protected Vector2 gravity = new Vector2(0, 9.81f);
        public Vector2 position { get; protected set; }
        public Vector2 velocity { get; protected set; }
        public double deltaTime { get; protected set; }
        public double slowMotion { get; protected set; }
        public bool isFalling { get; set; }

        public abstract void LoadContent(ContentManager content, string assetName);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
