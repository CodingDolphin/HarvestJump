﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

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
        private RenderTarget2D target { get; set; }
        private Camera playerCamera { get; set; }
        private int screenWidth { get; set; }
        private int screenHeight { get; set; }

        //Konstanten

        public const int virtualWidth = 1280;
        public const int virutalHeight = 720;

        //Temporär Testing

        private SoundEffect speedDown;
        private SoundEffect speedUp;
        private bool isPlaying;
        private double currentSoundDuration;
        private double deltaTime;

        //Alle Konstruktoren

        public ScreenManager(GraphicsDeviceManager graphicsDeviceManager)
        {
            //Referenz zu GrafikDeviceManager holen

            this.graphicDeviceManager = graphicsDeviceManager;

            //Alle start Bildschimreinstellungen hier vornehmen

            screenWidth = graphicDeviceManager.PreferredBackBufferWidth = 1280;
            screenHeight = graphicDeviceManager.PreferredBackBufferHeight = 720;

            graphicDeviceManager.IsFullScreen = false;
            graphicDeviceManager.ApplyChanges();

            //Alle wichtigen Variablen instanzieren

            screenList = new List<GameScreen>();

            //Weitere GameScreens hier hinzufügen

            screenList.Add(new IntroScreen(virtualWidth, virutalHeight));
            screenList.Add(new MenuScreen(virtualWidth, virutalHeight));
            screenList.Add(new PlayScreen(virtualWidth, virutalHeight));
            currentScreen = screenList[2];

            //Events hier registrieren

            foreach (GameScreen screen in screenList)
            {
                screen.ScreenChanged += HandleScreenChange;
            }
        }

        //Allen Content hier laden

        public void LoadContent(ContentManager content)
        {
            this.content = content;

            foreach (GameScreen screen in screenList)
            {
                screen.LoadContent(content);
            }

            playerCamera = new Camera(graphicDeviceManager.GraphicsDevice.Viewport);
            target = new RenderTarget2D(graphicDeviceManager.GraphicsDevice, virtualWidth, virutalHeight);

            //Testing

            speedUp = content.Load<SoundEffect>("SoundAssets/PlayAssets/speedUp");
            speedDown = content.Load <SoundEffect>("SoundAssets/PlayAssets/speedDown");

            var menu = (MenuScreen)screenList[1];
            foreach (MenuEntry entry in menu.menuEntryList)
            {
                entry.ScreenChanged += HandleScreenChange;
            }
        }

        //Spiel Updaten

        public void Update(GameTime gameTime)
        {
            if(isPlaying)
            deltaTime += gameTime.ElapsedGameTime.TotalSeconds;

            PlayScreen test = screenList[2] as PlayScreen;
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Q))
                playerCamera.Position = test.platformerWorld.player.position;

            if (currentSoundDuration <= deltaTime)
            {
                isPlaying = false;
                currentSoundDuration = 0;
                deltaTime = 0;
            }

            if (keyboardState.IsKeyDown(Keys.E))
            {
                currentSoundDuration = speedDown.Duration.Seconds;

                if(currentSoundDuration >= deltaTime && isPlaying == false && test.platformerWorld.player.slowMotion != 3)
                {
                    MediaPlayer.Volume = 0.1f;
                    speedDown.Play(0.5f, 0f, 0f);
                    isPlaying = true;
                    deltaTime = 0;
                    test.platformerWorld.player.slowMotion = 3;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Enter))
            {

                currentSoundDuration = speedUp.Duration.Seconds;

                if (currentSoundDuration >= deltaTime && isPlaying == false && test.platformerWorld.player.slowMotion != 1)
                {
                    MediaPlayer.Volume = 0.3f;
                    speedUp.Play(0.5f, 0f, 0f);
                    isPlaying = true;
                    deltaTime = 0;
                    test.platformerWorld.player.slowMotion = 1;
                }
            }


            if (keyboardState.IsKeyDown(Keys.W))
                playerCamera.Position -= new Vector2(0, 11);

            if (keyboardState.IsKeyDown(Keys.S))
                playerCamera.Position += new Vector2(0, 11);

            if (keyboardState.IsKeyDown(Keys.A))
                playerCamera.Position -= new Vector2(11, 0);

            if (keyboardState.IsKeyDown(Keys.D))
                playerCamera.Position += new Vector2(11, 0);

            currentScreen.Update(gameTime);
        }

        //ScreenChange Events hier behandeln

        public void HandleScreenChange(ScreenManagerAction input)
        {
            switch (input)
            {
                case ScreenManagerAction.StartIntro: currentScreen = screenList[0]; break;
                case ScreenManagerAction.SwitchToMenu: currentScreen = screenList[1]; break;
                case ScreenManagerAction.StartGame: currentScreen = screenList[2]; break;
            }

            MediaPlayer.Stop();
        }

        //Aktuellen Gamescreen zeichnen

        public void Draw(SpriteBatch spriteBatch)
        {
            graphicDeviceManager.GraphicsDevice.SetRenderTarget(target);

            spriteBatch.Begin(transformMatrix: playerCamera.GetViewMatrix());
            currentScreen.Draw(spriteBatch);
            spriteBatch.End();

            graphicDeviceManager.GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin();
            spriteBatch.Draw(target, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            spriteBatch.End();
        }
    }
}
