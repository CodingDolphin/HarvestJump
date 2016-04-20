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

    public enum ScreenManagerAction
    {
        StartGame,
        SwitchToMenu,
        StartIntro,
        SwitchToOptions,
        Exit,
    }

    abstract class GameScreen
    {
        //Alle Klassenvariablen für jeden GameScreen

        public ScreenState screenState { get; set; }
        public string screenName { get; protected set; }
        public int screenWidth { get; private set; }
        public int screenHeight { get; private set; }
        protected Viewport viewport { get; set; }

        //Alle Konstruktoren hier deklarieren

        protected GameScreen(int screenWidth, int screenHeight, Viewport viewport)
        {
            this.screenName = screenName;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.viewport = viewport;
        }

        public GameScreen(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        //Registrierte Methoden über ScreenChange Event informieren

        public event ScreenHandler ScreenChanged;

        protected void NotifyScreenChange(ScreenManagerAction input)
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
