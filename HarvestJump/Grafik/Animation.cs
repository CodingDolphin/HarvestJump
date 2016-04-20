using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    class Animation : SpriteSheet
    {
        public int frameCount { get; private set; }
        public double deltaTime { get; private set; }
        public double frameCycle { get; private set; }
        public bool animationIsActive { get; set; }
        public bool isLooping { get; set; }

        public Animation(Vector2 position, int index, int frameWidth, int frameHeight, double frameCycle, int frameCount, bool isLooping) : base(position, index, frameWidth, frameHeight)
        {
            this.animationIsActive = true;
            this.isLooping = isLooping;
            this.frameCycle = frameCycle;
            this.frameCount = frameCount;
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime += gameTime.ElapsedGameTime.TotalSeconds / GameObject.slowMotion;

            if (animationIsActive)
            {
                if (index == frameCount)
                {
                    if (!isLooping)
                    {
                        index = frameCount;
                        return;
                    }

                    index = 0;
                }

                if (deltaTime >= frameCycle)
                {
                    index++;
                    deltaTime = 0d;
                }

                sourceRectangle = createSourceRectangle();
            }
        }
    }
}
