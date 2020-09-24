module PencilPreviewModule

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input
open GameCamera

type PencilPreview(GraphicsDevice: GraphicsDevice, camera:Camera) =
    let mutable pixels = Unchecked.defaultof<Color[]>
    let mutable texture = Unchecked.defaultof<Texture2D>
    let mutable area = Rectangle.Empty

    do
        texture <- new Texture2D(GraphicsDevice, 100, 100)
        pixels <- [| for i in 1 .. texture.Width * texture.Height -> Color.Transparent |]
        
        for pencilPoint in PaintModule.poitsFromPencilArea(6, texture.Bounds.Center) do
            //this line is duplicated elsewhere
            let actualPosition = pencilPoint.Y * texture.Width + pencilPoint.X
            pixels.[actualPosition] <- Color.Red

        texture.SetData(pixels)
        area.Width <- texture.Width
        area.Height <- texture.Height
        ()

    member this.update() =
        area.Location <- camera.GetWorldPosition(Mouse.GetState().Position) - texture.Bounds.Center
        ()

    member this.Texture = texture
    member this.Area = area