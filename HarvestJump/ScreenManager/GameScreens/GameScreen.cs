using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    //Declare Enums here

    public enum ScreenState
    {
        isActive,
        isInactive,
        transitionOn,
        hidden,
    }

    public enum ScreenName
    {
        PLAYSCREEN,
        MENUSCREEN,
        INTROSCREEN,
    }

    abstract class GameScreen
    {
        //Alle Klassenvariablen für jeden GameScreen

        public ScreenState screenState { get; set; }
        public string screenName { get; protected set; }
        public int screenWidth { get; private set; }
        public int screenHeight { get; private set; }

        //Alle Konstruktoren hier deklarieren

        protected GameScreen(int screenWidth, int screenHeight)
        {
            this.screenName = screenName;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        //Registrierte Methoden über ScreenChange Event informieren

        public event ScreenHandler ScreenChanged;

        protected void NotifyScreenChange(ScreenName input)
        {
            if (ScreenChanged != null)
                ScreenChanged(input); 
        }

        //Abstakte Methoden für jeden Screen

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
