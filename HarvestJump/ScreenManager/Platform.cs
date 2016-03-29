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
        public List<Tile> tileList { get; set; }
        public int tileWidth { get; set; }
        public int tileHeight { get; set; }

        public Platform(Vector2 position,int width, int height, int tileWidth, int tileHeight)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.position = position;
            tileList = new List<Tile>();
            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, width * tileWidth, height * tileHeight);
            CreatePlatform(width, height);
        }

        public void CreatePlatform(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                if(i == 0)
                {
                    tileList.Add(new Tile(new Vector2(position.X + i * tileWidth, position.Y), tileWidth, tileHeight, TileType.grassLeftEnd));
                }
                else if(i == width -1)
                {
                    tileList.Add(new Tile(new Vector2(position.X + i * tileWidth, position.Y), tileWidth, tileHeight, TileType.grassRightEnd));
                }
                else
                {
                    tileList.Add(new Tile(new Vector2(position.X + i * tileWidth, position.Y), tileWidth, tileHeight, TileType.grassTop));
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
