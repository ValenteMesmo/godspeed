using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NSubstitute;

namespace Godspeed.CommonProject.Tests
{
    [TestClass]
    public class DrawingAndCamereMovementControllerTests
    {
        [TestMethod]
        public void ScrollDownMovesCamera()
        {
            var keyboarInput = new KeyboardInput();
            var mouseInput = new MouseInput();
            var camera = Substitute.For<Camera>();
            mouseInput.Scroll.SetValue(100);

            var sut = new DrawingAndCamereMovementController(
                camera
                , new PinchController(camera)
                , new Texture2DEditor(new Texture2D(null, 10, 10))
                , mouseInput
                , keyboarInput
            );

            sut.Update();
            camera.DidNotReceive().ScrollDown();
            camera.Received().ScrollUp();
        }

        [TestMethod]
        public void ScrollUpMovesCamera()
        {
            var keyboarInput = new KeyboardInput();
            var mouseInput = new MouseInput();
            var camera = Substitute.For<Camera>();
            mouseInput.Scroll.SetValue(-100);

            var sut = new DrawingAndCamereMovementController(
                camera
                , new PinchController(camera)
                , new Texture2DEditor(new Texture2D(null, 10, 10))
                , mouseInput
                , keyboarInput
            );

            sut.Update();
            camera.Received().ScrollDown();
            camera.DidNotReceive().ScrollUp();
        }

        [TestMethod]
        public void ScrollAndControlZoomInCamera()
        {
            var keyboarInput = new KeyboardInput();
            var mouseInput = new MouseInput();
            var camera = Substitute.For<Camera>();
            mouseInput.Scroll.SetValue(-100);
            keyboarInput.ControlPressed.SetValue(true);

            var sut = new DrawingAndCamereMovementController(
                camera
                , new PinchController(camera)
                , new Texture2DEditor(new Texture2D(null, 10, 10))
                , mouseInput
                , keyboarInput
            );

            sut.Update();
            camera.DidNotReceive().ScrollDown();
            camera.DidNotReceive().ScrollUp();
            camera.Received().ZoomIn();
            camera.DidNotReceive().ZoomOut();
        }

        [TestMethod]
        public void ScrollAndControlZoomOutCamera()
        {
            var keyboarInput = new KeyboardInput();
            var mouseInput = new MouseInput();
            var camera = Substitute.For<Camera>();
            mouseInput.Scroll.SetValue(100);
            keyboarInput.ControlPressed.SetValue(true);

            var sut = new DrawingAndCamereMovementController(
                camera
                , new PinchController(camera)
                , new Texture2DEditor(new Texture2D(null, 10, 10))
                , mouseInput
                , keyboarInput
            );

            sut.Update();
            camera.DidNotReceive().ScrollDown();
            camera.DidNotReceive().ScrollUp();
            camera.DidNotReceive().ZoomIn();
            camera.Received().ZoomOut();
        }

        [TestMethod]
        public void LeftClickDraw()
        {
            var keyboarInput = new KeyboardInput();
            var mouseInput = new MouseInput();
            var camera = Substitute.For<Camera>();
            var editor = Substitute.For<TextureEditor>();
            mouseInput.LeftButtonPressed.SetValue(true);

            var sut = new DrawingAndCamereMovementController(
                camera
                , new PinchController(camera)
                , editor
                , mouseInput
                , keyboarInput
            );

            sut.Update();
            sut.Update();

            camera.DidNotReceive().ScrollDown();
            camera.DidNotReceive().ScrollUp();
            camera.DidNotReceive().ZoomIn();
            camera.DidNotReceive().ZoomOut();
            editor.ReceivedWithAnyArgs().SetColor(Arg.Any<Point>());
        }
    }
}
