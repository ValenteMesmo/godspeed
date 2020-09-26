module PaintModule

open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework
open DrawingCanvasModule
open GameCamera

let mutable previousWorldPosition = Point.Zero 
let mutable pencilSize = 6

let paintOnMouseClick(camera:Camera, editor: DrawingCanvas) =
    let mouse = Mouse.GetState()
    
    if mouse.LeftButton = ButtonState.Pressed 
        || mouse.RightButton = ButtonState.Pressed then

        let mousePosition = camera.GetWorldPosition(mouse.Position)

        if previousWorldPosition = Point.Zero then
            previousWorldPosition <- mousePosition

        for point in PointUtils.pointsBetween(previousWorldPosition, mousePosition) do
            for pencilPoint in PointUtils.poitsFromPencilArea(pencilSize, point) do
                if editor.Texture.Bounds.Contains(pencilPoint) then
                    if mouse.RightButton = ButtonState.Pressed then
                        editor.SetColor(pencilPoint, Color.Transparent)
                    else 
                        editor.SetColor(pencilPoint, Color.Red)

        editor.UpdateTexture()
        previousWorldPosition <- mousePosition
    else
        previousWorldPosition <- Point.Zero