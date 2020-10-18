module PencilPreviewModule

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework

let create(graphicsDevice: GraphicsDevice) = 
    let mutable pixels = Unchecked.defaultof<Color[]>
    let mutable texture = Unchecked.defaultof<Texture2D>
    let mutable area = Rectangle.Empty
    let mutable previousPencilSize = 0

    let obj = GameObjectModule.GameObject()

    let updatePreview() =
        if previousPencilSize <> PaintModule.pencilSize then
            pixels <- [| for i in 1 .. texture.Width * texture.Height -> Color.Transparent |]
           
            for pencilPoint in PointUtils.poitsFromPencilArea(PaintModule.pencilSize, texture.Bounds.Center) do
                //this line is duplicated elsewhere
                let actualPosition = pencilPoint.Y * texture.Width + pencilPoint.X
                pixels.[actualPosition] <- Color.White

            texture.SetData(pixels)
        previousPencilSize <- PaintModule.pencilSize
        area.Location <- Input.mouseWorldPosition - texture.Bounds.Center    

    texture <- new Texture2D(graphicsDevice, 100, 100)
    updatePreview()
    area.Width <- texture.Width
    area.Height <- texture.Height

    let update() =
        updatePreview()
        ()

    let draw(world :SpriteBatch,gui :SpriteBatch) = 
        world.Draw(
            texture
            , area
            , PaintModule.pencilColor
        )
    obj.Update <- update
    obj.Draw <- draw
            
    obj