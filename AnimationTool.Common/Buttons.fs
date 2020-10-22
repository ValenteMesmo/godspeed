module Buttons

open Microsoft.Xna.Framework
open GameObjectModule

let size = 100
let bot_Y = 510
let left_X = -330

let createPencilGray(mode : Config.ScreenMode) = 

    let pencilButtonArea = if mode = Config.Portrait then Rectangle(left_X + size + size/2, bot_Y - size - size/2, size, size) else Rectangle(-50, 50, size, size)
    let pencil = GameObject()
    
    pencil.Update <- fun () ->
        match Input.touchGuiPosition with
            | Some touch -> if pencilButtonArea.Contains(touch) then PaintModule.pencilColor <- Colors.gray
            | None -> ()
    ()

    pencil.Draw <- fun (world,gui) ->
        gui.Draw(
            Textures.pencil
            , pencilButtonArea
            , if PaintModule.pencilColor = Colors.gray then Color.Red else Colors.paper
        )

    pencil


let createPencilDark(mode : Config.ScreenMode) =

    let pencilButtonArea = if mode = Config.Portrait then Rectangle(left_X + size + size/2, bot_Y, size, size) else Rectangle(-50, 50, size, size)
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
            , if PaintModule.pencilColor = Colors.dark then Color.Red else Colors.paper
        )

    pencil

let createPencilLight(mode : Config.ScreenMode) =

    let pencilButtonArea = if mode = Config.Portrait then Rectangle(left_X, bot_Y - size - size/2, size, size) else Rectangle(-50, 50, size, size)
    let pencil = GameObject()
    
    pencil.Update <- fun () ->
        match Input.touchGuiPosition with
            | Some touch -> if pencilButtonArea.Contains(touch) then PaintModule.pencilColor <- Colors.light
            | None -> ()
    ()

    pencil.Draw <- fun (world,gui) ->
        gui.Draw(
            Textures.pencil
            , pencilButtonArea
            , if PaintModule.pencilColor = Colors.light then Color.Red else Colors.paper
        )

    pencil

let createEraser(mode : Config.ScreenMode) =
    let eraserButtonArea = if mode = Config.Portrait then Rectangle(left_X , bot_Y, size, size) else Rectangle(-50, 100, size, size)
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

let createSave(mode : Config.ScreenMode) =
    let saveButtonArea = if mode = Config.Portrait then Rectangle(left_X + size*4, bot_Y, size, size) else Rectangle(-50, 0, size, size)

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