module TextureIO

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System
open SkiaSharp

let fileName = System.IO.Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "savefile.png")

let FromArrayIndexToPoint(index: int, width: int) =
    Point((index / width), index % width)

let convertColor(color:Color) =
    SKColor(color.R, color.G, color.B, color.A)

let loadFile(cloneTexture, device: GraphicsDevice) =
    if System.IO.File.Exists(fileName) then
        let texture = Texture2D.FromFile(device, fileName)
    
        cloneTexture(texture)

        texture.Dispose()
    ()

let saveFile(getBitmap) =
    
    if Events.Save = Events.Done then
        Events.Save <- Events.Requested    

    ()
