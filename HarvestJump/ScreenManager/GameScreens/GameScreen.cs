using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HarvestJump
{
    public enum ScreenState
    {
        isActive,
        transitionOn,
        hidden,
    }

    abstract class GameScreen
    {
        //Member Variables for all GameScreens

        public ScreenState screenState { get; set; }
        public string screenName { get; protected set; }
        public int screenWidth { get; private set; }
        public int screenHeight { get; private set; }

        //Declare all Constructor's here

        protected GameScreen(string screenName, int screenWidth, int screenHeight)
        {
            this.screenName = screenName;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        //Abstract Method Declartion

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
