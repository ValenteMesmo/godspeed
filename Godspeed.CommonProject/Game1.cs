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
        Cooldown toogleToolButtonCooldown = new Cooldown(60);
        private float pinch = 0;
        Vector2 CameraOriginalPosition = new Vector2(TEXTURE_SIZE / 2, TEXTURE_SIZE / 2);
        private Vector2 pinchCenter;

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
            camera.SetPosition(CameraOriginalPosition);
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
                GestureHelper.HandleTouchInput((value, point) =>
                {
                    pinch += value;
                    camera.SetZoom(pinch);
                    pinchCenter = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2) - point.ToVector2();
                    camera.LerpPosition(
                        pinchCenter
                        , (1f - value) * 0.5f
                    );
                }
                , ()=> {

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
            spriteBatch.Draw(btnTexture, new Rectangle((int)pinchCenter.X, (int)pinchCenter.Y, 10, 10), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
