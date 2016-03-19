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

            graphicDeviceManager.PreferredBackBufferWidth = 800;
            graphicDeviceManager.PreferredBackBufferHeight = 600;

            screenWidth = graphicDeviceManager.PreferredBackBufferWidth;
            screenHeight = graphicDeviceManager.PreferredBackBufferHeight;

            graphicDeviceManager.IsFullScreen = false;
            graphicDeviceManager.ApplyChanges();

            //Initialize all important Variables

            screenList = new List<GameScreen>();

            //Define all available Screens and give each Screen a Name for ScreenChange Events

            screenList.Add(new IntroScreen(screenWidth, screenHeight));
            screenList.Add(new MenuScreen(screenWidth, screenHeight));
            screenList.Add(new PlayScreen(screenWidth, screenHeight));
            currentScreen = screenList[0];

            //Register all Events here

            screenList[0].ScreenChanged += HandleScreenChange;
        }

        public void LoadContent(ContentManager content)
        {
            //Take Reference to the Content Manager

            this.content = content;

            foreach (GameScreen screen in screenList)
            {
                screen.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        //buttonName has to Match ScreenName

        public void HandleScreenChange(ScreenName input)
        {
            switch (input)
            {
                case ScreenName.INTROSCREEN: currentScreen = screenList[0]; break;
                case ScreenName.MENUSCREEN: currentScreen = screenList[1]; break;
                case ScreenName.PLAYSCREEN: currentScreen = screenList[2]; break;
            }
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
