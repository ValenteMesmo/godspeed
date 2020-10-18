module Buttons

open Microsoft.Xna.Framework
open GameObjectModule

let size = 100
let bot_Y = size*5
let left_X = -250

let createPencil runningOnAndroid = 
    //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
    //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width

    let pencilButtonArea = if runningOnAndroid then Rectangle(-250, bot_Y, size, size) else Rectangle(-50, 50, size, size)
    let pencil = GameObject()
    
    pencil.Update <- fun () ->
        match Input.touchGuiPosition with
            | Some touch -> if pencilButtonArea.Contains(touch) then PaintModule.pencilColor <- Colors.dark
            | None -> ()
    ()

    pencil.Draw <- fun (world,gui) ->
        gui.Draw(
            Textures.pencil
            , pencilButtonArea
            , if PaintModule.pencilColor = Colors.eraser then Colors.paper else Color.Red
        )

    pencil

let createEraser runningOnAndroid =
    let eraserButtonArea = if runningOnAndroid then Rectangle(left_X + size*2, bot_Y, size, size) else Rectangle(-50, 100, size, size)
    let eraser = GameObject()
    
    eraser.Update <- fun () ->
        match Input.touchGuiPosition with
        | Some touch -> if eraserButtonArea.Contains(touch) then PaintModule.pencilColor <- Colors.eraser
        | None -> ()
    
    eraser.Draw <- fun (world,gui)->
        gui.Draw(
            Textures.eraser
            , eraserButtonArea
            , if PaintModule.pencilColor = Colors.eraser then Color.Red else Colors.paper
        )

    eraser

let createSave runningOnAndroid =
    let saveButtonArea = if runningOnAndroid then Rectangle(left_X + size*4, bot_Y, size, size) else Rectangle(-50, 0, size, size)

    let save = GameObject()

    save.Update <- fun () ->
        match Input.touchGuiPosition with
        | Some touch -> if saveButtonArea.Contains(touch) then TextureIO.saveFile()
        | None -> ()

    save.Draw <- fun (world,gui) ->
        gui.Draw(
            Textures.save
            , saveButtonArea
            , Colors.paper
        )

    save