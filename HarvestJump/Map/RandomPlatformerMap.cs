using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace HarvestJump
{
    class RandomPlatformerMap
    {
        //Klassenvariablen hier deklarieren

        private int platformLenght { get; set; }
        private int maxPlatforms { get; set; }
        private int startHeight { get; set; }
        private int maxPlatformHeight { get; set; }
        private int maxPlatformLenght { get; set; }  
        private int minPlatformLenght { get; set; }
        private int minPlatformHeight { get; set; }
        private int currentPositionX { get; set; }
        private int currentPositionY { get; set; }

        private List<Platform> platformList { get; set; }
        private Random random { get; set; }
        private Sprite mapBackground { get; set; }
        public Player player { get; set; }
        private CollisionSystem collisionSystem { get; set; }

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

            startHeight = 300;
            maxPlatforms = 0;
            maxPlatformHeight = 10;
            minPlatformHeight = 1;

            maxPlatformLenght = 8;
            maxHorizentalPlatformSpace = 4;
            minPlatformLenght = 5;
            minHorizontalPlatformSpace = 2;

            tileWidth = 32;
            tileHeight = 32;

            //Instance Variables

            platformList = new List<Platform>();
            random = new Random();
            collisionSystem = new CollisionSystem(platformList);

            //Map Generierung starten

            mapBackground = new Sprite(Vector2.Zero);
            player = new Player(Vector2.Zero,75,75);
            createMap(300, 10);
        }

        public void createMap(int startposition, int maxPlatform)
        {
            int spaceX = 0;
            int spaceY = startposition;
            int height;

            for (int i = 0; i < maxPlatform; i++)
            {
                platformLenght = random.Next(minPlatformLenght, maxPlatformLenght);
                height = random.Next(minPlatformHeight, maxPlatformHeight);
                platformList.Add(new Platform(new Vector2(currentPositionX + spaceX, spaceY), platformLenght, height, 32, 32));
                spaceX = random.Next(100, 200);
                spaceY = random.Next(100, 400);
                currentPositionX = (int)platformList[i].boundingBox.x + (int)platformList[i].boundingBox.width;
            }
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Platform platform in platformList)
            {
                platform.LoadContent(content, "MapAssets/GrassPlatformSheet");
            }

            mapBackground.LoadContent(content, "MapAssets/Background02");
            player.LoadContent(content, "PlayAssets/PlayerIdleAnimationRight");
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            collisionSystem.checkCollision(player);
            player.UpdateAnimation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mapBackground.Draw(spriteBatch);

            foreach (Platform platform in platformList)
            {
                platform.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);
        }
    }
}
