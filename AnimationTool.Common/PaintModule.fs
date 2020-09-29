module PaintModule

open Microsoft.Xna.Framework
open DrawingCanvasModule

let mutable previousWorldPosition = Point.Zero 
let mutable pencilSize = 6

let paintOnMouseClick(editor: DrawingCanvas) =    
    
    if Input.mouseLeftButtonPress > 0 || Input.mouseRightButtonPress > 0 then
        if previousWorldPosition = Point.Zero then
            previousWorldPosition <- Input.mousePosition

        for point in PointUtils.pointsBetween(previousWorldPosition, Input.mousePosition) do
            for pencilPoint in PointUtils.poitsFromPencilArea(pencilSize, point) do
                if editor.Texture.Bounds.Contains(pencilPoint) then
                    if Input.mouseRightButtonPress > 0 then
                        editor.SetColor(pencilPoint, Color.Transparent)
                    else 
                        editor.SetColor(pencilPoint, Color.Red)

        editor.UpdateTexture()
        previousWorldPosition <- Input.mousePosition
    else
        previousWorldPosition <- Point.Zero