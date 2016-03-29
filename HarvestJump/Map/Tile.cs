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

    class Tile : GameObject
    {
        //Klassenvariablen

        public SpriteSheet tileSprite { get; private set; }
        public TileType tileType { get; private set; }

        //Konstruktor
         
        public Tile(Vector2 position, int tileWidth, int tileHeight, TileType tileType) : base(position, tileWidth, tileHeight)
        {
            this.tileType = tileType;
            this.position = position;
            this.tileSprite = new SpriteSheet(position, getSpriteIndex(), tileWidth, tileHeight);
        }

        //Methoden

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

        public override void LoadContent(ContentManager content, string assetName)
        {
            tileSprite.LoadContent(content, assetName);
            onContentLoad();
        }

        public void onContentLoad()
        {
            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, tileSprite.frameWidth, tileSprite.frameHeight);
        }

        public override void Update(GameTime gameTime)
        {
            tileSprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            tileSprite.Draw(spriteBatch);
        }
    }
}
