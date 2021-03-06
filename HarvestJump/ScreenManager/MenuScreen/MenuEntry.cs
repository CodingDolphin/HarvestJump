﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    abstract class MenuEntry
    {
        protected Rectangle position { get; set; }
        protected Texture2D texture { get; set; }
        protected int endPosX { get; set; }
        protected bool slideAppear { get; set; }
        protected int appearSpeed { get; set; }
        protected bool isSelected { get; set; }
        protected ScreenManagerAction choice { get; set; }
        protected SoundEffect hoverSound { get; set; }
        public event ScreenHandler ScreenChanged;

        public MenuEntry(Rectangle position, bool slideAppear, ScreenManagerAction choice)
        {
            this.position = new Rectangle(position.X, position.Y, position.Width, position.Height);
            endPosX = position.X;

            if (slideAppear)
                this.position = new Rectangle(0 - position.Width, position.Y, position.Width, position.Height);

            this.choice = choice;
            appearSpeed = 5;
        }

        public virtual void LoadContent(ContentManager content, string assetName, string soundName)
        {
            hoverSound = content.Load<SoundEffect>(soundName);
            texture = content.Load<Texture2D>(assetName);
        }

        public virtual void Update(GameTime gameTime)
        {
            SlideAppear();

            //TODO Implement Proper Controls for Menu

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                NotifyScreenChange(ScreenManagerAction.StartGame);
        }

        public void checkSelected(Vector2 mousePosition, bool mouseClick)
        {
            Rectangle mouseRectangle = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);

            if (mouseRectangle.Intersects(position))
            {
                this.isSelected = true;
                if (mouseClick)
                {
                    hoverSound.Play();
                    NotifyScreenChange(this.choice);
                }
            }
            else
            {
                this.isSelected = false;
            }
        }

        public void NotifyScreenChange(ScreenManagerAction choice)
        {
            if (ScreenChanged != null)
                ScreenChanged(choice);
        }

        public virtual void SlideAppear()
        {
            if (position.X <= endPosX)
                position = new Rectangle(position.X + appearSpeed, position.Y, position.Width, position.Height);
        }

        public  virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
