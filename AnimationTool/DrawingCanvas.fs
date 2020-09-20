module DrawingCanvasModule

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type DrawingCanvas(GraphicsDevice: GraphicsDevice) =
    let mutable pixels = Unchecked.defaultof<Color[]>
    //let mutable StateFile = Unchecked.defaultof<StateFile>

    let mutable texture = Unchecked.defaultof<Texture2D>
    let mutable TransparencyColor = Color.Beige
    let mutable erasing = false

    //let UpdateTextureData =
    //    texture.SetData(pixels);

    let setColor(position: Point, color: Color) =
        let actualPosition = position.Y * texture.Width + position.X
        
        if actualPosition < 0 || actualPosition > pixels.Length - 1 then
            ()
        else
            pixels.SetValue(color, position.Y * texture.Width + position.X)            
            ()

    do
        texture <- new Texture2D(GraphicsDevice, 100, 100)
        pixels <- [| for i in 1 .. texture.Width * texture.Height -> Color.White |]
        //texture.GetData(pixels)
        erasing <- true
        for i in 0 .. texture.Height do
            for j in 0 .. texture.Width do
                setColor(
                    new Point(j, i)
                    , if erasing then TransparencyColor else Color.Red
                )
        erasing <- false
        texture.SetData(pixels)
        ()

    member this.SetColor(position: Point, color: Color) =
        setColor(position, color)
        ()

    member this.Erase(position: Point) =
        setColor(position, TransparencyColor)
        ()

    member this.UpdateTexture() =
        texture.SetData(pixels)

    member this.Texture = texture
    member this.Pixels = pixels