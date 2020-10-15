module Buttons

open Microsoft.Xna.Framework
open GameObjectModule

let size = 20

let createPencil runningOnAndroid = 
    let pencilButtonArea = if runningOnAndroid then Rectangle(0-size/2, 150, size, size) else Rectangle(-50, 50, size, size)
    let pencil = GameObject()
    
    pencil.Update <- fun () ->
        match Input.touchPosition with
            | Some touch -> if pencilButtonArea.Contains(touch) then PaintModule.pencilColor <- Colors.dark
            | None -> ()
    ()

    pencil.Draw <- fun spriteBatch ->
        spriteBatch.Draw(
            Textures.pencil
            , pencilButtonArea
            , if PaintModule.pencilColor = Colors.eraser then Colors.paper else Color.Red
        )

    pencil

let createEraser runningOnAndroid =
    let eraserButtonArea = if runningOnAndroid then Rectangle(50-size/2, 150, size, size) else Rectangle(-50, 100, size, size)
    let eraser = GameObject()
    
    eraser.Update <- fun () ->
        match Input.touchPosition with
        | Some touch -> if eraserButtonArea.Contains(touch) then PaintModule.pencilColor <- Colors.eraser
        | None -> ()
    
    eraser.Draw <- fun spriteBatch ->
        spriteBatch.Draw(
            Textures.eraser
            , eraserButtonArea
            , if PaintModule.pencilColor = Colors.eraser then Color.Red else Colors.paper
        )

    eraser

let createSave runningOnAndroid =
    let saveButtonArea = if runningOnAndroid then Rectangle(100-size/2, 150, size, size) else Rectangle(-50, 0, size, size)

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

    save