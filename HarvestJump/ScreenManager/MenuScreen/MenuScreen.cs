using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace HarvestJump
{
    class MenuScreen : GameScreen
    {
        //Properties

        private Sprite menuBanner { get; set; }
        private Sprite menuBackground { get; set; }
        private Song menuMusic { get; set; }
        public List<MenuEntry> menuEntryList { get; set; }
        private Vector2 startPosition { get; set; }
        private InputManager input { get; set; }

        //Fields

        private int bannerSpaceTop { get; set; }
        private int buttonSpaceLeft { get; set; }
        private int buttonSpaceFromBannerTop { get; set; }
        private int buttonSpacing { get; set; }
        private int buttonWidth { get; set; }
        private int buttonHeight { get; set; }
        private bool slideAppear { get; set; }

        public MenuScreen(int screenWidth, int screenHeight) : base(screenWidth, screenHeight)  
        {
            menuBackground = new Sprite(Vector2.Zero);
            menuBanner = new Sprite(Vector2.Zero);
            menuEntryList = new List<MenuEntry>();
            input = new InputManager();

            //Spacing und Padding in Pixeln

            bannerSpaceTop = 65;
            buttonSpaceLeft = 10;
            buttonSpaceFromBannerTop = 65;
            buttonWidth = 214;
            buttonHeight = 60;
            buttonSpacing = buttonHeight + 20;
            slideAppear = true;
        }

        public override void LoadContent(ContentManager content)
        {
            menuBanner.LoadContent(content, "GraphicAssets/MenuAssets/MenuBanner");
            menuBackground.LoadContent(content, "GraphicAssets/MenuAssets/MenuBackground");
            menuMusic = content.Load<Song>("SoundAssets/MenuAssets/MenuMusic");
            menuBanner.position = (new Vector2(screenWidth / 2 - menuBanner.texture.Width / 2, bannerSpaceTop));

            //Buttons erstellen und Content laden

            CreateButtons(new ScreenName[] { ScreenName.PLAYSCREEN, ScreenName.OPTIONSSCREEN, ScreenName.EXITSCREEN }, "Start Game", "Options", "Exit");

            foreach (MenuEntry entry in menuEntryList)
            {
                if (entry is FontButton)
                {
                    var button = (FontButton)entry;
                    button.LoadContent(content, "GraphicAssets/MenuAssets/redButton", "Fonts/LeagueFont","SoundAssets/MenuAssets/rollover1");
                }
            }
        }

        public void CreateButtons(ScreenName[] screenNames, params string[] buttonNames)
        {
            startPosition = new Vector2(menuBanner.position.X + buttonSpaceLeft, menuBanner.position.Y + menuBanner.texture.Height + buttonSpaceFromBannerTop);

            for (int i = 0; i < buttonNames.Length; i++)
            {
                menuEntryList.Add(new FontButton(new Rectangle((int)startPosition.X, (int)startPosition.Y + i * buttonSpacing, buttonWidth, buttonHeight),slideAppear,screenNames[i], buttonNames[i]));
            }
        }

        public override void Update(GameTime gameTime)
        {
            input.Update(gameTime);

            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.Play(menuMusic);
            }

            foreach (MenuEntry entry in menuEntryList)
            {
                entry.Update(gameTime);
                entry.checkSelected(input.getMousePosition(), input.GetLeftClick());
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            menuBackground.Draw(spriteBatch);
            menuBanner.Draw(spriteBatch);

            foreach (MenuEntry entry in menuEntryList)
            {
                if(entry is FontButton)
                {
                    var button = (FontButton)entry;
                    button.Draw(spriteBatch);
                }
            }
        }
    }
}
