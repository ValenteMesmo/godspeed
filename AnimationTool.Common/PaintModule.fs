module PaintModule

open Microsoft.Xna.Framework

let mutable previousWorldPosition = Point.Zero 
let mutable pencilSize = 6
let mutable pencilColor = Colors.light

let paintOnMouseClick(targetArea: Rectangle, setColor, updateTexture) =
    
    if Input.mouseLeftButtonPress > 0 || Input.mouseRightButtonPress > 0 then
        if previousWorldPosition = Point.Zero then
            previousWorldPosition <- Input.mouseWorldPosition

        for point in PointUtils.pointsBetween(previousWorldPosition, Input.mouseWorldPosition) do
            for pencilPoint in PointUtils.poitsFromPencilArea(pencilSize, point) do
                if targetArea.Contains(pencilPoint) then
                    setColor(pencilPoint, pencilColor)

        updateTexture()
        previousWorldPosition <- Input.mouseWorldPosition
    else
        previousWorldPosition <- Point.Zero