using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame.Common
{
    public abstract class ContentLoader
    {
        public abstract Dictionary<string, Texture2D> LoadTextures(ContentManager value);
    }
}
