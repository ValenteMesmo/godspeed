module PencilPreviewModule

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input
open GameCamera

type PencilPreview(GraphicsDevice: GraphicsDevice, camera:Camera) =
    let mutable pixels = Unchecked.defaultof<Color[]>
    let mutable texture = Unchecked.defaultof<Texture2D>
    let mutable area = Rectangle.Empty
    let mutable previousPencilSize = 0
    let updatePreview() =
        if previousPencilSize <> PaintModule.pencilSize then
            pixels <- [| for i in 1 .. texture.Width * texture.Height -> Color.Transparent |]
           
            for pencilPoint in PointUtils.poitsFromPencilArea(PaintModule.pencilSize, texture.Bounds.Center) do
                //this line is duplicated elsewhere
                let actualPosition = pencilPoint.Y * texture.Width + pencilPoint.X
                pixels.[actualPosition] <- Color.Red

            texture.SetData(pixels)
        previousPencilSize <- PaintModule.pencilSize

    do
        texture <- new Texture2D(GraphicsDevice, 100, 100)
        updatePreview()
        area.Width <- texture.Width
        area.Height <- texture.Height
        ()

    member this.update() =
        updatePreview()
        area.Location <- camera.GetWorldPosition(Mouse.GetState().Position) - texture.Bounds.Center
        ()

    member this.Texture = texture
    member this.Area = area

//todo: move to other file
let updatePencilSize() =
    if Input.plusKeyPress = 1 && PaintModule.pencilSize < 30 then
        PaintModule.pencilSize <- PaintModule.pencilSize + 3

    else if Input.minusKeyPress = 1 && PaintModule.pencilSize > 0 then
        PaintModule.pencilSize <- PaintModule.pencilSize - 3