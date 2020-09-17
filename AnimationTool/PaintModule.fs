module PaintModule

open Microsoft.Xna.Framework.Input
open GameCamera
open Microsoft.Xna.Framework
open DrawingCanvasModule

let paintOnMouseClick(camera:Camera, editor: DrawingCanvas) =     
    let mouse = Mouse.GetState()
    
    if mouse.LeftButton = ButtonState.Pressed then
        let mousePosition = camera.GetWorldPosition(mouse.Position)
        let drawArea = Rectangle(0, 0, editor.Texture.Width, editor.Texture.Height)
        if drawArea.Contains(mousePosition) then
            editor.SetColor(mousePosition, Color.Red)        
    ()