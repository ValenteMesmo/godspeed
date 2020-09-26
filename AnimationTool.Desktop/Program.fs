//TODO: save/load on android
//TODO: add touch controls to increase/decrease pencil size
//TODO: add touch controls to change color

open Game

[<EntryPoint>]
let main argv =
    use g = new MyGame()
    g.Run()
    0