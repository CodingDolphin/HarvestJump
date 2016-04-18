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
        public Raptor enemy { get; set; }
        public Raptor enemy1 { get; set; }
        private Random random { get; set; }
        private Sprite mapBackground { get; set; }
        private CollisionSystem collisionSystem { get; set; }
        private PlatformManager platformManager { get; set; }
        private WayPointManager wayPointManager { get; set; }

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

            player = new Player(Vector2.Zero);
            mapBackground = new Sprite(Vector2.Zero);
            enemy = new Raptor(new Vector2(250, 0));
            enemy1 = new Raptor(new Vector2(0, 0));
            random = new Random();
            platformManager = new PlatformManager(tileWidth, tileHeight);

            //Map Generierung starten

            createMap(startHeight, maxPlatforms);
            wayPointManager = new WayPointManager(platformManager.platformList);

            collisionSystem = new CollisionSystem(platformManager.platformList);
        }

        public void createMap(int startposition, int maxPlatform)
        {
            platformManager.CreatePlatform(new Vector2(0, 250), 15, 300);
            platformManager.CreateMovingPlatform(new Vector2(platformManager.furthestPositionX + 4 * 32, 300), 10, 1, 1, 5 * 32, IsMoving.vertical);
            platformManager.CreateMovingPlatform(new Vector2(platformManager.furthestPositionX, 300), 10, 1, 1, 5 * 32, IsMoving.horizontal);
            platformManager.CreatePlatform(new Vector2(platformManager.furthestPositionX, 300), 25, 30);
            platformManager.CreateMovingPlatform(new Vector2(platformManager.furthestPositionX + 10 * 32, 300), 15, 1, 1, 10 * 32,IsMoving.directional);
        }

        public void LoadContent(ContentManager content)
        {
            mapBackground.LoadContent(content, "GraphicAssets/MapAssets/ScrollingBG");
            player.LoadContent(content, "GraphicAssets/PlayAssets/");
            enemy.LoadContent(content, "GraphicAssets/PlayAssets/");
            enemy1.LoadContent(content, "GraphicAssets/PlayAssets/");
            platformManager.LoadContent(content);
            wayPointManager.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            wayPointManager.Update(enemy);
            wayPointManager.Update(enemy1);
            platformManager.Update(gameTime);

            enemy.Update(gameTime);
            enemy1.Update(gameTime);
            player.Update(gameTime);

            collisionSystem.checkCollision(player);
            collisionSystem.checkCollision(enemy);
            collisionSystem.checkCollision(enemy1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mapBackground.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            enemy1.Draw(spriteBatch);
            player.Draw(spriteBatch);
            platformManager.Draw(spriteBatch);
            wayPointManager.Draw(spriteBatch);
        }
    }
}
