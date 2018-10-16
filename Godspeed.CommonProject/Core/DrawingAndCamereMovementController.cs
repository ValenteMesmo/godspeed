using Godspeed.CommonProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Godspeed
{
    public class DrawingAndCamereMovementController
    {
        public readonly TextureEditor editor;
        public Rectangle btnArea = new Rectangle(100, 100, 100, 100);
        Cooldown toogleToolButtonCooldown = new Cooldown(60);
        private Camera camera;
        PinchController PinchController;
        MouseInput MouseScroll;
        KeyboardInput KeyboardInput;
        bool wasDrawing;

        public DrawingAndCamereMovementController(
            Camera camera
            , PinchController PinchController
            , TextureEditor editor
            , MouseInput MouseInput
            , KeyboardInput KeyboardInput)
        {
            this.MouseScroll = MouseInput;
            this.KeyboardInput = KeyboardInput;
            this.camera = camera;
            this.PinchController = PinchController;
            this.editor = editor;
        }

        public void Update()
        {
            var touch = TouchPanel.GetState();

            toogleToolButtonCooldown.Update();
            HandleMouseScroll();
            HandleDrawing(touch);
        }

        private void HandleDrawing(TouchCollection touch)
        {
            if (MouseScroll.LeftButtonPressed.GetValue())
                DrawPoint(MouseScroll.Position.GetValue());
            else if (touch.Count == 1)
                DrawPoint(touch[0].Position.ToPoint());
            else
            {
                wasDrawing = false;
                PinchController.Update();
            }

            editor.UpdateTextureData();
        }

        private void HandleMouseScroll()
        {
            if (KeyboardInput.ControlPressed.GetValue())
                UseMouseScrollToChangeZoom();
            else
                UseMouseScrollToMoveCamera();
        }

        private void UseMouseScrollToMoveCamera()
        {
            if (MouseScroll.Scroll.GetValue() < MouseScroll.Scroll.GetPreviousValue())
            {
                camera.ScrollDown();
            }
            else if (MouseScroll.Scroll.GetValue() > MouseScroll.Scroll.GetPreviousValue())
            {
                camera.ScrollUp();
            }
        }

        private void UseMouseScrollToChangeZoom()
        {
            if (MouseScroll.Scroll.GetValue() < MouseScroll.Scroll.GetPreviousValue())
            {
                camera.ZoomIn();
                SetCameraPositionLimitedByTextureArea(MouseScroll.Position.GetValue());
            }
            else if (MouseScroll.Scroll.GetValue() > MouseScroll.Scroll.GetPreviousValue())
                camera.ZoomOut();
        }

        private void SetCameraPositionLimitedByTextureArea(Point targetPosition)
        {
            var position = camera.GetPosition()
                .Lerp(
                    targetPosition.Add(Game1.TEXTURE_SIZE / 2, Game1.TEXTURE_SIZE / 2)
                        .ToWorldPosition(camera)
                    , 0.1f
                );

            camera.SetPosition(position);
        }

        private void DrawPoint(Point pressedPosition)
        {
            if (btnArea.Contains(pressedPosition) && toogleToolButtonCooldown.NotOnCooldown())
            {
                editor.erasing = !editor.erasing;
                toogleToolButtonCooldown.SetOnCooldown();
            }
            else
            {
                var actualPosition = camera.ToWorldLocation(pressedPosition);
                editor.SetColor(actualPosition);

                if (wasDrawing)
                {
                    MouseScroll.Position.GetPreviousValue()
                        .ToWorldPosition(camera)
                        .ForEachPointUntil(
                            actualPosition
                            , (a, b) =>
                            {
                                var points = PencilAreaCalculator.Calculate(20, new Point(a, b));
                                foreach (var point in points)
                                {
                                    editor.SetColor(point);
                                }
                            });
                }

                wasDrawing = true;
            }
        }
    }
}
