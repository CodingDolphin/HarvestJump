using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestJump
{
    public enum TileType
    {
        grassTop,
        grassMid,
        grassLeftEnd,
        grassRightEnd,
    }

    class Tile
    {
        public Sprite tileSprite { get; set; }
        public Rectangle position { get; set; }
        public TileType tileType;
         
        public Tile(TileType tileType,int x, int y, int tileWidth, int tileHeight)
        {
            this.tileType = tileType;
            this.position = new Rectangle(x, y, tileWidth, tileHeight);

            tileSprite = new Sprite(tileWidth, tileHeight, getSpriteIndex(), 1, 1, 3, 0.3d);
        }

        public int getSpriteIndex()
        {
            switch (tileType)
            {
                case TileType.grassTop: return 0;
                case TileType.grassMid: return 1;
                case TileType.grassLeftEnd:return 2;
                case TileType.grassRightEnd:return 3;
                default: return 0;
            }
        }

        public void LoadContent(ContentManager content, string assetName)
        {
            tileSprite.LoadContent(content, assetName);
        }

        public void Update(GameTime gameTime)
        {
            tileSprite.Update(gameTime, position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            tileSprite.Draw(spriteBatch);
        }
    }
}
