using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Godspeed.CommonProject
{
    public class Texture2DEditor
    {
        private readonly Color[] pixels;
        public readonly Texture2D texture;

        public Texture2DEditor(Texture2D texture)
        {
            this.texture = texture;
            pixels = new Color[texture.Width * texture.Height];

            texture.GetData(pixels);
        }

        public void SetColor(Point position, Color color)
        {



            //var lineIndex = ((position.X + position.Y) / texture.Height).AsInt();

            //var actualPosition = (lineIndex * texture.Width) + position.X.AsInt();
            //if (actualPosition > pixels.Length - 1
            //    || actualPosition < 0)
            //    return;
            //[width * row + col]
            //UTILIZAR ALtura do..... retangulo! não da textura~
            var actualPosition = position.Y * texture.Width + position.X;
            if (actualPosition < 0 || actualPosition > pixels.Length - 1)
                return;
            pixels.SetValue(color, position.Y * texture.Width + position.X);
        }

        public void UpdateTextureData()
        {
            texture.SetData(pixels);
        }
    }
}
