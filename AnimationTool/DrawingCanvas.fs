module DrawingCanvasModule

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework

type DrawingCanvas(GraphicsDevice: GraphicsDevice) =
    let mutable pixels = Unchecked.defaultof<Color[]>
    let mutable texture = Unchecked.defaultof<Texture2D>

    let setColor(position: Point, color: Color) =
        let actualPosition = position.Y * texture.Width + position.X
        
        if actualPosition < 0 || actualPosition > pixels.Length - 1 then
            ()
        else
            pixels.SetValue(color, position.Y * texture.Width + position.X)            
            ()

    do
        texture <- new Texture2D(GraphicsDevice, 100, 100)
        pixels <- [| for i in 1 .. texture.Width * texture.Height -> Color.Transparent |]
        texture.SetData(pixels)
        ()

    member this.SetColor(position: Point, color: Color) =
        setColor(position, color)
        ()

    member this.UpdateTexture() =
        texture.SetData(pixels)
        
    member this.Texture = texture
    member this.Pixels = pixels