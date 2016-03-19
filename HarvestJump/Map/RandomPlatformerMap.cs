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
        //Konstanten hier deklarieren

        private const float gravity = 9.71f;

        //Klassenvariablen hier deklarieren

        private bool levelConstructed { get; set; }
        private int platformLenght { get; set; }
        private int maxPlatforms { get; set; }
        private int startHeight { get; set; }
        private int maxPlatformLenght { get; set; }  
        private int minPlatformLenght { get; set; }
        private int currentPositionX { get; set; }
        private int currentPositionY { get; set; }

        private List<Tile> tileList;
        private Random random;
        private Sprite grassTile;

        //Horizontale Variablen

        private int maxHorizentalPlatformSpace { get; set; }
        private int minHorizontalPlatformSpace { get; set; }

        //Vertikale Variablen

        private int maxVerticalPlatformSpace { get; set; }
        private int minVerticalPlatformSpace { get; set; }

        //Tile größe

        private int tileWidth { get; set; }
        private int tileHeight { get; set; }

        public RandomPlatformerMap()
        {
            //Map Eigenschaften hier einstellen

            startHeight = 400;
            maxPlatforms = 0;
            levelConstructed = false;

            maxPlatformLenght = 6;
            maxHorizentalPlatformSpace = 5;
            minPlatformLenght = 4;
            minHorizontalPlatformSpace = 2;

            tileWidth = 32;
            tileHeight = 32;

            //Instance Variables

            grassTile = new Sprite(0, 0, tileWidth, tileHeight);
            tileList = new List<Tile>();
            random = new Random();

            //Map Generierung starten

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
                tileList.Add(new Tile(tileWidth * i + currentPositionX, startHeight, tileWidth, tileHeight));
            }

            int space = random.Next(minHorizontalPlatformSpace, maxHorizentalPlatformSpace);

            for (int i = 0; i < space; i++)
            {
                currentPositionX = currentPositionX + tileWidth;
            }

            currentPositionX = platformLenght * tileWidth + currentPositionX;
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
