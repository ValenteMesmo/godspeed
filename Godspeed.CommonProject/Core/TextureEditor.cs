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
}
