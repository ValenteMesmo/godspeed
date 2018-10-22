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
        MouseInput MouseInput;
        KeyboardInput KeyboardInput;
        TouchInput TouchInput;
        bool wasDrawing;

        public DrawingAndCamereMovementController(
            Camera camera
            , PinchController PinchController
            , TextureEditor editor
            , MouseInput MouseInput
            , KeyboardInput KeyboardInput
            , TouchInput TouchInput)
        {
            this.MouseInput = MouseInput;
            this.KeyboardInput = KeyboardInput;
            this.TouchInput = TouchInput;
            this.camera = camera;
            this.PinchController = PinchController;
            this.editor = editor;
        }

        public void Update()
        {
            toogleToolButtonCooldown.Update();
            HandleMouseScroll();
            HandleDrawing();
        }

        private void HandleDrawing()
        {
            if (MouseInput.LeftButtonPressed.GetValue())
                DrawPoint(MouseInput.Position);
            else if (TouchInput.touching.GetValue())
                DrawPoint(TouchInput.Position);
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
            if (MouseInput.Scroll.GetValue() < MouseInput.Scroll.GetPreviousValue())
            {
                camera.ScrollDown();
            }
            else if (MouseInput.Scroll.GetValue() > MouseInput.Scroll.GetPreviousValue())
            {
                camera.ScrollUp();
            }
        }

        private void UseMouseScrollToChangeZoom()
        {
            if (MouseInput.Scroll.GetValue() < MouseInput.Scroll.GetPreviousValue())
            {
                camera.ZoomIn();
                SetCameraPositionLimitedByTextureArea(MouseInput.Position.GetValue());
            }
            else if (MouseInput.Scroll.GetValue() > MouseInput.Scroll.GetPreviousValue())
                camera.ZoomOut();
        }

        private void SetCameraPositionLimitedByTextureArea(Point targetPosition)
        {
            var position = camera.GetPosition()
                .Lerp(
                    targetPosition.Add(GameLoop.TEXTURE_SIZE / 2, GameLoop.TEXTURE_SIZE / 2)
                        .ToWorldPosition(camera)
                    , 0.1f
                );

            camera.SetPosition(position);
        }

        private void DrawPoint(MemberValue<Point> pressedPosition)
        {
            if (btnArea.Contains(pressedPosition.GetValue()) && toogleToolButtonCooldown.NotOnCooldown())
            {
                editor.erasing = !editor.erasing;
                toogleToolButtonCooldown.SetOnCooldown();
            }
            else
            {
                var actualPosition = camera.ToWorldLocation(pressedPosition.GetValue());
                editor.SetColor(actualPosition);

                if (wasDrawing)
                {
                    pressedPosition.GetPreviousValue()
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

        public void Save()
        {
            editor.Save();
        }
    }
}
