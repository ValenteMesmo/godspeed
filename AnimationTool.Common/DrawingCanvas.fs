module DrawingCanvasModule

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework
open GameObjectModule
open SkiaSharp

let create(graphicsDevice: GraphicsDevice) = 
    let obj = GameObject()

    let mutable pixels = Unchecked.defaultof<Color[]>
    let mutable texture = Unchecked.defaultof<Texture2D>
    let mutable frame = Rectangle.Empty

    let setColor(position: Point, color: Color) =
        let actualPosition = position.Y * texture.Width + position.X
        
        if actualPosition < 0 || actualPosition > pixels.Length - 1 then
            ()
        else
            pixels.SetValue(color, position.Y * texture.Width + position.X)            
            ()

    let updateTexture() =
        texture.SetData(pixels)

    let save() =
        use bitmap = new SKBitmap(texture.Width, texture.Height)
    
        for i in 0..pixels.Length-1 do
            let position = TextureIO.FromArrayIndexToPoint(i, texture.Width)
            bitmap.SetPixel(position.Y, position.X, TextureIO.convertColor pixels.[i])

        use image = SkiaSharp.SKImage.FromBitmap(bitmap)
        use data = image.Encode(SkiaSharp.SKEncodedImageFormat.Png, 80)
        use stream = System.IO.File.OpenWrite(TextureIO.fileName)
        data.SaveTo(stream);

    obj.Update <- fun () ->
        PaintModule.paintOnMouseClick(texture.Bounds, setColor, updateTexture)
        if Events.Save = Events.Requested then
            Events.Save <- Events.Processing
            save()
            Events.Save <- Events.Done

    obj.Draw <- fun (world,gui) ->
        world.Draw(
            Textures.pixel
            , frame
            , Colors.paper
        )
        world.Draw(
            Textures.pixel
            , texture.Bounds
            , Color.Black
        )

        world.Draw(
            texture
            , texture.Bounds
            , Color.White
        )

    texture <- new Texture2D(graphicsDevice, 100, 100)
    frame <- Rectangle(-6,-6,112,112)
    pixels <- [| for i in 1 .. texture.Width * texture.Height -> Color.Transparent |]
    texture.SetData(pixels)

    let cloneTexure (loadedTexture: Texture2D) =
        loadedTexture.GetData(pixels)
        updateTexture()

    TextureIO.loadFile(cloneTexure, graphicsDevice)

    obj