using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Godspeed
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D skull;
        private Texture2D btn;
        private List<Rectangle> Rectangles = new List<Rectangle>();
        Rectangle btnArea = new Rectangle(100, 100, 200, 200);
        private List<Rectangle> RectanglesDragged = new List<Rectangle>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            skull = Content.Load<Texture2D>("skull");
            btn = Content.Load<Texture2D>("btn");
        }

        protected override void UnloadContent()
        {
        }

        bool wasPressed = false;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var mouseState = Mouse.GetState();
            var isPressed = mouseState.LeftButton == ButtonState.Pressed;

            if (RectanglesDragged.Any())
            {
                //....
            }

            wasPressed = isPressed;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(
                  btn
                  , btnArea
                  , null
                  , Color.White
            );

            foreach (var item in Rectangles)
            {
                spriteBatch.Draw(
                  skull
                  , item
                  , null
                  , Color.White
                );
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
