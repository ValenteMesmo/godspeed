module GameCamera
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System

type Camera() = 
    let VIRTUAL_WIDTH = 1280.0f
    let VIRTUAL_HEIGHT = 720.0f
    let mutable Transform = Unchecked.defaultof<Matrix>
    let mutable Location = Vector2(50.0f,50.0f)
    member val Rotation = 0.0f with get, set
    member val Zoom = 5.0f with get, set

    member this.GetTransformation(graphicsDevice : GraphicsDevice) : Nullable<Matrix> =
        Transform <-
            Matrix.CreateTranslation(-Location.X, -Location.Y, 0.0f)
            * Matrix.CreateRotationZ(this.Rotation)
            * Matrix.CreateScale(
                this.Zoom * (float32 graphicsDevice.Viewport.Width / VIRTUAL_WIDTH)
                , this.Zoom * (float32 graphicsDevice.Viewport.Height / VIRTUAL_HEIGHT)
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