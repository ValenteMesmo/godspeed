using Godspeed.CommonProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Godspeed
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private Camera2d camera;
        private SpriteBatch spriteBatch;
        private readonly MouseInput MouseInput;
        private readonly KeyboardInput KeyboardInput;
        private DrawingAndCamereMovementController DrawingAndCamereMovementController;
        private Texture2D btnTexture;
        private SpriteFont font;
        private readonly bool RunningOnAndroid;
        public const int TEXTURE_SIZE = 100;

        public Game1(bool android = false)
        {
            RunningOnAndroid = android;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";

            MouseInput = new MouseInput();
            KeyboardInput = new KeyboardInput();
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();

            if (RunningOnAndroid)
                TouchPanel.EnabledGestures =
                   GestureType.Hold |
                   GestureType.Tap |
                   GestureType.DoubleTap |
                   GestureType.FreeDrag |
                   GestureType.Flick |
                   GestureType.Pinch;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


            font = Content.Load<SpriteFont>("font");


            btnTexture = Content.Load<Texture2D>("btn");
            camera = new Camera2d(RunningOnAndroid);
            camera.SetZoom(5f);
            camera.SetPosition(new Vector2(TEXTURE_SIZE / 2, TEXTURE_SIZE / 2));

            var editor = new Texture2DEditor(
                   new Texture2D(
                       GraphicsDevice
                       , TEXTURE_SIZE
                       , TEXTURE_SIZE
                   )
               );
            camera.LimitPositionByBounds(editor.texture.Bounds);
    
            DrawingAndCamereMovementController = new DrawingAndCamereMovementController(
               camera
               , new PinchController(camera)
               , editor
               , MouseInput
               , KeyboardInput
           );
            DrawingAndCamereMovementController.editor.UpdateTextureData();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            MouseInput.Update();
            KeyboardInput.Update();
            DrawingAndCamereMovementController.Update();

            camera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                 BlendState.AlphaBlend,
                 SamplerState.PointClamp,
                 null,
                 null,
                 null,
                 camera.GetTransformation(GraphicsDevice));

            spriteBatch.Draw(
                DrawingAndCamereMovementController.editor.texture
                , new Rectangle(0, 0, TEXTURE_SIZE, TEXTURE_SIZE)
                , Color.White);

            spriteBatch.End();
            spriteBatch.Begin();
            //spriteBatch.DrawString(font, pinch.ToString(), new Vector2(100, 100), Color.Black);
            spriteBatch.Draw(btnTexture, DrawingAndCamereMovementController.btnArea, DrawingAndCamereMovementController.editor.erasing ? Color.White : Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
