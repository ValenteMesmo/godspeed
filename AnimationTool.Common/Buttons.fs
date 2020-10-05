module Buttons

open Microsoft.Xna.Framework
open GameObjectModule

let pencilButtonArea = Rectangle(-50, 50, 10, 10)
let eraserButtonArea = Rectangle(-50, 100, 10, 10)
let saveButtonArea = Rectangle(-50, 0, 10, 10)

let pencil = GameObject()

pencil.Update <- fun () ->
    match Input.touchPosition with
        | Some touch -> if pencilButtonArea.Contains(touch) then PaintModule.eraserMode <- false
        | None -> ()
        
pencil.Draw <- fun spriteBatch ->
    spriteBatch.Draw(
        Textures.pencil
        , pencilButtonArea
        , if not PaintModule.eraserMode then Color.Red else Colors.paper
    )




let eraser = GameObject()

eraser.Update <- fun () ->
    match Input.touchPosition with
    | Some touch -> if eraserButtonArea.Contains(touch) then PaintModule.eraserMode <- true
    | None -> ()

eraser.Draw <- fun spriteBatch ->
    spriteBatch.Draw(
        Textures.eraser
        , eraserButtonArea
        , if PaintModule.eraserMode then Color.Red else Colors.paper
    )




let save = GameObject()

save.Update <- fun () ->
    match Input.touchPosition with
    | Some touch -> if saveButtonArea.Contains(touch) then TextureIO.saveFile()
    | None -> ()

save.Draw <- fun spriteBatch ->
    spriteBatch.Draw(
        Textures.save
        , saveButtonArea
        , Colors.paper
    )
