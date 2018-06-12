using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Godspeed
{
    public class Button
    {
        public Button(int x, int y, int w, int h)
        {
            Rectangle = new Rectangle(x, y, w, h);
        }
        public Rectangle Rectangle;
    }

    public class AnimationPart
    {
        public AnimationPart(int x, int y, int w, int h)
        {
            Rectangle = new Rectangle(x, y, w, h);
        }
        public Rectangle Rectangle;
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D skull;
        private Texture2D btn;
        private List<AnimationPart> Rectangles = new List<AnimationPart>() { new AnimationPart(0, 0, 200, 200) };
        Button btnArea = new Button(200, 0, 50, 50);
        private List<AnimationPart> RectanglesDragged = new List<AnimationPart>();

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

        private bool isPressed = false;
        private Rectangle mousePosition;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            var mouseState = Mouse.GetState();
            mousePosition = new Rectangle(mouseState.Position, new Point(1, 1));
            isPressed = mouseState.LeftButton == ButtonState.Pressed;

            if (RectanglesDragged.Any())
                ArrastarSpriteSegurado();
            else
                ClicarEmAlgoNovo();

            base.Update(gameTime);
        }

        private void ClicarEmAlgoNovo()
        {
            if (isPressed == false)
                return;

            foreach (var item in Rectangles)
            {
                if (item.Rectangle.Intersects(mousePosition)
                    && RectanglesDragged.Contains(item) == false)
                {
                    RectanglesDragged.Add(item);
                    break;
                }
            }

            if (RectanglesDragged.Any() == false )
            {
                if (btnArea.Rectangle.Contains(mousePosition))
                {
                    var newOne = new AnimationPart(mousePosition.X, mousePosition.Y, 200, 200);
                    Rectangles.Add(newOne);
                    RectanglesDragged.Add(newOne);
                }
            }
        }

        private void ArrastarSpriteSegurado()
        {
            if (isPressed == false)
                RectanglesDragged.Clear();
            if (isPressed == true)
                foreach (var item in RectanglesDragged)
                {
                    item.Rectangle = new Rectangle(
                        mousePosition.X - item.Rectangle.Width / 2
                        , mousePosition.Y - item.Rectangle.Height / 2
                        , item.Rectangle.Width
                        , item.Rectangle.Height);
                }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(
                  btn
                  , btnArea.Rectangle
                  , null
                  , Color.White
            );

            foreach (var item in Rectangles)
            {
                spriteBatch.Draw(
                  skull
                  , item.Rectangle
                  , null
                  , Color.White
                );
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
