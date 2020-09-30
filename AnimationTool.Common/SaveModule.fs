module SaveModule

open Microsoft.Xna.Framework
open DrawingCanvasModule
open System.Linq

let saveButtonArea = Rectangle(-50,0,10,10)

let saveIfButtonClicked(editor: DrawingCanvas) =
    if Input.touches.Any(fun f -> saveButtonArea.Contains(f)) then
        TextureIO.saveFile(editor)
    ()