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
        private Sprite gameBanner { get; set; }
        private Sprite menuBackground { get; set; }
        private Song menuMusic { get; set; }
        private double deltaTime { get; set; }
        private List<MenuEntry> menuEntryList { get; set; }
        private Vector2 startPosition { get; set; }

        public MenuScreen(int screenWidth, int screenHeight) : base(screenWidth, screenHeight)  
        {
            gameBanner = new Sprite(Vector2.Zero);
            menuBackground = new Sprite(Vector2.Zero);
            menuEntryList = new List<MenuEntry>();
        }

        public override void LoadContent(ContentManager content)
        {
            gameBanner.LoadContent(content, "MenuAssets/MenuBanner");
            menuBackground.LoadContent(content, "MenuAssets/MenuBackground");
            menuMusic = content.Load<Song>("MenuAssets/MenuMusic");

            //Game Banner Position relativ zur Screensize, Y Wert ist der obere Abstand

            gameBanner.position = new Vector2(screenWidth / 2 - gameBanner.texture.Width / 2, 50);

            //Startposition abhängig vom Game Logo X und Y haben eine kleine Abweichung bitte hier setzen

            startPosition = new Vector2(gameBanner.position.X + 10, gameBanner.position.Y + gameBanner.texture.Height + 30);

            //Hier die einzelnen Menu Button hinzufügen

            CreateButtons("Start Game", "Options", "Exit");

            foreach (MenuEntry entry in menuEntryList)
            {
                if (entry is FontButton)
                {
                    var button = (FontButton)entry;
                    button.LoadContent(content, "MenuAssets/redButton", "MenuAssets/LeagueFont");
                }
            }
        }

        public void CreateButtons(params string[] buttonNames)
        {
            for (int i = 0; i < buttonNames.Length; i++)
            {
                menuEntryList.Add(new FontButton(new Rectangle((int)startPosition.X, (int)startPosition.Y + i * 50 + i * 20, 175, 50), buttonNames[i]));
            }
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.Play(menuMusic);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            menuBackground.Draw(spriteBatch);
            gameBanner.Draw(spriteBatch);

            foreach (MenuEntry entry in menuEntryList)
            {
                if(entry is FontButton)
                {
                    var button = (FontButton)entry;
                    button.Draw(spriteBatch);
                }
            }

            //playbutton.Draw(spriteBatch);
            //optionbutton.Draw(spriteBatch);
        }
    }
}
