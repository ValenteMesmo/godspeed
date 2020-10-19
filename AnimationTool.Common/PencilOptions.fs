module PencilOptions

open Microsoft.Xna.Framework
let updatePencilSize() =
    if Input.plusKeyPress = 1 && PaintModule.pencilSize < 30 then
        PaintModule.pencilSize <- PaintModule.pencilSize + 3

    else if Input.minusKeyPress = 1 && PaintModule.pencilSize > 0 then
        PaintModule.pencilSize <- PaintModule.pencilSize - 3

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

