using Godspeed.CommonProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

namespace Godspeed
{
    public class Cooldown
    {
        private readonly int cooldownDurationInUpdatesCount;
        private int cooldown;

        public Cooldown(int cooldownDurationInUpdatesCount)
            => this.cooldownDurationInUpdatesCount = cooldownDurationInUpdatesCount;

        public void Update() { if (cooldown > 0) cooldown--; }

        public bool IsOnCooldown() => cooldown > 0;
        public bool NotOnCooldown() => cooldown == 0;
        public void SetOnCooldown() => cooldown = cooldownDurationInUpdatesCount;
    }

    public class PencilAreaCalculator
    {
        public static List<Point> Calculate(int radiusSquared, Point point)
        {
            List<Point> indices = new List<Point>();

            for (int i = point.X - radiusSquared; i < point.X + radiusSquared; i++)
            {
                for (int j = point.Y - radiusSquared; j < point.Y + radiusSquared; j++)
                {
                    int deltaX = i - point.X;
                    int deltaY = j - point.Y;
                    double distanceSquared = Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2);
                    if (distanceSquared <= radiusSquared)
                        indices.Add(new Point(i, j));
                }
            }

            return indices;
        }
    }

    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private Camera2d camera;
        private SpriteBatch spriteBatch;
        private Texture2DEditor editor;
        private Texture2D btnTexture;
        private Rectangle btnArea = new Rectangle(100, 100, 100, 100);
        private SpriteFont font;
        private int previousScrollValue;
        private readonly bool RunningOnAndroid;
        public const int TEXTURE_SIZE = 100;
        Point? previousPoint;
        Point? previousTouchPoint;
        Cooldown toogleToolButtonCooldown = new Cooldown(60);

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
            base.Initialize();

            if (RunningOnAndroid)
                TouchPanel.EnabledGestures =
                   GestureType.Hold |
                   GestureType.Tap |
                   GestureType.DoubleTap |
                   GestureType.FreeDrag |
                   GestureType.Flick |
                   GestureType.Pinch;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            editor = new Texture2DEditor(new Texture2D(GraphicsDevice, TEXTURE_SIZE, TEXTURE_SIZE));
            font = Content.Load<SpriteFont>("font");

            editor.UpdateTextureData();
            btnTexture = Content.Load<Texture2D>("btn");
            camera = new Camera2d(RunningOnAndroid);
            camera.SetZoom(5f);
            camera.SetPosition(new Vector2(TEXTURE_SIZE / 2, TEXTURE_SIZE / 2));
            camera.LimitPositionByBounds(editor.texture.Bounds);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            toogleToolButtonCooldown.Update();

            var keyboard = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || keyboard.IsKeyDown(Keys.Escape))
                Exit();

            var mousestate = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl))
            {
                if (mousestate.ScrollWheelValue < previousScrollValue)
                {
                    camera.ZoomIn();
                    SetCameraPositionLimitedByTextureArea(mousestate.Position);
                }
                else if (mousestate.ScrollWheelValue > previousScrollValue)
                    camera.ZoomOut();
            }
            else
            {
                if (mousestate.ScrollWheelValue < previousScrollValue)
                {
                    camera.ScrollDown();
                }
                else if (mousestate.ScrollWheelValue > previousScrollValue)
                {
                    camera.ScrollUp();
                }
            }

            previousScrollValue = mousestate.ScrollWheelValue;

            var touch = TouchPanel.GetState();
            if (mousestate.LeftButton == ButtonState.Pressed)
                HandleDrawingAction(mousestate.Position);
            else if (touch.Count == 1)
                HandleDrawingAction(touch[0].Position.ToPoint());
            else
            {
                previousTouchPoint = null;
                GestureHelper.HandleTouchInput((value, point) =>
                {
                    pinch += value;
                    camera.SetZoom(pinch);
                    //verificar posicao atuao do pinch e mover de acordo com o delta (world delta)
                    camera.SetPosition(point.ToWorldPosition(camera));
                });
            }

            editor.UpdateTextureData();
            camera.Update();

            base.Update(gameTime);
        }

        private void HandleDrawingAction(Point pressedPosition)
        {
            if (btnArea.Contains(pressedPosition) && toogleToolButtonCooldown.NotOnCooldown())
            {
                editor.erasing = !editor.erasing;
                toogleToolButtonCooldown.SetOnCooldown();
            }
            else
            {
                var actualPosition = camera.ToWorldLocation(pressedPosition);
                editor.SetColor(actualPosition);

                if (previousPoint.HasValue)
                {
                    previousPoint.Value.ForEachPointUntil(actualPosition
                      , (a, b) =>
                      {
                          var points = PencilAreaCalculator.Calculate(20, new Point(a, b));
                          foreach (var point in points)
                          {
                              editor.SetColor(point);
                          }
                      });
                }

                previousPoint = actualPosition;
            }
        }

        public void line(Point a, Point b, Action<int, int> putpixel)
        {
            int w = b.X - a.X;
            int h = b.Y - a.Y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                putpixel(a.X, a.Y);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    a.X += dx1;
                    a.Y += dy1;
                }
                else
                {
                    a.X += dx2;
                    a.Y += dy2;
                }
            }
        }

        private float pinch = 0;

        private void SetCameraPositionLimitedByTextureArea(Point targetPosition)
        {
            var position = camera.GetPosition().Lerp(
                                    targetPosition.Add(TEXTURE_SIZE / 2, TEXTURE_SIZE / 2).ToWorldPosition(camera)
                                    , 0.1f);


            camera.SetPosition(position);
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
            spriteBatch.Begin();
            spriteBatch.DrawString(font, pinch.ToString(), new Vector2(100, 100), Color.Black);
            spriteBatch.Draw(btnTexture, btnArea, editor.erasing ? Color.White : Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
