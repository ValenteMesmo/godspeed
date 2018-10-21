using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Godspeed
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D btnTexture;
        private SpriteFont font;
        private GameLoop GameLoop;
        private readonly bool RunningOnAndroid;

        public Game1(bool android = false)
        {
            RunningOnAndroid = android;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            if (RunningOnAndroid)
                TouchPanel.EnabledGestures =
                   GestureType.Hold |
                   GestureType.Tap |
                   GestureType.DoubleTap |
                   GestureType.FreeDrag |
                   GestureType.Flick |
                   GestureType.Pinch;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");
            btnTexture = Content.Load<Texture2D>("btn");

            GameLoop = new GameLoop(RunningOnAndroid, GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            GameLoop.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                 BlendState.AlphaBlend,
                 SamplerState.PointClamp,
                 null,
                 null,
                 null,
                 GameLoop.GetTransformation(GraphicsDevice));

            spriteBatch.Draw(
                GameLoop.texture
                , new Rectangle(0, 0, GameLoop.TEXTURE_SIZE, GameLoop.TEXTURE_SIZE)
                , Color.White);

            spriteBatch.End();
            spriteBatch.Begin();
            //spriteBatch.DrawString(font, pinch.ToString(), new Vector2(100, 100), Color.Black);
            spriteBatch.Draw(btnTexture, GameLoop.btnArea, GameLoop.erasing ? Color.White : Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
