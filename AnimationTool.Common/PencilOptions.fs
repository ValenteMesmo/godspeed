module PencilOptions

open Microsoft.Xna.Framework

//should not be here
let updatePencilSize() =
    if Input.plusKeyPress = 1 && PaintModule.pencilSize < 30 then
        PaintModule.pencilSize <- PaintModule.pencilSize + 3

    else if Input.minusKeyPress = 1 && PaintModule.pencilSize > 0 then
        PaintModule.pencilSize <- PaintModule.pencilSize - 3

let updatePencilColor() =
    if Input.num1KeyPress then
        PaintModule.pencilColor <- Color.White
    else if Input.num2KeyPress then
        PaintModule.pencilColor <- Color.Black

let create() =
    let obj = GameObjectModule.GameObject()
    obj.Update <- 
        fun () ->
            updatePencilColor()
            updatePencilSize()
    obj

//eu ia fazer um update aqui, apetando os numeros ia mudar as cores

