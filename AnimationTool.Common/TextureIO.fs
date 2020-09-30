module TextureIO

open DrawingCanvasModule
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System
open SkiaSharp


let fileName = System.IO.Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),"savefile.png" )

let FromArrayIndexToPoint(index: int, width: int) =
    Point((index / width), index % width)

let convertColor(color:Color) =
    new SKColor(color.R, color.G, color.B, color.A)

let loadFile(editor: DrawingCanvas, device: GraphicsDevice) =
    if System.IO.File.Exists(fileName) then
        let texture = Texture2D.FromFile(device, fileName)
    
        texture.GetData(editor.Pixels)
        editor.UpdateTexture()

        texture.Dispose()
    ()

let saveFile(editor: DrawingCanvas) =
    use bitmap = new SKBitmap(editor.Texture.Width, editor.Texture.Height)
    
    for i in 0..editor.Pixels.Length-1 do
        let position = FromArrayIndexToPoint(i, editor.Texture.Width)
        bitmap.SetPixel(position.Y, position.X, convertColor editor.Pixels.[i])

    use image = SkiaSharp.SKImage.FromBitmap(bitmap)
    use data = image.Encode(SkiaSharp.SKEncodedImageFormat.Png, 80)
    use stream = System.IO.File.OpenWrite(fileName)
    data.SaveTo(stream);

    ()
