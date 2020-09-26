module TextureIO

open DrawingCanvasModule
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System
type Bitmap = System.Drawing.Bitmap

let fileName = "savefile.png"

let FromArrayIndexToPoint(index: int, width: int) =
    Point((index / width), index % width)

let convertColor(color:Color) =
    System.Drawing.Color.FromArgb(
        int color.A
        , int color.R
        , int color.G
        , int color.B
    )

let loadFile(editor: DrawingCanvas, device: GraphicsDevice) =
    if System.IO.File.Exists(fileName) then
        let texture = Texture2D.FromFile(device, fileName)
    
        texture.GetData(editor.Pixels)
        editor.UpdateTexture()

        texture.Dispose()
    ()

let saveFile(editor: DrawingCanvas) =
    let pic = 
        new Bitmap(
                    editor.Texture.Width
                    , editor.Texture.Height
                    , System.Drawing.Imaging.PixelFormat.Format32bppArgb
        )    
    
    for i in 0..editor.Pixels.Length-1 do
        let position = FromArrayIndexToPoint(i, editor.Texture.Width)
        pic.SetPixel(position.Y, position.X, convertColor editor.Pixels.[i])

    pic.Save(fileName, Drawing.Imaging.ImageFormat.Png)
    pic.Dispose()
    ()
