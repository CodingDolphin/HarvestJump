using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestJump
{
    class RandomPlatformerMap
    {
        //Constants

        private const float gravity = 9.71f;

        //Random World Properties

        private int maxPlatformLenght { get; set; }
        private int maxHorizentalPlatformSpace { get; set; }
        private int minPlatformLenght { get; set; }
        private int minHorizontalPlatformSpace { get; set; }

        //Declare all Properties here.

        private List<Tile> tileList;
        private Random random;

        public RandomPlatformerMap()
        {
            //New World config

        }

        public void LoadContent(ContentManager content, string assetName)
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void CreateMap()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
