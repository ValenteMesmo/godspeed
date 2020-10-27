module Input

open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework
open CameraModule
open Microsoft.Xna.Framework.Input.Touch

let mutable num1KeyPress = false
let mutable num2KeyPress = false
let mutable num3KeyPress = false
let mutable num4KeyPress = false

let mutable plusKeyPress = 0
let mutable minusKeyPress = 0

let mutable mouseLeftButtonPress = 0
let mutable mouseRightButtonPress = 0
let mutable mouseWorldPosition = Point.Zero
let mutable mouseGuiPosition = Point.Zero
let mutable touchWorldPosition : Option<Point> = None
let mutable touchGuiPosition : Option<Point> = None

let update(worldCamera: Camera, guiCamera: Camera) =
    let keyboardState = Keyboard.GetState()
    let mouse = Mouse.GetState()
    let _touches = TouchPanel.GetState()
  
    if _touches.Count > 0 then        
        mouseWorldPosition <- worldCamera.GetRelativePosition(_touches.[0].Position)
        touchWorldPosition <- Some mouseWorldPosition
        touchGuiPosition <- Some mouseGuiPosition
        mouseGuiPosition <- guiCamera.GetRelativePosition(_touches.[0].Position)
            
        mouseLeftButtonPress <- mouseLeftButtonPress + 1
    else
        mouseWorldPosition <- worldCamera.GetRelativePosition(mouse.Position)
        touchWorldPosition <- None 
        mouseGuiPosition <- guiCamera.GetRelativePosition(mouse.Position)
        touchGuiPosition <- None 
        if mouse.LeftButton = ButtonState.Pressed then
            touchWorldPosition <- Some mouseWorldPosition
            touchGuiPosition <- Some mouseGuiPosition
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

    num1KeyPress <- keyboardState.IsKeyDown(Keys.D1)
    num2KeyPress <- keyboardState.IsKeyDown(Keys.D2)
    num3KeyPress <- keyboardState.IsKeyDown(Keys.D3)
    num4KeyPress <- keyboardState.IsKeyDown(Keys.D4)
        
    ()

