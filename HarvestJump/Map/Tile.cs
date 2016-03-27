﻿using System;
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
         
        public Tile(TileType tileType, int x, int y, int tileWidth, int tileHeight)
        {
            this.tileType = tileType;
            this.position = new Vector2(x, y);
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
