using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    class Sprite
    {
        //Klassenvariablen

        private Rectangle position { get; set; }
        private Rectangle sourceRectangle { get; set; }
        private Texture2D spriteTexture { get; set; }
        private double deltaTime { get; set; }
        private int index { get; set; }
        private int rows { get; set; }
        private int columns { get; set; }
        private int totalFrames { get; set; }
        private int frameWidth { get; set; }
        private int frameHeight { get; set; } 
        private int maxFrame { get; set; }
        private double frameCycle { get; set; }
        private bool isAnimated { get; set; }

        //Sprite Konstruktor

        public Sprite(Rectangle position)
        {
            this.position = position;
            this.sourceRectangle = new Rectangle(0, 0, position.Width, position.Height);
        }

        //SpriteSheet Konstruktor

        public Sprite(int frameWidth, int frameHeight, int index, int rows, int columns)
        {
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.rows = rows;
            this.columns = columns;
            this.index = index;

            sourceRectangle = createSourceRectangle();
        }

        //SpriteSheet animated

        public Sprite(int frameWidth, int frameHeight, int index, int rows, int columns,int maxFrame, double frameCycle)
        {
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.rows = rows;
            this.columns = columns;
            this.index = index;
            this.maxFrame = maxFrame;
            this.frameCycle = frameCycle;
            this.isAnimated = true;

            sourceRectangle = createSourceRectangle();
        }

        //Methoden

        public void LoadContent(ContentManager content, string assetName)
        {
            spriteTexture = content.Load<Texture2D>(assetName);
        }
        
        public virtual void Update(GameTime gameTime, Rectangle position)
        {
            this.position = position;

            if(isAnimated)
            {
                deltaTime += gameTime.ElapsedGameTime.TotalSeconds;

                if (index == maxFrame)
                    index = 0;

                if (deltaTime >= frameCycle)
                {
                    index++;
                    deltaTime = 0d;
                }

                sourceRectangle = createSourceRectangle();
            }
        }

        public Rectangle createSourceRectangle()
        {
            int row = index / columns;
            int column = index % columns;
            return new Rectangle(frameWidth * row, frameHeight * column, frameWidth, frameHeight);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, position, sourceRectangle, Color.White);
        }
    }
}
