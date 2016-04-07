using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestJump
{
    class Platform : GameObject
    {
        protected List<Tile> tileList { get; set; }
        protected int platformWidth { get; set; }
        protected int platformHeight { get; set; }
        protected int tileWidth { get; set; }
        protected int tileHeight { get; set; }

        public Platform(Vector2 position,int platformWidth, int platformHeight, int tileWidth, int tileHeight) : base(position, platformWidth * tileWidth, platformHeight * tileHeight)
        {
            tileList = new List<Tile>();
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.platformWidth = platformWidth;
            this.platformHeight = platformHeight;
            this.position = position;

            CreatePlatform();
        }

        public void CreatePlatform()
        {
            if (platformHeight == 1)
                CreateSingleLayerPlatform();
            else
                CreateMultiLayerPlatform();
        }

        public void CreateSingleLayerPlatform()
        {
            for (int x = 0; x < platformWidth; x++)
            {
                if (x == 0)
                tileList.Add(new Tile(new Vector2(position.X + x * tileWidth, position.Y), tileWidth, tileHeight, TileType.grassLeftEnd));
                else if (x == platformWidth - 1)
                tileList.Add(new Tile(new Vector2(position.X + x * tileWidth, position.Y), tileWidth, tileHeight, TileType.grassRightEnd));
                else
                tileList.Add(new Tile(new Vector2(position.X + x * tileWidth, position.Y), tileWidth, tileHeight, TileType.grassTop));
            }
        }

        public void CreateMultiLayerPlatform()
        {
            for (int y = 0; y < platformHeight; y++)
            {
                for (int x = 0; x < platformWidth; x++)
                {
                    if(y == 0)
                    {
                        tileList.Add(new Tile(new Vector2(position.X + x * tileWidth, position.Y), tileWidth, tileHeight, TileType.grassTop));
                    }
                    else
                    {
                        tileList.Add(new Tile(new Vector2(position.X + x * tileWidth, position.Y + y * tileHeight), tileWidth, tileHeight, TileType.grassMid));
                    }
                }
            }
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            foreach (var tile in tileList)
            {
                tile.LoadContent(content, assetName);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in tileList)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
