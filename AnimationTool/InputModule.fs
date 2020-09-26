module Input

open Microsoft.Xna.Framework.Input

let mutable plusKeyPress = 0
let mutable minusKeyPress = 0

let update() =
    let keyboardState = Keyboard.GetState()
        
    if keyboardState.IsKeyDown(Keys.Add) || keyboardState.IsKeyDown(Keys.OemPlus) then
        plusKeyPress <- plusKeyPress + 1
    else
        plusKeyPress <- 0

    if keyboardState.IsKeyDown(Keys.Subtract) || keyboardState.IsKeyDown(Keys.OemMinus) then
        minusKeyPress <- minusKeyPress + 1
    else
        minusKeyPress <- 0
        
    ()

