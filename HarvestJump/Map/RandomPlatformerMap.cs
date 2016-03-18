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
        private int startHeight { get; set; }
        private int maxPlatforms { get; set; }
        private bool levelConstructed { get; set; }
        private int currentPosition { get; set; }
        private int platformLenght { get; set; }

        //Config Tile size here

        private int tileWidth { get; set; }
        private int tileHeight { get; set; }

        //Declare all Properties here.

        private List<Tile> tileList;
        private Random random;
        private Sprite grassTile;

        public RandomPlatformerMap()
        {
            //New World config

            startHeight = 400;
            maxPlatforms = 0;
            levelConstructed = false;

            maxPlatformLenght = 6;
            maxHorizentalPlatformSpace = 4;
            minPlatformLenght = 3;
            minHorizontalPlatformSpace = 1;

            tileWidth = 32;
            tileHeight = 32;

            //Instance Variables

            grassTile = new Sprite(0, 0, tileWidth, tileHeight);
            tileList = new List<Tile>();
            random = new Random();

            //Start Map Generation

            CreateMap();         
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Tile tile in tileList)
            {
                tile.LoadContent(content, "MapAssets/grassBlock");
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public void CreateMap()
        {
            while (maxPlatforms != 10)
            {
                CreateHorizontalTile();
                maxPlatforms++;
            }
        }

        public void CreateHorizontalTile()
        {
            platformLenght = random.Next(minPlatformLenght, maxPlatformLenght);

            for (int i = 0; i < platformLenght; i++)
            {
                tileList.Add(new Tile(tileWidth * i + currentPosition, startHeight, tileWidth, tileHeight));
            }

            int space = random.Next(minHorizontalPlatformSpace, maxHorizentalPlatformSpace);

            for (int i = 0; i < space; i++)
            {
                currentPosition = currentPosition + tileWidth;
            }

            currentPosition = platformLenght * tileWidth + currentPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in tileList)
            {
                spriteBatch.Draw(tile.tileSprite.spriteTexture, tile.tileSprite.spriteRectangle, Color.White);
            }
        }
    }
}
