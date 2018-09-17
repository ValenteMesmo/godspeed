using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Godspeed.CommonProject
{
    public class Texture2DEditor
    {
        private readonly Color[] pixels;
        public readonly Texture2D texture;
        private readonly Color TransparencyColor = Color.Beige;
        public bool erasing = false;

        public Texture2DEditor(Texture2D texture)
        {
            this.texture = texture;
            pixels = new Color[texture.Width * texture.Height];

            texture.GetData(pixels);

            erasing = true;
            for (int i = 0; i < texture.Height; i++)
                for (int j = 0; j < texture.Width; j++)
                    SetColor(new Point(j, i));
            erasing = false;
        }

        public void SetColor(Point position)
        {
            var color = Color.Red;
            if (erasing)
                color = TransparencyColor;
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
