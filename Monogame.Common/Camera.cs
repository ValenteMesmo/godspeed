using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame.Common
{
    public class Camera
    {
        private const float defaultZoom = 0.5f;
        
        private float zoom = defaultZoom;
        private float rotation = 0.0f;

        public Vector2 position = Vector2.Zero;

        public Matrix transform { get; private set; } = new Matrix();
        
        public Vector2 GetWorldPosition(Vector2 position2)
        {
            return Vector2.Transform(position2, Matrix.Invert(transform));
        }

        public Vector2 GetScreenPosition(Vector2 position2)
        {
            return Vector2.Transform(position2, transform);
        }

        public void Update(GraphicsDevice graphicsDevice)
        {
            var widthDiff = graphicsDevice.Viewport.Width / 1176.0f;
            var HeightDiff = graphicsDevice.Viewport.Height / 664.0f;

            transform =
                Matrix.CreateTranslation(
                    new Vector3(-position.X, -position.Y, 0.0f))
                        * Matrix.CreateRotationZ(rotation)
                        * Matrix.CreateScale(
                            new Vector3(
                                zoom * widthDiff
                                , zoom * HeightDiff
                                , 1.0f))
                        * Matrix.CreateTranslation(
                            new Vector3(
                                graphicsDevice.Viewport.Width * 0.5f
                                , graphicsDevice.Viewport.Height * 0.5f
                                , 0.0f));
        }
    }
}
