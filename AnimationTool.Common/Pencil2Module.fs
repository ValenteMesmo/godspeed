module Pencil2Module

open Microsoft.Xna.Framework
open System.Linq

let pencilButtonArea = Rectangle(-50,50,10,10)
let eraserButtonArea = Rectangle(-50,100,10,10)

let togglePencilEraser() =
    if Input.touches.Any(fun f -> pencilButtonArea.Contains(f)) then
        PaintModule.eraserMode <- false
    else if Input.touches.Any(fun f -> eraserButtonArea.Contains(f)) then
        PaintModule.eraserMode <- true
    ()