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
        public Player player { get; set; }
        private Random random { get; set; }
        private Sprite mapBackground { get; set; }

        //Manager

        private PlatformManager platformManager { get; set; }
        private GameObjectManager gameObjectManager { get; set; }

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
            maxPlatforms = 50;
            maxPlatformHeight = 10;
            minPlatformHeight = 1;

            maxPlatformLenght = 15;
            maxHorizentalPlatformSpace = 4;
            minPlatformLenght = 5;
            minHorizontalPlatformSpace = 2;

            tileWidth = 32;
            tileHeight = 32;

            //Instance Variables

            mapBackground = new Sprite(Vector2.Zero);

            //Map Variablen

            random = new Random();
            platformManager = new PlatformManager(tileWidth, tileHeight);
            createMap(startHeight, maxPlatforms);

            //Manager
            
            gameObjectManager = new GameObjectManager(platformManager.platformList);
            gameObjectManager.AddPlayer(Vector2.Zero);
            gameObjectManager.AddEnemy(EnemyType.raptor, Vector2.Zero);
            gameObjectManager.AddEnemy(EnemyType.raptor, new Vector2(700, 0));
        }

        public void createMap(int startposition, int maxPlatform)
        {
            platformManager.CreatePlatform(new Vector2(0, 250), 20, 300);
            platformManager.CreateMovingPlatform(new Vector2(platformManager.furthestPositionX + 4 * 32, 300), 10, 1, 1, 5 * 32, IsMoving.vertical);
            platformManager.CreateMovingPlatform(new Vector2(platformManager.furthestPositionX, 300), 10, 1, 1, 5 * 32, IsMoving.horizontal);
            platformManager.CreatePlatform(new Vector2(platformManager.furthestPositionX, 300), 25, 30);
            platformManager.CreateMovingPlatform(new Vector2(platformManager.furthestPositionX + 10 * 32, 300), 15, 1, 1, 10 * 32,IsMoving.directional);
        }

        public void LoadContent(ContentManager content)
        {
            platformManager.LoadContent(content);
            mapBackground.LoadContent(content, "GraphicAssets/MapAssets/ScrollingBG");
            gameObjectManager.LoadContent(content, "GraphicAssets/PlayAssets/");
        }

        public void Update(GameTime gameTime)
        {
            platformManager.Update(gameTime);
            gameObjectManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mapBackground.Draw(spriteBatch);
            platformManager.Draw(spriteBatch);
            gameObjectManager.Draw(spriteBatch);
        }
    }
}
