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
        //Member Variables for all GameScreens

        public ScreenState screenState { get; set; }
        public string screenName { get; protected set; }
        public int screenWidth { get; private set; }
        public int screenHeight { get; private set; }

        //Declare all Constructor's here

        protected GameScreen(int screenWidth, int screenHeight)
        {
            this.screenName = screenName;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        //Screen Change Event

        public event ScreenHandler ScreenChanged;

        protected void NotifyScreenChange(ScreenName input)
        {
            if (ScreenChanged != null)
                ScreenChanged(input); 
        }

        //Abstract Method Declartion

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
