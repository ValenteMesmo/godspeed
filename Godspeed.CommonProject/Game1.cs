using Godspeed.CommonProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Godspeed
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private readonly Camera2d camera;
        private SpriteBatch spriteBatch;
        private Texture2DEditor editor;
        private int previousScrollValue;

        public const int TEXTURE_SIZE = 100;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            camera = new Camera2d();
            camera.SetZoom(5f);
            camera.SetPosition(new Vector2(TEXTURE_SIZE/2, TEXTURE_SIZE/2));
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            editor = new Texture2DEditor(new Texture2D(GraphicsDevice, TEXTURE_SIZE, TEXTURE_SIZE));

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

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var mousestate = Mouse.GetState();

            if (mousestate.ScrollWheelValue != previousScrollValue)
                camera.SetPosition(
                    camera.GetPosition().Lerp(
                        mousestate.Position.Add(TEXTURE_SIZE/2, TEXTURE_SIZE/2).ToWorldPosition(camera)
                        , 0.1f))
                ;

            if (mousestate.ScrollWheelValue < previousScrollValue)
                camera.ZoomIn();
            else if (mousestate.ScrollWheelValue > previousScrollValue)
                camera.ZoomOut();
            previousScrollValue = mousestate.ScrollWheelValue;

            if (mousestate.LeftButton == ButtonState.Pressed)
            {
                var actualPosition = camera.ToWorldLocation(mousestate.Position);
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
                 camera.GetTransformation(GraphicsDevice));

            spriteBatch.Draw(
                editor.texture
                , new Rectangle(0, 0, TEXTURE_SIZE, TEXTURE_SIZE)
                , Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
