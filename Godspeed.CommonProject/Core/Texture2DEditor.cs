using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Godspeed.CommonProject
{
    public interface TextureEditor
    {
        void SetColor(Point position);
        void UpdateTextureData();
        bool erasing { get; set; }
        Texture2D texture { get; }

        void Save();
    }

    public static class PointToArrayIndexExtensions
    {
        public static int ToArrayIndex(this Point point, int width)
        {
            var actualPosition = point.Y * width + point.X;
            return actualPosition;
        }

        public static Point FromArrayIndexToPoint(this int index, int width)
        {
            var result = new Point();

            if (index < width)
            {
                result.X = index;
                return result;
            }

            result.Y = (index / width);
            result.X = index % width;

            return result;
        }

    }

    public class Texture2DEditor : TextureEditor
    {
        private readonly Color[] pixels;
        private readonly StateFile StateFile;

        public Texture2D texture { get; private set; }
        private readonly Color TransparencyColor = Color.Beige;
        public bool erasing { get; set; } = false;

        public Texture2DEditor(Texture2D texture, StateFile StateFile)
        {
            this.StateFile = StateFile;
            this.texture = texture;
            var loadedPixels = StateFile.Load();
            if (loadedPixels == null || loadedPixels.Length == 0)
            {
                this.pixels = new Color[texture.Width * texture.Height];
                texture.GetData(pixels);

                erasing = true;
                for (int i = 0; i < texture.Height; i++)
                    for (int j = 0; j < texture.Width; j++)
                        SetColor(new Point(j, i));
                erasing = false;

            }
            else
            {
                this.pixels = loadedPixels;

                for (var i = 0; i < pixels.Length; i++)
                {
                    //position.Y * texture.Width + position.X
                    SetColor(i.FromArrayIndexToPoint(texture.Width), pixels[i]);
                }
            }

        }

        public void SetColor(Point position)
        {
            var color = Color.Red;
            if (erasing)
                color = TransparencyColor;

            SetColor(position, color);
        }

        private void SetColor(Point position, Color color)
        {

            var actualPosition = position.Y * texture.Width + position.X;


            if (actualPosition < 0 || actualPosition > pixels.Length - 1)
                return;
            pixels.SetValue(color, position.Y * texture.Width + position.X);
        }

        public void UpdateTextureData()
        {
            texture.SetData(pixels);
        }

        public void Save()
        {
            StateFile.Save(pixels);
        }
    }
}
