﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HarvestJump
{
    class Raptor : Enemy
    {
        private string variant { get; set; }

        public Raptor(Vector2 position, int width = 151, int height = 115) : base(position, width, height)
        {
            this.speed = new Vector2(0, 0);
            this.jumpStrength = new Vector2(0f, -250f);
            this.random = new Random(System.DateTime.Now.Millisecond);

            initializeRaptorAnimation();
            decideVariant();
        }

        public void decideVariant()
        {
            if (random.Next(0,2) == 0)
            {
                variant = "Green";
            }
            else
            {
                variant = "Blue";
            }
        }

        public void initializeRaptorAnimation()
        {
            this.AddState(AnimationStatus.atacking, Position, 53, 0, 191, 115, 0.07f, 9, false, 140, 111);
            this.AddState(AnimationStatus.idle, Position, 53, 0, 191, 115, 0.3f, 7, true, 140, 111);
            this.AddState(AnimationStatus.run, Position, 53, 0, 191, 115, 0.2f, 7, true, 140, 111);
            this.AddState(AnimationStatus.walking, Position, 53, 0, 191, 115, 0.3f, 9, true, 140, 111);
            this.AddState(AnimationStatus.dead, Position, 104.5f, 0, 209, 115, 0.3f, 8, false, 191, 85);
            this.CurrentAnimation = StateData[AnimationStatus.walking].Item1;
            this.BoundingBox = StateData[AnimationStatus.walking].Item2;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            string contentPath = assetName;

            foreach (var item in StateData)
            {
                switch (item.Key)
                {
                    case AnimationStatus.run:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorRunAnimation" + variant);
                        break;
                    case AnimationStatus.walking:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorWalkAnimation" + variant);
                        break;
                    case AnimationStatus.dead:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorDeadAnimation" + variant);
                        break;
                    case AnimationStatus.jumping:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorJumpAnimation" + variant);
                        break;
                    case AnimationStatus.idle:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorIdleAnimation" + variant);
                        break;
                    case AnimationStatus.atacking:
                        item.Value.Item1.LoadContent(content, contentPath + "RaptorAtackAnimation" + variant);
                        break;
                }
            }

            base.LoadContent(content, assetName);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
