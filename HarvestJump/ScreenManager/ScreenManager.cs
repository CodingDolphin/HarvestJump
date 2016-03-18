using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    class ScreenManager
    {
        //Declare all Events here.

        public event ScreenHandler MenuButtonPressed;

        //Mono Components

        private GraphicsDeviceManager graphicDeviceManager { get; set; }
        private ContentManager content { get; set; }

        //Declare all Properties here.

        private List<GameScreen> screenList { get; set; }
        private GameScreen currentScreen { get; set; }
        private int screenWidth { get; set; }
        private int screenHeight { get; set; }

        //Declare all Constructor's here.

        public ScreenManager(GraphicsDeviceManager graphicsDeviceManager)
        {
            //Take Reference to all Important Graphic Objects

            this.graphicDeviceManager = graphicsDeviceManager;

            //Configurate Screen

            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            graphicDeviceManager.PreferredBackBufferWidth = 1280;
            graphicDeviceManager.PreferredBackBufferHeight = 720;

            graphicDeviceManager.IsFullScreen = false;
            graphicDeviceManager.ApplyChanges();

            //Initialize all important Variables

            screenList = new List<GameScreen>();

            //Define all available Screens and give each Screen a Name for ScreenChange Events

            screenList.Add(new MenuScreen("MENU", screenWidth, screenHeight));
            screenList.Add(new IntroScreen("INTRO", screenWidth, screenHeight));
            screenList.Add(new PlayScreen("PLAYSCREEN", screenWidth, screenHeight));
            currentScreen = screenList[2];
        }
        
        public void LoadContent(ContentManager content)
        {
            //Take Reference to the Content Manager

            this.content = content;

            currentScreen.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        //buttonName has to Match ScreenName

        public void HandleScreenChange(GameScreen source, string buttonName)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            graphicDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            currentScreen.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
