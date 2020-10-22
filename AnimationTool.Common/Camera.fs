module CameraModule
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System
open Config

let DESKTOP_PORTRAIT_WIDTH = 320
let DESKTOP_PORTRAIT_HEIGHT = 480

let mutable screenWidth = DESKTOP_PORTRAIT_WIDTH
let mutable screenHeight = DESKTOP_PORTRAIT_HEIGHT

type Camera(mode : ScreenMode) =
    let VIRTUAL_WIDTH = 1280.0f
    let VIRTUAL_HEIGHT = 720.0f
    let screenWidth = if mode = Config.Portrait then VIRTUAL_HEIGHT else VIRTUAL_WIDTH
    let screenHeight = if mode = Config.Portrait then VIRTUAL_WIDTH else VIRTUAL_HEIGHT
    let mutable Transform = Unchecked.defaultof<Matrix>
    let mutable Location = Vector2(0.0f, 0.0f)
    let mutable Rotation = 0.0f
    let mutable Zoom = 1.0f

    member this.GetTransformation(graphicsDevice : GraphicsDevice) : Nullable<Matrix> =
        Transform <-
            Matrix.CreateTranslation(-Location.X, -Location.Y, 0.0f)
            * Matrix.CreateRotationZ(Rotation)
            * Matrix.CreateScale(
                Zoom * (float32 graphicsDevice.Viewport.Width / screenWidth)
                , Zoom * (float32 graphicsDevice.Viewport.Height / screenHeight)
                , 1.0f)
            * Matrix.CreateTranslation(
                (float32 graphicsDevice.Viewport.Width) * 0.5f
                , (float32 graphicsDevice.Viewport.Height) * 0.5f
                , 0.0f)
        Nullable<Matrix> Transform

    member this.GetRelativePosition (position : Point) =
        this.GetRelativePosition(position.ToVector2())

    member this.GetRelativePosition (position : Vector2) =
        Vector2.Transform(
            position
            , Matrix.Invert(Transform)
        ).ToPoint()

    member this.SetLocation location =
        Location <- location

    member this.SetZoom zoom =
        Zoom <- zoom