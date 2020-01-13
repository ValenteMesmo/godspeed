using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Common;

namespace Godspeed.Desktop
{
    public class Game1 : BaseGame
    {
        public Game1() 
            : base(
                new DesktopContetLoader()
                , new TouchController()
                , new Camera()
                , runningOnAndroid : false)
        {
        }
    }
}
