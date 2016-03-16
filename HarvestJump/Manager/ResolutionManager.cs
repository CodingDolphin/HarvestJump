using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace HarvestJump
{
    class ResolutionManager
    {
        const float virtualWidth = 1280;
        const float virtualHeight = 720;

        private Matrix scaleMatrix;
        private Viewport viewPort;

        public ResolutionManager(Viewport viewPort)
        {
            this.viewPort = viewPort;
        }

        public Matrix getScaleMatrix()
        {
            return scaleMatrix = Matrix.CreateScale(viewPort.Width / virtualWidth, viewPort.Height / virtualHeight, 1f);
        }
    }
}
