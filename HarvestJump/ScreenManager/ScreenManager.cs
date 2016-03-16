using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Harvest_Jump
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

        //Testing

        Texture2D testSprite;

        //Declare all Constructor's here.

        public ScreenManager(GraphicsDeviceManager graphicsDeviceManager)
        {
            //Take Reference to all Important Graphic Objects

            this.graphicDeviceManager = graphicsDeviceManager;

            //Configurate Screen

            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            graphicDeviceManager.PreferredBackBufferWidth = screenWidth;
            graphicDeviceManager.PreferredBackBufferHeight = screenHeight;

            graphicDeviceManager.IsFullScreen = false;
            graphicDeviceManager.ApplyChanges();

            //Initialize all important Variables

            screenList = new List<GameScreen>();

            //Define all available Screens and give each Screen a Name for ScreenChange Events

            screenList.Add(new MenuScreen("MENU", screenWidth, screenHeight));
            screenList.Add(new IntroScreen("INTRO", screenWidth, screenHeight));
        }
        
        public void LoadContent(ContentManager content)
        {
            //Take Reference to the Content Manager

            this.content = content;

            //Load Content here

            testSprite = content.Load <Texture2D>("IntroAssets/CompanyLogo");
        }

        public void Update(GameTime gameTime)
        {
        }

        //buttonName has to Match ScreenName

        public void HandleScreenChange(GameScreen source, string buttonName)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            graphicDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(testSprite, new Rectangle(0, 0, 100, 100), Color.White);

            spriteBatch.End();


        }
    }
}
