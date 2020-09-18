// Learn more about F# at http://fsharp.org

open System
open Game

[<EntryPoint>]
let main argv =
    use g = new MyGame()
    g.Run()
    0