module PencilOptions

open Microsoft.Xna.Framework

let updatePencilSize() =
    if Input.plusKeyPress = 1 then
        PaintModule.increasePencilSize()

    else if Input.minusKeyPress = 1 then
        PaintModule.decreasePencilSize()

let updatePencilColor() =
    if Input.num1KeyPress then
        PaintModule.pencilColor <- Colors.light

    else if Input.num2KeyPress then
        PaintModule.pencilColor <- Colors.gray

    else if Input.num3KeyPress then
        PaintModule.pencilColor <- Colors.dark

    else if Input.num4KeyPress then
        PaintModule.pencilColor <- Colors.eraser

let create() =
    let obj = GameObjectModule.GameObject()
    obj.Update <- 
        fun () ->
            updatePencilColor()
            updatePencilSize()
    obj

