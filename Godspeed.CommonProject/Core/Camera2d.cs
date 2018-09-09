﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Godspeed.CommonProject
{
    public class Camera2d : GameObject
    {
        private readonly float VIRTUAL_WIDTH = 1366;
        private readonly float VIRTUAL_HEIGHT = 768;

        private const float ZOOM_SPEED = 0.31f;
        private const float SCROLL_VELOCIY = 0.5f;
        private const float SCROLL_BREAK_VELOCITY = 0.05f;

        private float scrollSpeed = 0.0f;
        private Matrix transform;
        private Vector2 originalPosition;
        private float rotation;
        private float zoom;

        public Camera2d(bool android = false)
        {
            if (android)
            {
                var height = VIRTUAL_HEIGHT;
                VIRTUAL_HEIGHT = VIRTUAL_WIDTH;
                VIRTUAL_WIDTH = height;
            }

            zoom = 1.0f;
            rotation = 0.0f;
            position = Vector2.Zero;

            AddUpdateHandler(SetPositionBasedOnVelocity);
        }

        private void SetPositionBasedOnVelocity(GameObject obj)
        {
            SetPosition(new Vector2(position.X, position.Y + scrollSpeed));
            if (scrollSpeed > 0)
            {
                scrollSpeed -= SCROLL_BREAK_VELOCITY;
                if (scrollSpeed < 0)
                    scrollSpeed = 0;
            }
            else if (scrollSpeed < 0)
            {
                scrollSpeed += SCROLL_BREAK_VELOCITY;
                if (scrollSpeed > 0)
                    scrollSpeed = 0;
            }
        }

        public override void SetPosition(Vector2 value)
        {
            originalPosition = position = value;
        }

        public void SetPosition(Point value)
        {
            SetPosition(value.ToVector2());
        }

        public float GetZoom()
        {
            return zoom;
        }

        /// <summary>
        /// ???
        /// </summary>
        /// <param name="value">Cameras Zoom must be greater than zero! Negative zoom will flip image!</param>
        public void SetZoom(float value)
        {
            zoom = value;
            if (zoom < ZOOM_SPEED)
                zoom = ZOOM_SPEED;
        }

        public void ZoomIn()
        {
            //scrollSpeed +=
            zoom += ZOOM_SPEED;
        }

        public void ZoomOut()
        {
            zoom -= ZOOM_SPEED;
            if (zoom < ZOOM_SPEED)
                zoom = ZOOM_SPEED;
        }

        public void ScrollUp()
        {
            scrollSpeed -= SCROLL_VELOCIY;
            //SetPosition(new Vector2(position.X,position.Y - 1));
        }

        public void ScrollDown()
        {
            scrollSpeed += SCROLL_VELOCIY;
            //SetPosition(new Vector2(position.X, position.Y + 1));
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            var widthDiff = graphicsDevice.Viewport.Width / VIRTUAL_WIDTH;
            var HeightDiff = graphicsDevice.Viewport.Height / VIRTUAL_HEIGHT;

            transform =
              Matrix.CreateTranslation(
                  new Vector3(-position.X, -position.Y, 0))
                    * Matrix.CreateRotationZ(rotation)
                    * Matrix.CreateScale(new Vector3(zoom * widthDiff, zoom * HeightDiff, 1))
                    * Matrix.CreateTranslation(new Vector3(
                        graphicsDevice.Viewport.Width * 0.5f,
                        graphicsDevice.Viewport.Height * 0.5f, 0));

            return transform;
        }

        public Vector2 ToWorldLocation(Vector2 position)
        {
            return Vector2.Transform(
                position
                , Matrix.Invert(transform)
            );
        }

        public Point ToWorldLocation(Point position)
        {
            return ToWorldLocation(position.ToVector2()).ToPoint();
        }

        public Vector2 ToScreenLocation(Vector2 position)
        {
            return Vector2.Transform(position, transform);
        }
    }
}
