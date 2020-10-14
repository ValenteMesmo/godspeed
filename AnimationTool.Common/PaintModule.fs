module PaintModule

open Microsoft.Xna.Framework

let mutable previousWorldPosition = Point.Zero 
let mutable pencilSize = 6
let mutable pencilColor = Color.White
let mutable eraserMode = false

let paintOnMouseClick(targetArea: Rectangle, setColor, updateTexture) =
    
    if Input.mouseLeftButtonPress > 0 || Input.mouseRightButtonPress > 0 then
        if previousWorldPosition = Point.Zero then
            previousWorldPosition <- Input.mousePosition

        for point in PointUtils.pointsBetween(previousWorldPosition, Input.mousePosition) do
            for pencilPoint in PointUtils.poitsFromPencilArea(pencilSize, point) do
                if targetArea.Contains(pencilPoint) then
                    if eraserMode then
                        setColor(pencilPoint, Color.Transparent)
                    else 
                        setColor(pencilPoint, pencilColor)

        updateTexture()
        previousWorldPosition <- Input.mousePosition
    else
        previousWorldPosition <- Point.Zero