open Game

[<EntryPoint>]
let main argv =
    use g = new MyGame(true)
    g.Run()
    0