using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

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
        private Sprite mapBackground;
        private Player player;
        private Song bgm;

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

            maxPlatformLenght = 8;
            maxHorizentalPlatformSpace = 4;
            minPlatformLenght = 4;
            minHorizontalPlatformSpace = 2;

            tileWidth = 32;
            tileHeight = 32;

            //Instance Variables

            //grassTile = new Sprite(0, 0, tileWidth, tileHeight);
            tileList = new List<Tile>();
            random = new Random();

            //Map Generierung starten

            
            mapBackground = new Sprite(Vector2.Zero);

            player = new Player(Vector2.Zero, CreateMap());
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Tile tile in tileList)
            {
                tile.LoadContent(content, "MapAssets/GrassPlatformSheet");
            }

            mapBackground.LoadContent(content, "MapAssets/Background02");
            bgm = content.Load<Song>("PlayAssets/LevelBackground");
            player.LoadContent(content, "PlayAssets/PlayerIdleAnimation");
        }

        public void Update(GameTime gameTime)
        {
            foreach (Tile tile in tileList)
            {
                tile.Update(gameTime);
            }

            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Volume = 0.60f;
                MediaPlayer.Play(bgm);
            }


            player.Update(gameTime);
        }

        public int CreateMap()
        {  
            int test = random.Next(0, 3);

            switch (test)
            {
                case 0:

                    while (maxPlatforms != 20)
                    {
                        CreateHorizontalTile(200);
                        CreateHorizontalTile(400);
                        CreateHorizontalTile(300);
                        maxPlatforms++;
                        return 200;
                    }
                    break;

                case 1:

                    while (maxPlatforms != 20)
                    {

                        CreateHorizontalTile(400);
                        CreateHorizontalTile(300);
                        CreateHorizontalTile(200);
                        maxPlatforms++;
                        return 400;
                    }
                    break;

                case 2:

                    while (maxPlatforms != 20)
                    {
                        CreateHorizontalTile(300);
                        CreateHorizontalTile(400);
                        CreateHorizontalTile(200);
                        maxPlatforms++;
                        return 300;
                    }
                    break;
            }

            return 0;
        }

        public void CreateHorizontalTile(int test)
        {
            platformLenght = random.Next(minPlatformLenght, maxPlatformLenght);
            bool firstTile;
            bool endTile;

            for (int i = 0; i < platformLenght; i++)
            {
                firstTile = false;
                endTile = false;

                if (i == 0)
                    firstTile = true;
                if (i == platformLenght -1)
                    endTile = true;
                

                if (firstTile)
                    tileList.Add(new Tile(TileType.grassLeftEnd, tileWidth * i + currentPositionX, test, tileWidth, tileHeight));
                else if (endTile)
                    tileList.Add(new Tile(TileType.grassRightEnd, tileWidth * i + currentPositionX, test, tileWidth, tileHeight));
                else
                    tileList.Add(new Tile(TileType.grassTop, tileWidth * i + currentPositionX, test, tileWidth, tileHeight));
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
            mapBackground.Draw(spriteBatch);

            foreach (Tile tile in tileList)
            {
               tile.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);
        }
    }
}
