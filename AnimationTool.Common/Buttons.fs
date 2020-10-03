module Buttons

open Microsoft.Xna.Framework
open System.Linq
open GameObjectModule

let pencilButtonArea = Rectangle(-50, 50, 10, 10)
let eraserButtonArea = Rectangle(-50, 100, 10, 10)
let saveButtonArea = Rectangle(-50, 0, 10, 10)

let pencil = GameObject()

pencil.Update <- fun () ->
    if Input.touches.Any(fun f -> pencilButtonArea.Contains(f)) then
        PaintModule.eraserMode <- false

pencil.Draw <- fun spriteBatch ->
    spriteBatch.Draw(
        Textures.pencil
        , pencilButtonArea
        , if not PaintModule.eraserMode then Color.Red else Colors.paperColor
    )

let eraser = GameObject()

eraser.Update <- fun () ->
    if Input.touches.Any(fun f -> eraserButtonArea.Contains(f)) then
        PaintModule.eraserMode <- true

eraser.Draw <- fun spriteBatch ->
    spriteBatch.Draw(
        Textures.eraser
        , eraserButtonArea
        , if PaintModule.eraserMode then Color.Red else Colors.paperColor
    )


let save = GameObject()

save.Update <- fun () ->
    if Input.touches.Any(fun f -> saveButtonArea.Contains(f)) then
        TextureIO.saveFile()

save.Draw <- fun spriteBatch ->
    spriteBatch.Draw(
        Textures.save
        , saveButtonArea
        , Colors.paperColor
    )
