using Godspeed.CommonProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Linq;
using System;

//sempre modo portrait
//obter rotacao para decidir como renderizar
//https://stackoverflow.com/questions/3674933/find-out-if-android-device-is-portrait-or-landscape-for-normal-usage
namespace Godspeed
{
    public interface Draggable : Touchable { }
    public interface Touchable
    {
        Rectangle Area { get; }
        void Update(IEnumerable<Vector2> touches);
    }

    public struct MyStruct : Draggable
    {
        public Rectangle Area { get; set; }

        public void Update(IEnumerable<Vector2> touches)
        {
            foreach (var touch in touches)
            {
                if (Area.Contains(touch))
                {

                }
            }
        }
    }

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
    //touch begin
    //touch end
    //drag start (vector2)
    //drag end (vector2)
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D skull;
        private Texture2D btn;
        private List<AnimationPart> Rectangles = new List<AnimationPart>() { new AnimationPart(0, 0, 200, 200) };
        Button btnArea = new Button(200, 0, 50, 50);
        Button trashCan = new Button(-500, 200, 150, 150);
        private List<AnimationPart> RectanglesDragged = new List<AnimationPart>();
        private readonly Camera2d Camera2d;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Camera2d = new Camera2d();
            //Camera2d.Zoom = 
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
            mousePosition = new Rectangle(Camera2d.ToWorldLocation(mouseState.Position.ToVector2()).ToPoint(), new Point(1, 1));
            isPressed = mouseState.LeftButton == ButtonState.Pressed;

            var touch = TouchPanel.GetState();
            if (touch.Any())
            {
                mousePosition = new Rectangle(Camera2d.ToWorldLocation(touch[0].Position).ToPoint(), new Point(1, 1));
                isPressed = true;
            }

            if (RectanglesDragged.Any())
                ArrastarSpriteSegurado();
            else
                ClicarEmAlgoNovo();

            Camera2d.Update();
            base.Update(gameTime);
        }

        private void ClicarEmAlgoNovo()
        {
            if (isPressed == false)
                return;

            for (int i = Rectangles.Count - 1; i >= 0; i--)
            {
                var item = Rectangles[i];
                if (item.Rectangle.Intersects(mousePosition)
                   && RectanglesDragged.Contains(item) == false)
                {
                    RectanglesDragged.Add(item);
                    break;
                }
            }

            if (RectanglesDragged.Any() == false)
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
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                 BlendState.AlphaBlend,
                 null,
                 null,
                 null,
                 null,
                 Camera2d.GetTransformation(GraphicsDevice));

            spriteBatch.Draw(
                  btn
                  , btnArea.Rectangle
                  , null
                  , Color.White
            );
            spriteBatch.Draw(
                 btn
                 ,
                trashCan.Rectangle
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
