using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Monogame.Common
{
    public abstract class Drawable
    {
        public abstract void Draw(SpriteBatch batch);
    }
    public abstract class Behavior
    {
        public abstract void Update();
    }
    public abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Destroyed { get; private set; }

        public readonly List<Drawable> Sprites = new List<Drawable>();

        protected Behavior Behavior { get; set; }

        public void Update()
        {
            if (Behavior != null)
                Behavior.Update();
        }

        public void Destroy()
        {
            Destroyed = true;
        }

#if DEBUG
        public override string ToString()
        {
            return GetType().Name;
        }
#endif
    }


    public abstract class ContentLoader
    {
        public abstract Dictionary<string, Texture2D> LoadTextures(ContentManager value);
    }
    public abstract class BaseGame : Game
    {
        private readonly List<GameObject> objectList = new List<GameObject>();
        private GameObject[] objectArray = new GameObject[0];

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private readonly ContentLoader contentLoader;
        private readonly TouchController TouchInputs ;
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

            font = Content.Load<SpriteFont>("Font");
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
            TouchInputs.Update();

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

            Camera.Update();



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
    public abstract class TouchInputs
    {
        public abstract IEnumerable<Vector2> GetTouches();
    }
    public class TouchController : TouchInputs
    {
        private readonly Camera camera;
        private List<Vector2> touches = new List<Vector2>();

        public TouchController(Camera camera)
        {
            this.camera = camera;
        }

        public override IEnumerable<Vector2> GetTouches() => touches;

        public void Update()
        {
            touches.Clear();

            var touchCollection = TouchPanel.GetState();
            foreach (var touch in touchCollection)
            {
                if (touch.State == TouchLocationState.Pressed)
                    touches.Add(camera.GetWorldPosition(touch.Position));
            }

            var mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
                touches.Add(camera.GetWorldPosition(mouse.Position.ToVector2()));
        }
    }

    public class Camera
    {
        private const float defaultZoom = 0.5f;
        private readonly GraphicsDevice graphicsDevice;
        private float zoom = defaultZoom;
        private float rotation = 0.0f;

        public Vector2 position = Vector2.Zero;

        public Matrix transform { get; private set; } = new Matrix();
        public Camera(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public Vector2 GetWorldPosition(Vector2 position2)
        {
            return Vector2.Transform(position2, Matrix.Invert(transform));
        }

        public Vector2 GetScreenPosition(Vector2 position2)
        {
            return Vector2.Transform(position2, transform);
        }

        public void Update()
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
