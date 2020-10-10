module CameraModule
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System

let DESKTOP_PORTRAIT_WIDTH = 320
let DESKTOP_PORTRAIT_HEIGHT = 480

type Camera(runningOnAndroid) =    
    let VIRTUAL_WIDTH = 1280.0f
    let VIRTUAL_HEIGHT = 720.0f
    let screenWidth = if runningOnAndroid then VIRTUAL_HEIGHT else VIRTUAL_WIDTH
    let screenHeight = if runningOnAndroid then VIRTUAL_WIDTH else VIRTUAL_HEIGHT
    let mutable Transform = Unchecked.defaultof<Matrix>
    let mutable Location = Vector2(50.0f, 50.0f)
    member val Rotation = 0.0f with get, set
    member val Zoom = 5.0f with get, set

    member this.GetTransformation(graphicsDevice : GraphicsDevice) : Nullable<Matrix> =
        Transform <-
            Matrix.CreateTranslation(-Location.X, -Location.Y, 0.0f)
            * Matrix.CreateRotationZ(this.Rotation)
            * Matrix.CreateScale(
                this.Zoom * (float32 graphicsDevice.Viewport.Width / screenWidth)
                , this.Zoom * (float32 graphicsDevice.Viewport.Height / screenHeight)
                , 1.0f)
            * Matrix.CreateTranslation(
                (float32 graphicsDevice.Viewport.Width) * 0.5f
                , (float32 graphicsDevice.Viewport.Height) * 0.5f
                , 0.0f)
        Nullable<Matrix> Transform

    member this.GetWorldPosition (position : Point) =
        this.GetWorldPosition(position.ToVector2())

    member this.GetWorldPosition (position : Vector2) =
        Vector2.Transform(
            position
            , Matrix.Invert(Transform)
        ).ToPoint()

    member this.GetScreenLocation(position : Vector2) =
        Vector2.Transform(position, Transform)