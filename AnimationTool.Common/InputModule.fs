module Input

open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework
open CameraModule
open Microsoft.Xna.Framework.Input.Touch

let mutable plusKeyPress = 0
let mutable minusKeyPress = 0
let mutable mousePosition = Point.Zero
let mutable mouseLeftButtonPress = 0
let mutable mouseRightButtonPress = 0
let touches = System.Collections.Generic.List<Point>()

let update(camera: Camera) =
    let keyboardState = Keyboard.GetState()
    let mouse = Mouse.GetState()
    let _touches = TouchPanel.GetState()
  
    touches.Clear()    
    if _touches.Count > 0 then
        for touch in _touches do
            mousePosition <- camera.GetWorldPosition(touch.Position)
            touches.Add(mousePosition)
        mouseLeftButtonPress <- mouseLeftButtonPress + 1
    else
        mousePosition <- camera.GetWorldPosition(mouse.Position)

        if mouse.LeftButton = ButtonState.Pressed then
            touches.Add(mousePosition)
            mouseLeftButtonPress <- mouseLeftButtonPress + 1
        else
            mouseLeftButtonPress <- 0

        if mouse.RightButton = ButtonState.Pressed then
            mouseRightButtonPress <- mouseRightButtonPress + 1
        else
            mouseRightButtonPress <- 0
        
    if keyboardState.IsKeyDown(Keys.Add) || keyboardState.IsKeyDown(Keys.OemPlus) then
        plusKeyPress <- plusKeyPress + 1
    else
        plusKeyPress <- 0

    if keyboardState.IsKeyDown(Keys.Subtract) || keyboardState.IsKeyDown(Keys.OemMinus) then
        minusKeyPress <- minusKeyPress + 1
    else
        minusKeyPress <- 0
        
    ()

