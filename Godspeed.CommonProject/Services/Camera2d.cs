using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Godspeed.CommonProject
{
    public class Camera2d
    {
        private const float VIRTUAL_WIDTH = 1366;
        private const float VIRTUAL_HEIGHT = 768;

        private Matrix transform;
        private Vector2 position;
        private Vector2 originalPosition;
        private float rotation;
        private float zoom;

        public Camera2d()
        {
            zoom = 1.0f;
            rotation = 0.0f;
            position = Vector2.Zero;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector2 value)
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
        }

        public void ZoomIn() {
            zoom += 0.31f;
        }

        public void ZoomOut()
        {
            zoom -= 0.31f;
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
