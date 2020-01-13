using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame.Common
{
    public abstract class BaseGame : Game
    {
        private readonly List<GameObject> objectList = new List<GameObject>();
        private GameObject[] objectArray = new GameObject[0];

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private readonly ContentLoader contentLoader;
        private readonly TouchController TouchInputs;
        private readonly Camera Camera;
        public BaseGame(
            ContentLoader contentLoader
            , TouchController TouchInputs
            , Camera Camera
            , bool runningOnAndroid = false)
        {
            this.contentLoader = contentLoader;
            this.TouchInputs = TouchInputs;
            this.Camera = Camera;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //World = new World(
            //    new Camera(GraphicsDevice)
            //    {
            //        position = new Vector2(1179.0f, 0.0f)
            //    }
            //    , contentLoader.LoadTextures(Content));

            //font = Content.Load<SpriteFont>("Font");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //World.AddObject(new BalloonFactory(World));
            //World.AddObject(new FrameCounter(new TextSprite
            //{
            //    Font = font,
            //    Position = new Vector2(100, -500)
            //}));
        }


        protected override void Update(GameTime gameTime)
        {
            //TODO:parei aqui... deixando de usar a class world para botar tudo nessa class
            TouchInputs.Update(Camera);

            objectArray = objectList.ToArray();
            for (var i = 0; i < objectArray.Length; i++)
            {
                var obj = objectArray[i];

                //TODO: remove this iff? create real remove method instead of flag
                if (obj.Destroyed)
                    objectList.Remove(obj);
                else
                    obj.Update();
            }

            Camera.Update(GraphicsDevice);



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                Camera.transform);

            for (var i = 0; i < objectArray.Length; i++)
                foreach (var spriteData in objectArray[i].Sprites)
                    spriteData.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
