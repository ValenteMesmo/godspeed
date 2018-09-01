using Godspeed.CommonProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Godspeed
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private readonly Camera2d Camera2d;
        private Texture2DEditor editor;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            Camera2d = new Camera2d();
            Camera2d.SetZoom (5f);
            Camera2d.SetPosition( new Vector2(50,50));
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            editor = new Texture2DEditor(new Texture2D(GraphicsDevice, 100, 100));

            for (int i = 0; i < editor.texture.Height; i++)
            {
                for (int j = 0; j < editor.texture.Width; j++)
                {
                    editor.SetColor(new Point(j, i), Color.Beige);
                }
            }

            editor.UpdateTextureData();

        }

        protected override void UnloadContent()
        {
        }
        private int previousScrollValue;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var mousestate = Mouse.GetState();

            if (mousestate.ScrollWheelValue < previousScrollValue)
                Camera2d.ZoomIn();
            else if (mousestate.ScrollWheelValue > previousScrollValue)
                Camera2d.ZoomOut();
            previousScrollValue= mousestate.ScrollWheelValue;

            if (mousestate.LeftButton == ButtonState.Pressed)
                {
                    var actualPosition = Camera2d.ToWorldLocation(mousestate.Position);
                    editor.SetColor(actualPosition, Color.Green);
                }

            editor.UpdateTextureData();
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
                 Camera2d.GetTransformation(GraphicsDevice));

            spriteBatch.Draw(
                editor.texture
                , new Rectangle(0, 0,100, 100)
                , Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
