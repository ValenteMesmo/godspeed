open Game

[<EntryPoint>]
let main argv =
    use g = new MyGame(Config.GameEnviroment.Desktop, Config.ScreenMode.Portrait)
    g.Run()
    0