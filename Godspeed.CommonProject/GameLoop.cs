using Godspeed.CommonProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Godspeed
{
    public class GameLoop
    {
        public const int TEXTURE_SIZE = 100;
        public Texture2D texture { get => DrawingAndCamereMovementController.editor.texture; }
        public bool erasing { get => DrawingAndCamereMovementController.editor.erasing; }
        public Rectangle btnArea { get => DrawingAndCamereMovementController.btnArea; }

        private Camera2d camera;
        private readonly MouseInput MouseInput;
        private readonly KeyboardInput KeyboardInput;
        private DrawingAndCamereMovementController DrawingAndCamereMovementController;

        public GameLoop(bool RunningOnAndroid, GraphicsDevice GraphicsDevice)
        {
            MouseInput = new MouseInput();
            KeyboardInput = new KeyboardInput();

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

        public void Update()
        {
            MouseInput.Update();
            KeyboardInput.Update();
            DrawingAndCamereMovementController.Update();
            camera.Update();
        }

        public Matrix GetTransformation(GraphicsDevice GraphicsDevice)
        {
            return camera.GetTransformation(GraphicsDevice);
        }
    }
}
