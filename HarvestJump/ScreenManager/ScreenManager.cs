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
        //Alle Monogame Komponenten

        private GraphicsDeviceManager graphicDeviceManager { get; set; }
        private ContentManager content { get; set; }

        //Alle Klassenvariablen

        private List<GameScreen> screenList { get; set; }
        private GameScreen currentScreen { get; set; }
        private int screenWidth { get; set; }
        private int screenHeight { get; set; }

        //Alle Konstruktoren

        public ScreenManager(GraphicsDeviceManager graphicsDeviceManager)
        {
            //Referenz zu GrafikDeviceManager holen

            this.graphicDeviceManager = graphicsDeviceManager;

            //Alle start Bildschimreinstellungen hier vornehmen

            graphicDeviceManager.PreferredBackBufferWidth = 800;
            graphicDeviceManager.PreferredBackBufferHeight = 600;

            screenWidth = graphicDeviceManager.PreferredBackBufferWidth;
            screenHeight = graphicDeviceManager.PreferredBackBufferHeight;

            graphicDeviceManager.IsFullScreen = false;
            graphicDeviceManager.ApplyChanges();

            //Alle wichtigen Variablen instanzieren

            screenList = new List<GameScreen>();

            //Weitere GameScreens hier hinzufügen

            screenList.Add(new IntroScreen(screenWidth, screenHeight));
            screenList.Add(new MenuScreen(screenWidth, screenHeight));
            screenList.Add(new PlayScreen(screenWidth, screenHeight));
            currentScreen = screenList[0];

            //Events hier registrieren

            screenList[0].ScreenChanged += HandleScreenChange;
        }

        //Allen Content hier laden

        public void LoadContent(ContentManager content)
        {
            this.content = content;

            foreach (GameScreen screen in screenList)
            {
                screen.LoadContent(content);
            }
        }

        //Spiel Updaten

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        //ScreenChange Events hier behandeln

        public void HandleScreenChange(ScreenName input)
        {
            switch (input)
            {
                case ScreenName.INTROSCREEN: currentScreen = screenList[0]; break;
                case ScreenName.MENUSCREEN: currentScreen = screenList[1]; break;
                case ScreenName.PLAYSCREEN: currentScreen = screenList[2]; break;
            }
        }

        //Aktuellen Gamescreen zeichnen

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            currentScreen.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
